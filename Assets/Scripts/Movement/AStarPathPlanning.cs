using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

// Go to A* Pathfinding in Unity by Code Monkey in Youtube. Time 5:00.
public class AStarPathPlanning : MonoBehaviour
{
    [SerializeField] private PathFindingVisual pathfindingVisual;
    [SerializeField] private PathfindingMovementHandler pathfindingMovement;
    private Pathfinding pathfinding;
    [SerializeField] private bool drawDebug = false;

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        pathfinding = new Pathfinding(10, 10);
        // Debug.Log("Text buffer for null");
        pathfindingVisual.SetGrid(pathfinding.GetGrid()); // find null error in this line
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
            // Vector3 startPosition = pathfindingMovement.transform.position;
            // Debug.Log($"Pathfinding Position: {startPosition}");
            // List<PathNode> path = pathfinding.FindPath(Mathf.Clamp(startPosition.x), Mathf.Clamp(startPosition.y), x, y);
            List<PathNode> path = pathfinding.FindPath(0,0, x, y);
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
        // On a right click, toggle the grid node between passable or not
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = InputUtil.GetActiveMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mousePosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsPassable(!pathfinding.GetNode(x, y).isPassable);
        }
        
    }

    // private Grid<PathNode> grid;
    // public void Pathfinding(int width, int height)
    // {
    //     grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    // }
}
