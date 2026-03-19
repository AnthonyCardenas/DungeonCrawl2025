using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class ObstacleAvoidanceTest : MonoBehaviour
{
    private const int obstacleDepth = 5;
    [SerializeField] private PathFindingVisual pathfindingVisual;
    [SerializeField] private PathfindingMovementHandler pathfindingMovement;
    [SerializeField] private GameObject obstacleCopy;
    private List<GameObject> obstacleLocations;

    private Pathfinding pathfinding;
    [SerializeField] private bool drawDebug = true;

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathfinding = new Pathfinding(10, 10);
        // Debug.Log("Text buffer for null");
        if( pathfindingVisual != null)
        {
            pathfindingVisual.SetGrid(pathfinding.GetGrid());
        } else
        {
            Debug.Log("No PathFinding Movement Handler found");
        }
        
        // Set obstacles here
        if(obstacleCopy != null)
        {
            AddObstacle(obstacleCopy);
            AddObstacleAtLocation(15, 15);
            AddObstacleAtLocation(15, 5);
        }
    }

    // // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0))
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            // testing GenGrid for object instantiation
            Vector3 mousePosition = InputUtil.GetActiveMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mousePosition, out int x, out int y);
            // Debug.Log($"End Position: X: {x}; Y: {y}");
            Vector3 startPosition = pathfindingMovement.transform.position;
            // Debug.Log($"Pathfinding Position: {startPosition}");
            pathfinding.GetGrid().GetXY(startPosition, out int startX, out int startY);
            // Debug.Log($"Start Position: X: {startX}; Y: {startY}");
            if(pathfindingMovement != null)
            {
                List<PathNode> path = pathfinding.FindPath(startX, startY, x, y);
                if(path != null)
                {
                    if(drawDebug)
                    {
                        for(int i = 0; i < path.Count - 1; i++)
                        {
                            Debug.DrawLine(new Vector3( path[i].x, path[i].y ) * 10f + Vector3.one * 5f , new Vector3( path[i+1].x, path[i+1].y ) * 10f + Vector3.one * 5f, Color.green, 10f);
                        }
                    }
                    
                    pathfindingMovement.SetTargetPosition(mousePosition);
                }
                else
                {
                    Debug.Log("No Path Found");
                }
            }
            
        }
        // On a right click, toggle the grid node between passable or not
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = InputUtil.GetActiveMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mousePosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsPassable(!pathfinding.GetNode(x, y).isPassable);
        }
        
    }

    private void AddObstacle(GameObject obstacle)
    {
        Vector3 obstaclePos = obstacle.transform.position;
        pathfinding.GetGrid().GetXY(obstaclePos, out int x, out int y);
        pathfinding.GetNode(x, y).SetIsPassable(false);
    }

    private void AddObstacleAtLocation(int nextX, int nextY)
    {
        Vector3 nextPos     = new Vector3(nextX, nextY, obstacleDepth);
        Quaternion nextRot  = Quaternion.identity;
        GameObject nextObstacle = Instantiate(obstacleCopy, nextPos, nextRot);
        // Debug.Log($"Adding next obstacle at {nextX}, {nextY}");
        AddObstacle(nextObstacle);
    }
}
