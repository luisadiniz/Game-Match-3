using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public int PosX { get; set; }
    public int PosY { get; set; }
    public Action<StoneView> OnClick;

    public void SetColour(Sprite colour)
    {
        _image.sprite = colour;
    }

    public void OnSelectStone(StoneView stone)
    {
        _image.color = Color.gray;

        OnClick?.Invoke(stone);
    }

    public void OnUnselectStone()
    {
        _image.color = Color.white;
    }
}
