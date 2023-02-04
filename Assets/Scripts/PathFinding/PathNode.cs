
using System;

public class PathNode
{
    private Grid<PathNode> grid;
    private int _x;
    private int _y;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode cameFromNode;
    public bool isWalkable = true;

    public int X => _x;
    public int Y => _y;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        _x = x;
        _y = y;
        this.grid = grid;
    }

    public override string ToString()
    {
        return _x + ", " + _y;
    }

    internal void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
