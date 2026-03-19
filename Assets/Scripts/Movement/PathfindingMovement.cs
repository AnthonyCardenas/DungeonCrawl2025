using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

// Go to A* Pathfinding in Unity by Code Monkey in Youtube. Time 5:00.
public class PathfindingMovementHandler : MonoBehaviour
{
    private const float speed = 10.0f;
    private const float centeredDist = 1.0f;
    private int currPathIndex;
    private List<Vector3> pathVectorList;
    // [SerializeField] private GameObject targetObject;

    private void Start()
    {
        // Transform bodyTransform = transform.Find("Body");
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Debug.Log("Entering Handle Movement func in PathfindingMovement");
        if(pathVectorList != null)
        {
            // Debug.Log($"Handling Index: {currPathIndex}");
            Vector3 targetPosition = pathVectorList[currPathIndex];
            // Debug.Log($"Target Position: {targetPosition}");
            float travelDist = Vector3.Distance(transform.position, targetPosition);
            // Debug.Log($"Distance to travel: {Vector3.Distance(transform.position, targetPosition)}");
            // Debug.Log($"Distance to travel: {travelDist}");
            // if( Vector3.Distance(transform.position, targetPosition)  < 1f )
            if( travelDist > centeredDist )
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                // Debug.Log($"Move Direction: {moveDir}");

                // float distBefore = Vector3.Distance(transform.position, targetPosition);
                // animatedWalker.SetMoveVector(moveDir);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
                // Debug.Log($"New Position: {moveDir}");
            } else
            {
                currPathIndex++;
                if(currPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                    // animatedWalker.SetMoveVector(Vector3.zero);
                }
            }
        } else
        {
            // Set the animation to 
            // animatedWalker.SetMoveVector(Vector3.zero);
        }
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        // reset path index
        currPathIndex = 0;
        // Get a list of vectors leading to the target position
        Vector3 startPosition = GetPosition();
        // Debug.Log($"Character's position: {GetPosition()}");
        // Debug.Log($"Target position: {targetPosition}");
        pathVectorList = Pathfinding.Instance.FindVPath(startPosition, targetPosition);

        if(pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
}
