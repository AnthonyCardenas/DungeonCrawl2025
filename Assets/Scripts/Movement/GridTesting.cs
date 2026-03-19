using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;

public class GridTesting : MonoBehaviour
{
    // private Grid grid;
    private GenGrid<HeatMapGridObject> grid;

    private void Start()
    {
        //Debug.Log("Go to Grid System in Unity by Code Monkey in Youtube.");
        Debug.Log("Go to A* PathFinding in Unity by Code Monkey in Youtube.");
        // for testing resular grid
        // grid = new Grid(5, 2, 7f); // grid without origin specified
        // grid = new Grid(5, 2, 10f, new Vector3(-20, -10));
        // grid = new GenGrid<bool>(5, 2, 10f, new Vector3(-20, -10));
        // for testing GenGrid
        grid = new GenGrid<HeatMapGridObject>(5, 2, 10f, new Vector3(-20, -10), (GenGrid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
    }

    private void Update()
    {
        // if(Input.GetMouseButtonDown(0))
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            // testing GenGrid for object instantiation
            Vector3 position = InputUtil.GetActiveMouseWorldPosition();
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            if(heatMapGridObject != null)
            {
                heatMapGridObject.AddValue(5);
            }
            // tesing regular grid
            //grid.SetValue(InputUtil.GetActiveMouseWorldPosition(), 42);
        }
        // if(Input.GetMouseButtonDown(1))
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            // tesing regular grid
            // Debug.Log( grid.GetValue(InputUtil.GetActiveMouseWorldPosition()) );
            // tesing GenGrid
            Debug.Log( grid.GetGridObject(InputUtil.GetActiveMouseWorldPosition()).ToString() );
        }
    }

}


public class HeatMapGridObject
{
    private const int MIN = 0;
    private const int MAX = 10;

    private GenGrid<HeatMapGridObject> grid;
    private int x;
    private int y;
    private int value;

    public HeatMapGridObject(GenGrid<HeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }
    public float GetValueNormalized()
    {
        return Mathf.Clamp(value, MIN, MAX);
    }

    public override string ToString()
    {
        return value.ToString();
    }
}

// private class HeatMapVisual
// {
//     private Grid grid;
//     private Mesh mesh;

//     public HeatMapVisual(Grid grid, MeshFilter meshFilter)
//     {
//         this.grid = grid;
        
//         mesh = new Mesh();
//         meshFilter.mesh = mesh;

//         UpdateHeatMapVisual();

//         grid.OnGridObjectChanged += Grid_OnGridValueChanged;
//     }

//     private void Grid_OnGridValueChanged(object sender, System.EventArgs e)
//     {
//         UpdateHeatMapVisual();
//     }

//     public void UpdateHeatMapVisual()
//     {
//         Vector3[] vertices;
//         Vector2[] uvs;
//         int[] triangles;

//         MeshUtil.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out vertices, out uvs, out triangles);

//         for(int x = 0; x < grid.GetWidth(); x++)
//         {
//              for(int y = 0; y < grid.GetHeight(); y++)
//             {
//                 int index = x * grid.GetHeight() + y;
//                 Vector3 baseSize = new Vector3(1,1) * grid.GetCellSize();
//                 int gridValue = grid.GetValue(x, y);
//                 int maxGridValue = 100;
//                 float gridValueNormalized = Mathf.Clamp01((float)gridValue / maxGridValue);
//                 Vector2 gridCellUV = new Vector2(gridValueNormalized, 0f);
//                 MeshUtil.AddToMeshArrays(vertices, uvs, triangles, index, grid.GetWorldPosition(x,y), 0f, baseSize, gridCellUV, Vector2.zero);
//             }
//         }

//         mesh.vertices = vertices;
//         mesh.uv = uvs;
//         mesh.triangles = triangles;
//     }
// }