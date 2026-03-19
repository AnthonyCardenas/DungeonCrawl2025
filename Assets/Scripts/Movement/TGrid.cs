using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TGrid<TGridObject>
{
    // subscriber/publisher logic
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;

    private bool textDebug = false;
    private bool lineDebug = true;

    public TGrid(int width, int height, float cellSize, Vector3 originPosition, Func<TGrid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width,  height];
        debugTextArray = new TextMesh[width, height];

        // Initialize Grid
        // Debug.Log($"Width: {width}, Height: {height}");
        for(int x = 0;  x < gridArray.GetLength(0); x++)
        {
            for(int y = 0;  y < gridArray.GetLength(1); y++)
            {
                gridArray[x,y] = createGridObject(this, x, y);
            }
        }

        ///// Initialize Grid with debug text  /////
        // bool lineDebug = true;
        // bool textDebug = false;
        if(lineDebug)
        {
            // debugTextArray = new TextMesh[width, height];
            for(int x = 0;  x < gridArray.GetLength(0); x++)
            {
                for(int y = 0;  y < gridArray.GetLength(1); y++)
                {
                    // Debug.Log(x + "," + y);
                    if(textDebug)
                    {
                        debugTextArray[x,y] = TextUtil.CreateWorldText(gridArray[x,y]?.ToString(), null, GetWorldPosition(x,y) + new Vector3(cellSize, cellSize) * 0.5f , 20, Color.white, TextAnchor.MiddleCenter);
                    }
                    
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0),  GetWorldPosition(width, height), Color.white, 100f);
            
            // publisher/subscriber logic
            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
            {
                if(textDebug)
                {
                    debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
                }
                
            };
        }
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt( (worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt( (worldPosition - originPosition).y / cellSize);
    }

    public void SetGridObject(int x, int y, TGridObject gridObj)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x,y] = gridObj;
            debugTextArray[x,y].text = gridArray[x,y].ToString();
            // if(OnGridValueChanged != null) 
            //     OnGridValueChanged(this, new OnGridValueChangedEventArgs { x= x, y =y});
        }
    }

    public void SetGridObject(Vector2 worldPosition, TGridObject gridObj)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, gridObj);
        if (OnGridObjectChanged != null) 
            OnGridObjectChanged(this, new OnGridObjectChangedEventArgs{ x = x, y = y});
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null) 
            OnGridObjectChanged(this, new OnGridObjectChangedEventArgs{ x = x, y = y});
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x,y];
        } else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector2 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

}