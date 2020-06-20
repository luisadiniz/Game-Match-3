﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public Action<StoneView> OnStoneSelected;

    public void SetColour(Sprite colour)
    {
        _image.sprite = colour;
    }

    public void OnSelectStone(StoneView stone)
    {
        _image.color = Color.gray;

        OnStoneSelected?.Invoke(stone);
    }

    public void OnUnselectStone(StoneView stone)
    {
        _image.color = Color.white;
    }
}
