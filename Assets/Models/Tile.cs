using UnityEngine;
using System.Collections;
using System;

public enum TileType { Grass, Rock, Sand, Dirt, GrassRoad }

public class Tile {

    private TileType type = TileType.Rock;

    Action<Tile> cbTileTypeChanged;

    public TileType Type {
        get { return type; }
        set {
            TileType old = type;
            type = value;

            if (cbTileTypeChanged != null && old != type)
                cbTileTypeChanged(this);
        }
    }

    public World World { get; protected set; }
    public int X { get; protected set; }
    public int Y { get; protected set; }

    public Tile(World world, int x, int y) {
        this.World = world;
        this.X = x;
        this.Y = y;
    }

    public void RegisterTileTypeChanged(Action<Tile> cb) {
        cbTileTypeChanged += cb;
    }

    public void UnregisterTileTypeChanged(Action<Tile> cb) {
        cbTileTypeChanged -= cb;
    }
}
