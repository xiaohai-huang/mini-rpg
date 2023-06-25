using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputUtil
{
    public static Vector3 GetPointerPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            return hitInfo.point;
        }
        return Vector3.zero;
    }
}
