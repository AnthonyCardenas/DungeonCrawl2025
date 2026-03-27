using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class RoomGridHandler : MonoBehaviour
{
    private const int obstacleDepth = 5;
    [SerializeField] private PathFindingVisual pathfindingVisual;
    // [SerializeField] private PathfindingMovementHandler pathfindingMovement;
    // [SerializeField] private GameObject targetCharacter;
    [SerializeField] private GameObject obstacleCopy;
    private List<GameObject> obstacleLocations;

    private Pathfinding pathfinding;
    // [SerializeField] private bool drawDebug = false;
    [SerializeField] private bool showGridBackdrop = true;
    [SerializeField] private float gridCellSize = 2.5f;

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathfinding = new Pathfinding(8, 6, gridCellSize);
        if( pathfindingVisual != null)
        {
            if(showGridBackdrop)
            {
                pathfindingVisual.SetGrid(pathfinding.GetGrid());
            }
            
        } else
        {
            Debug.Log("No PathFinding Movement Handler found");
        }
        
        // Set obstacles here
        if(obstacleCopy != null)
        {
            AddObstacle(obstacleCopy);
            // AddObstacleAtLocation(15, 15);
            // AddObstacleAtLocation(15, 5);
        }
    }

    // // Update is called once per frame
    void Update()
    {
        // // On a right click, toggle the grid node between passable or not
        // if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        // {
        //     Vector3 mousePosition = InputUtil.GetActiveMouseWorldPosition();
        //     pathfinding.GetGrid().GetXY(mousePosition, out int x, out int y);
        //     pathfinding.GetNode(x, y).SetIsPassable(!pathfinding.GetNode(x, y).isPassable);
        // }
        
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

