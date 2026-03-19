using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Go to A* Pathfinding in Unity by Code Monkey in Youtube. Time 5:00.
public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance { get; private set; }

    private GenGrid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    

    public Pathfinding(int width, int height)
    {
        Instance = this;
        grid = new GenGrid<PathNode>(width, height, 10f, Vector3.zero, (GenGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public GenGrid<PathNode> GetGrid()
    {
        return grid;
    }

    /// Returns a List of vectors leading to the end position
    public List<Vector3> FindVPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        // Debug.Log($"Start Position: {startWorldPosition}, End Position: {endWorldPosition}");
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);
        
        // Debug.Log($"Start Position: {startX}, {startY}; End Position: {endX}, {endY}");
        List<PathNode> path = FindPath(startX, startY, endX, endY);

        if(path == null)
        {
            return null;
        } else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach( PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }
    }

    // A* Algorithm to find the path
    // returns a list of nodes that hold coordinates of the path from start to end
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode   = grid.GetGridObject(endX, endY);

        openList   = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        // Set up a node in every grid position
        // maybe improve this by making it not On^2
        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for(int y = 0; y < grid.GetWidth(); y++)
            {
                PathNode currNode = grid.GetGridObject(x, y);
                currNode.gCost = int.MaxValue;
                currNode.CalculateFCost();
                currNode.cameFromNode = null;
            }
        }
        // Debug.Log("Setup the grid to pathfind.");

        // Set up the initial value for the starting node
        startNode.gCost = 0;
        startNode.hCost = CalcDistCost(startNode, endNode);
        startNode.CalculateFCost();

        // loop through the grid/ curr nodes passed through
        while ( openList.Count > 0 )
        {
            // move to node with lowest fCost
            PathNode currNode = GetLowestFCostNode(openList);

            // end algorithm once end is reached
            if(currNode == endNode)
            {
                //reached final node / destination
                return CalcPath(endNode);
            }

            // curr node has been searched so remove from list
            openList.Remove(currNode);
            closedList.Remove(currNode);

            // add valid neighbors to open list
            foreach(PathNode neighborNode in GetNeighborList(currNode))
            {
                // check closed list, which means it's been searched previously
                if(closedList.Contains(neighborNode))
                {
                    continue;
                }
                // Check to see if node path is blocked
                if (!neighborNode.isPassable)
                {
                    closedList.Add(neighborNode);
                    continue;   
                }

                // check the gcost to check if its a faster path
                int tentativeGCost = currNode.gCost + CalcDistCost(currNode, neighborNode);
                if (tentativeGCost < neighborNode.gCost)
                {
                    // update values
                    neighborNode.cameFromNode = currNode;
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = CalcDistCost(neighborNode, endNode);
                    neighborNode.CalculateFCost();
                    // Debug.Log($"Checking the grid at position X: {neighborNode.x}, Y: {neighborNode.y}.");

                    // Add to important nodes to be searched
                    if(!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        // Out of nodes on the open list
        // Debug.Log("No path found.");
        return null;
    }

    private int CalcDistCost(PathNode a, PathNode b)
    {
        int xDist = Mathf.Abs(a.x - b.x);
        int yDist = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDist - yDist);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDist, yDist) + MOVE_STRAIGHT_COST * remaining;
    }

    // Maybe improve this by making it not linear, look into binary tree or OrderedList
    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        // cycle through whole list to find lowest fCost node
        for(int i = 1; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

    // Follow trail of CameFromNodes to get from end to start
    private List<PathNode> CalcPath(PathNode endNode)
    {
        // Initialize the path with the end node
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currNode = endNode; 

        // only start node has null for CameFromNode value
        while (currNode.cameFromNode != null)
        {
            // Add each node to the path
            path.Add(currNode.cameFromNode);
            // Continue back along the path
            currNode = currNode.cameFromNode;
        }

        // Path was made backwards
        path.Reverse();
        return path;
    }

    // Might be able to optimize by setting this before
    // Changed to not squeeze between two diagonal squares
    private List<PathNode> GetNeighborList(PathNode currNode)
    {
        List<PathNode> neighborList = new List<PathNode>();
        
        // Check X boundaries
        // Check boundaries to the left
        if(currNode.x - 1 >= 0)
        {
            // Left
            if ( GetNode(currNode.x - 1, currNode.y).isPassable )
            {
                neighborList.Add( GetNode(currNode.x - 1, currNode.y) );
                // Left Down
                if(currNode.y - 1 >= 0 && GetNode(currNode.x, currNode.y - 1).isPassable)
                    neighborList.Add( GetNode(currNode.x - 1, currNode.y - 1) );
                // Left Up
                if(currNode.y + 1 < grid.GetHeight() && GetNode(currNode.x, currNode.y + 1).isPassable)
                    neighborList.Add( GetNode(currNode.x - 1, currNode.y + 1) );
            }
            
        }
        // Check boundaries to the right
        if( currNode.x + 1 < grid.GetWidth() )
        {
            if ( GetNode(currNode.x + 1, currNode.y).isPassable )
            {
                // Right
                neighborList.Add( GetNode(currNode.x + 1, currNode.y));
                // Right Down
                if(currNode.y - 1 >= 0 && GetNode(currNode.x, currNode.y - 1).isPassable)
                    neighborList.Add( GetNode(currNode.x + 1, currNode.y - 1) );
                // Right Up
                if(currNode.y + 1 < grid.GetHeight() && GetNode(currNode.x, currNode.y + 1).isPassable)
                    neighborList.Add( GetNode(currNode.x + 1, currNode.y + 1) );
            }
            
        }
        // Check Y Boundaries
        // Down
        if( currNode.y - 1 >= 0 && GetNode(currNode.x, currNode.y - 1).isPassable ) 
            neighborList.Add( GetNode(currNode.x, currNode.y - 1) );
        // Up
        if(currNode.y + 1 < grid.GetHeight() && GetNode(currNode.x, currNode.y + 1).isPassable )
            neighborList.Add( GetNode(currNode.x, currNode.y + 1) );
        
        return neighborList;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x,y);
    }

}