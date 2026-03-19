using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PathNode
{
    
    private GenGrid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isPassable; // path is available through this node
    public PathNode cameFromNode;

    public PathNode(GenGrid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.isPassable = true;
    }

    public void SetIsPassable(bool isPassable)
    {
        this.isPassable = isPassable;
        grid.TriggerGridObjectChanged(x, y);
    }

    // public int GetX()
    // {
    //     return x;
    // }

    // public int GetY()
    // {
    //     return y;
    // }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public override string ToString()
    {
        return x + "," + y;
    }
}