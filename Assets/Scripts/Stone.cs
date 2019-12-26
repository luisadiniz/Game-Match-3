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

    private Colours _currentColour;
    private Color _color;

    private void Start()
    {
        TintStones();
    }

    public Color SetColourRange()
    {
        int sortedNumber = Random.Range(0, Colours.GetNames(typeof(Colours)).Length);
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

    void TintStones()
    {
        Image image = this.gameObject.GetComponent<Image>();
        image.color = SetColourRange();
    }



}
