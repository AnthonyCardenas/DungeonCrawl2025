using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

// Go to A* Pathfinding in Unity by Code Monkey in Youtube.
public class PathFindingVisual : MonoBehaviour
{
    private GenGrid<PathNode> grid;
    private Mesh mesh;
    private bool updateMesh;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        // Debug.Log("Working on visuals for pathfinding in script PathfindingVIsuals.");
    }

    public void SetGrid(GenGrid<PathNode> grid)
    {
        this.grid = grid;
        UpdateVisual();

        grid.OnGridObjectChanged += Grid_OnGridValueChanged;
    }

    public void Grid_OnGridValueChanged(object sender, GenGrid<PathNode>.OnGridObjectChangedEventArgs e)
    {
        // Debug.Log("Changed event on Grid_OnGridValueChanged");
        updateMesh = true;
    }

    private void LateUpdate()
    {
        if(updateMesh)
        {
            updateMesh = false;
            UpdateVisual();
        }
    }

    private void UpdateVisual()
    {
        // Debug.Log("Updating Visuals.");
        MeshUtil.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);


        // cycle through each grid position
        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for(int y = 0; y < grid.GetHeight(); y++)
            {
                int index = x * grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1,1) * grid.GetCellSize();

                PathNode pathNode = grid.GetGridObject(x, y);

                int fCost = 0;
                fCost = pathNode.fCost;
                // Debug.Log($"Updating Visuals with gCost:  {gCost}");
                float colorValue = 0.0f;
                // if (fCost > 10)
                // {
                //     colorValue = 0.2f;
                // }
                // else
                // {
                //     colorValue = 0.0f;
                // }
                
                if(!pathNode.isPassable)
                {
                    colorValue = 0.5f;
                    // quadSize = Vector3.zero;
                }

                Vector2 gridValueUV = new Vector2(colorValue, 0f);

                // MeshUtil.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, Vector2.zero, Vector2.zero);
                MeshUtil.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + (quadSize * .5f), 0f, quadSize, gridValueUV, gridValueUV);
            }
        }
        // Debug.Log($"Updating Visuals with value: {0.3f}");

        // Update Mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

}