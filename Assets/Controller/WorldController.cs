using UnityEngine;
using System.Collections.Generic;
using System;

public class WorldController : MonoBehaviour {

    private static WorldController _instance;
    public static WorldController Instance {
        get { return _instance; }
    }

    private World world;
    public World World {
        get { return world; }
    }

    Dictionary<Tile, GameObject> tileGameObjectMap;
    Dictionary<string, Sprite> tileSprites;

    void Awake() {
        if (_instance != null) {
            Debug.LogError("WorldController already instantiated.");
        }

        _instance = this;

        LoadSprites();

        world = new World(10,10);

        tileGameObjectMap = new Dictionary<Tile, GameObject>();

        for (int x = 0; x < world.Width; x++) {
            for (int y = 0; y < world.Height; y++) {
                Tile tile_data = world.GetTileAt(x, y);

                GameObject tile_go = new GameObject();
                tileGameObjectMap.Add(tile_data, tile_go);

                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
                tile_go.transform.SetParent(this.transform);

                tile_go.AddComponent<SpriteRenderer>().sprite = GetTileSprite(tile_data);
                
                tile_data.RegisterTileTypeChanged(OnTileTypeChanged);
            }
        }

        Camera.main.transform.position = new Vector3(world.Width / 2, world.Height / 2, Camera.main.transform.position.z);
    }

    private void LoadSprites() {
        tileSprites = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Tiles");

        foreach (Sprite sprite in sprites) {
            Debug.Log("Loaded sprite: " + sprite.name);
            tileSprites.Add(sprite.name, sprite);
        }
    }

    void OnTileTypeChanged(Tile t_data) {
        throw new NotImplementedException();
    }

    Sprite GetTileSprite(Tile t_data) {
        // get correct sprite according to the type of the tile

        //randomize grass because we have 2 different grass tiles
        if(t_data.Type == TileType.Grass) {
            if (UnityEngine.Random.Range(1, 3) == 1)
                return tileSprites["Grass"];
            else
                return tileSprites["Grass2"];
        }

        if (t_data.Type == TileType.Dirt) {
            if (UnityEngine.Random.Range(1, 3) == 1)
                return tileSprites["Dirt"];
            else
                return tileSprites["Dirt2"];
        }

        if (t_data.Type == TileType.Rock) {
            if (UnityEngine.Random.Range(1, 3) == 1)
                return tileSprites["Rock"];
            else
                return tileSprites["Rock2"];
        }

        if (t_data.Type == TileType.Sand) {
            if (UnityEngine.Random.Range(1, 3) == 1)
                return tileSprites["Sand"];
            else
                return tileSprites["Sand2"];
        }

        // if no sprite is found return null
        return null;
    }
}
