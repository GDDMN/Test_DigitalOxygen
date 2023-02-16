using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinding 
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 13;

    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;
        

    public PathFinding(int width, int height, Tilemap tilemap)
    {
        grid = new Grid<PathNode>(width, height, 1.0f, new Vector3(-11, -.5f),
            (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));

        Transform[] tilepositions = tilemap.gameObject.GetComponentsInChildren<Transform>();
        
        foreach(var tile in tilepositions)
        {
            grid.GetXY(tile.position, out int x , out int y);
            
            if(x < width && x > 0 && y > 0 && y < height)
                grid.GetGridObject(x, y-1).isWalkable = false;
        }
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode closedNode = grid.GetGridObject(endX, endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x=0;x < grid.GetWidth; x++)
        {
            for (int y = 0; y < grid.GetHeight; y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, closedNode);
        startNode.CalculateFCost();

        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if(currentNode == closedNode)
            {
                return CalculatePath(closedNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(PathNode neighbourNode in GetNeighboursList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if(!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if(tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, closedNode);
                    neighbourNode.CalculateFCost();

                    if(!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        return null;
    }

    private List<PathNode> GetNeighboursList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if(currentNode.X - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y));
            if (currentNode.Y - 1 >= 0) neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
            if (currentNode.Y + 1 < grid.GetHeight) neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
        }
        if (currentNode.X + 1 < grid.GetWidth)
        {
            neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y));
            if (currentNode.Y - 1 >= 0) neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y - 1));
            if (currentNode.Y + 1 < grid.GetHeight) neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y + 1));
        }
        if (currentNode.Y - 1 >= 0) neighbourList.Add(GetNode(currentNode.X, currentNode.Y - 1));
        if (currentNode.Y + 1 < grid.GetHeight) neighbourList.Add(GetNode(currentNode.X, currentNode.Y + 1));

        return neighbourList;
    }

    private PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathNode> CalculatePath(PathNode closedNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(closedNode);
        PathNode currentNode = closedNode;

        while(currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }

        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.X - b.X);
        int yDistance = Mathf.Abs(a.Y - b.Y);
        int remeaning = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remeaning;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for(int i=1;i<pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }

        return lowestFCostNode;
    }
}
