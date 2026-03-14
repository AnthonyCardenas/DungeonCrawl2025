using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;


    public Grid(int width, int height, float cellSize, Vector3 originPosition = default(Vector3))
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width,  height];
        debugTextArray = new TextMesh[width, height];

        // Debug.Log($"Width: {width}, Height: {height}");

        for(int x = 0;  x < gridArray.GetLength(0); x++)
        {
            for(int y = 0;  y < gridArray.GetLength(1); y++)
            {
                // Debug.Log(x + "," + y);
                debugTextArray[x,y] = TextUtil.CreateWorldText(gridArray[x,y].ToString(), null, GetWorldPosition(x,y) + new Vector3(cellSize, cellSize) * 0.5f , 20, Color.white, TextAnchor.MiddleCenter);
                
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0),  GetWorldPosition(width, height), Color.white, 100f);

        SetValue(2,1, 42);

    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt( (worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt( (worldPosition - originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x,y] = value;
            debugTextArray[x,y].text = gridArray[x,y].ToString();
        }
    }

    public void SetValue(Vector2 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x,y];
        } else
        {
            return -1;
        }
    }
    public int GetValue(Vector2 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

}