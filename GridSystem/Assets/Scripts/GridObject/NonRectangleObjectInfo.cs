using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GridObjectInfo",fileName = "NonRectangleObjectInfo")]
public class NonRectangleObjectInfo : SpriteWithSize
{
    public Vector2[] CellsPosFromPivot;
}
