using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stone : MonoBehaviour
{
    public enum Colours
    {
        Red,
        Blue,
        Purple,
        Green
    }

    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    private Colours _currentColour;
    private Color _color;

    public Action<Stone> OnStoneSelected;

    public int PosX { get; set; }
    public int PosY { get; set; }
    public Color Color { get { return _color;}}

    private void Start()
    {
        TintStones();
    }

    public Color SetRandomColor()
    {
        int sortedNumber = UnityEngine.Random.Range(0, Colours.GetNames(typeof(Colours)).Length);
        _currentColour = (Colours)sortedNumber;

        switch (_currentColour)
        {
            case Colours.Red:
                _color = Color.red;
                break;

            case Colours.Blue:
                _color = Color.blue;
                break;

            case Colours.Purple:
                _color = Color.magenta;
                break;

            case Colours.Green:
                _color = Color.green;
                break;
        }

        return _color; 
    }

    private void TintStones()
    {
        _image.color = SetRandomColor();
    }

    public void OnSelected(Stone stone)
    {
        OnStoneSelected?.Invoke(stone);
    }

    public void OnMatchTree()
    {
        _image.color = Color.grey;
        _color = Color.grey;
        _button.enabled = false;
    }


}
