using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class GridTesting : MonoBehaviour
{

    private Grid grid;

    private void Start()
    {
        //Debug.Log("Go to Grid System in Unity by Code Monkey in Youtube.");
        Debug.Log("Go to A* PathFinding in Unity by Code Monkey in Youtube.");
        // grid = new Grid(5, 2, 7f); // grid without origin specified
        grid = new Grid(5, 2, 10f, new Vector3(-20, -10));
    }

    private void Update()
    {
        // if(Input.GetMouseButtonDown(0))
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            grid.SetValue(InputUtil.GetActiveMouseWorldPosition(), 44);
        }
        // if(Input.GetMouseButtonDown(1))
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            Debug.Log( grid.GetValue(InputUtil.GetActiveMouseWorldPosition()) );
        }
    }

}