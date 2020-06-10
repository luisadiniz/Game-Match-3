using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public void SetColour(Sprite colour)
    {
        _image.sprite = colour;
    }
}
