using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public GameObject world;
    public Dictionary<string, Tilemap> worldTilemaps;

    public GameObject garden;
    public Dictionary<string, Tilemap> gardenTilemaps;

    public GameObject defaultTilemap;

    public bool isInWorldView;
    public Garden activeGarden;
    public StringReference viewMode;

    public TileBaseDatabase tileBaseDatabase;
    public TileWithInfo tileToPlace;

    public LevelData level;
    public TilemapWithInfo worldTiles;

    public StringReference mouseState;
    public StringReference selectedHotbarItem;

    public int isMouseHovering = 0;

    public void MouseEnteredUI() { isMouseHovering += 1;  }
    public void MouseExitUI() { isMouseHovering -= 1; }

    void Start()
    {
        world = GameObject.FindGameObjectWithTag("WorldTileMap");
        garden = GameObject.FindGameObjectWithTag("GardenTileMap");

        worldTilemaps = new Dictionary<string, Tilemap>();
        gardenTilemaps = new Dictionary<string, Tilemap>();

        worldTiles = level.worldTiles;

        foreach(TilemapWithInfoLayer layer in worldTiles.layers)
        {
            Debug.Log(layer.layerName);
            CreateTilemap(world, layer.layerName);
        }

        SetLevelTiles(worldTiles, worldTilemaps);
    }

    private void Update()
    {
        if(mouseState.Value != "Edit") { return;  }

        if (Input.GetMouseButtonDown(0))
        {
            if (selectedHotbarItem.Value != "none" && 
                isMouseHovering == 0 && 
                tileBaseDatabase.GetTileBase(selectedHotbarItem.Value, worldTiles) != null)
            {
                TileWithInfo toPlace = tileBaseDatabase.GetTileBase(selectedHotbarItem.Value, worldTiles);

                Vector3Int pos = worldTilemaps[toPlace.targetLayer].WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                PlaceTile(pos, toPlace, worldTilemaps[toPlace.targetLayer]);
            }
            
        }

        if (Input.GetMouseButtonDown(1) && isMouseHovering == 0)
        {
            Vector3Int pos = worldTilemaps["ground"].WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            RemoveTile(pos, worldTilemaps["ground"]);
        }

        else if (Input.GetAxisRaw("SwapView") != 0)
        {
            changeViewMode();
        }
    }

    private void changeViewMode()
    {
        if (isInWorldView)
        // is editing the world
        {
            garden.SetActive(false);
            world.SetActive(true);

            isInWorldView = !isInWorldView;
        }
        else
        // is editing a garden
        {
            if (activeGarden == null) return;

            garden.SetActive(true);
            world.SetActive(false);

            SetLevelTiles(activeGarden, gardenTilemaps);

            isInWorldView = !isInWorldView;
        }
    }

    private void LoadGarden(string id)
    {
        SetLevelTiles(level.GetGarden(id), gardenTilemaps);
    }

    void CreateTilemap(GameObject parent, string name)
    {
        GameObject newTilemap = Instantiate(defaultTilemap, parent.transform);
        newTilemap.name = name;
        
        worldTilemaps[name] = newTilemap.GetComponent<Tilemap>();  
    }

    void SetLevelTiles(TilemapWithInfo tilemap, Dictionary<string, Tilemap> maps)
    {
        foreach (Tilemap map in maps.Values)
        {
            map.ClearAllTiles();
        }
        
        foreach (TilemapWithInfoLayer layer in tilemap.layers)
        {
            Debug.Log($"layer name: {layer.layerName}");
            foreach (KeyValuePair<Vector3Int, InfoContainer> kvp in layer.tileInfo)
            {
                TileBase tileBase = tileBaseDatabase.GetTileBase(kvp.Value.tileId, tilemap);

                if(tileBase != null)
                {
                    maps[layer.layerName].SetTile(kvp.Key, tileBase);
                }
            }
        }
    }

    public void ClearWorld(Dictionary<string, Tilemap> map)
    {
       
    }

    public void PlaceTile(Vector3Int position, TileWithInfo tileToPlace, Tilemap map)
    {
        worldTiles.SetTileInfo(position, tileToPlace.GenerateTileInfoContainer());
        
        map.SetTile(position, tileToPlace);
    }

    public void RemoveTile(Vector3Int position, Tilemap map)
    {
        level.worldTiles.RemoveTile(position, 0);
        map.SetTile(position, null);
    }
}
