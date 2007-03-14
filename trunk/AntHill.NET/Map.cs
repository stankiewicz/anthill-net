using System;

public class Map
{
    private int width;
    private int height;
    private TileType[][] tiles;

    public Map() {}

    public int GetWidth {
		get { return width; }
	}

    public int GetHeight {
		get { return height; }
	}

    public TileType GetTile(int x, int y) { return tiles[x][y]; }    
}