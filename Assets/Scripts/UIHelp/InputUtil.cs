using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

//Look at Jaredbest code monkey github

public static class InputUtil
{
     // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition() {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ() {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static Vector3 GetDirToMouse(Vector3 fromPosition) {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        return (mouseWorldPosition - fromPosition).normalized;
    }


    // Get Mouse Position in World with Z = 0f using unity active input system
    public static Vector3 GetActiveMouseWorldPosition() {
        Vector3 vec = GetMouseWorldPositionWithZ(Mouse.current.position.ReadValue(), Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetActiveMouseWorldPositionWithZ() {
        return GetActiveMouseWorldPositionWithZ(Mouse.current.position.ReadValue(), Camera.main);
    }

    public static Vector3 GetActiveMouseWorldPositionWithZ(Camera worldCamera) {
        return GetActiveMouseWorldPositionWithZ(Mouse.current.position.ReadValue(), worldCamera);
    }

    public static Vector3 GetActiveMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static Vector3 GetActiveDirToMouse(Vector3 fromPosition) {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        return (mouseWorldPosition - fromPosition).normalized;
    }

}