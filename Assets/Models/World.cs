using UnityEngine;
using System.Collections;

public class World {

    Tile[,] tiles;

    public int Width { get; protected set; }
    public int Height { get; protected set; }

    public World(int width = 100, int height = 100) {
        this.Width = width;
        this.Height = height;

        tiles = new Tile[Width, Height];

        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                tiles[x, y] = new Tile(this, x, y);
            }
        }

        Debug.Log("World created with " + Width * Height + " tiles.");
    }

    public Tile GetTileAt(int x, int y) {
        if(x > Width || x < 0 || y > Height || y < 0) {
            Debug.LogError("Tile (" + x + "," + y + ") is out of range.");
            return null;
        }

        return tiles[x, y];
    }
}
