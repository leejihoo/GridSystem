using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;
using UnityEngine.UI;

public class RectangleGridObject : MonoBehaviour,IGridObject
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int OnGridPositionX { get; set; }
    public int OnGridPositionY { get; set; }
    public Size GridObjectInfo { get; set; }

    private bool _isRotate;
    
    public void SetGridObjectInfo(Size size)
    {
        GridObjectInfo = size;

        GetComponent<Image>().sprite = (size as SpriteWithSize)?.Sprite;
        
        Vector2 sizeDelta = new Vector2();
        
        // tile size 만큼 곱해야 됨.
        sizeDelta.x = Width;
        sizeDelta.y = Height;
        GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    public void Rotate()
    {
        _isRotate = !_isRotate;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, _isRotate? 90f : 0f);
    }
}
