using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interface;
using UnityEngine;
using UnityEngine.UI;

public class NonRectangleObject : MonoBehaviour,IGridObject
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int OnGridPositionX { get; set; }
    public int OnGridPositionY { get; set; }
    [field:SerializeField] public List<Vector2> CellPosList { get; set; }

    public Size GridObjectInfo { get; set; }
    private ObjectYAxisDirection _yAxisDirection;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //Debug.Log(GridObjectInfo.GetType());
        Width = GridObjectInfo.Width;
        Height = GridObjectInfo.Height;
        
        SetGridObjectInfo(GridObjectInfo);
        
    }

    public void SetGridObjectInfo(Size size)
    {
        CellPosList = (size as NonRectangleObjectInfo)?.CellsPosFromPivot.ToList();
        
        GetComponent<Image>().sprite = ((NonRectangleObjectInfo)size).Sprite;
        //GridObjectInfo = size;
        Vector2 sizeDelta = new Vector2();
        
        // tile size 만큼 곱해야 됨.
        sizeDelta.x = Width * 32;
        sizeDelta.y = Height * 32;
        GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    // 시계방향으로 회전 유니티는 왼손좌표계이기 때문에 z축을 기준으로 -값을 넣어야 시계방향으로 회전
    public void Rotate()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        // switch (_yAxisDirection)
        // {
        //     case ObjectYAxisDirection.North:
        //         rectTransform.rotation = Quaternion.Euler(0, 0, -90f);
        //         _yAxisDirection = ObjectYAxisDirection.East;
        //         break;
        //     case ObjectYAxisDirection.East:
        //         rectTransform.rotation = Quaternion.Euler(0, 0, -180f);
        //         _yAxisDirection = ObjectYAxisDirection.South;
        //         break;
        //     case ObjectYAxisDirection.South:
        //         rectTransform.rotation = Quaternion.Euler(0, 0, -270f);
        //         _yAxisDirection = ObjectYAxisDirection.West;
        //         break;
        //     case ObjectYAxisDirection.West:
        //         rectTransform.rotation = Quaternion.Euler(0, 0, 0f);
        //         _yAxisDirection = ObjectYAxisDirection.North;
        //         break;
        // }

        var targetDirection = (int)_yAxisDirection + 1;
        rectTransform.rotation = Quaternion.Euler(0, 0, targetDirection * -90f);
        _yAxisDirection = (ObjectYAxisDirection)(targetDirection % 4);

        for (int i = 0; i < CellPosList.Count; i++)
        {
            var rotatedX = Math.Round(CellPosList[i].x * Mathf.Cos(Mathf.PI/2) + CellPosList[i].y * Mathf.Sin(Mathf.PI/2),1);     
            var rotatedY = Math.Round(-CellPosList[i].x * Mathf.Sin(Mathf.PI/2) + CellPosList[i].y * Mathf.Cos(Mathf.PI/2),1);     
            Vector2 rotatedVector = new Vector2((float)rotatedX, (float)rotatedY);
            CellPosList[i] = rotatedVector;
        }
    }
}

/*
0 = spawn state
1 = state resulting from a clockwise rotation ("right") from spawn
2 = state resulting from 2 successive rotations in either direction from spawn.
3 = state resulting from a counter-clockwise ("left") rotation from spawn
*/

public enum ObjectYAxisDirection
{
    North,
    East,
    South,
    West
}
