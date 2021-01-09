using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectEx
{
    public static GameObject ExInstantiate(this GameObject prefab,  Vector3 position , Quaternion q )
    {
        return GameObject.Instantiate(prefab, position, q);
    }
}
