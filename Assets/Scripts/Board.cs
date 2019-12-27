using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private List<GameObject> _stonesList;
    [SerializeField] private GameObject _stonePrefab;
    [SerializeField] private Transform _board;
    [SerializeField] private int _distanceBetweenStones;

    void Start()
    {
        _stonesList = new List<GameObject>();
        CreateBoard();
    }

    void CreateBoard()
    {
       float newStoneWidth = _stonePrefab.GetComponent<RectTransform>().rect.width;
       float newStoneHeight = _stonePrefab.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 position = new Vector3(i * (newStoneWidth + _distanceBetweenStones), j * (newStoneHeight + _distanceBetweenStones));

                position = _board.transform.localPosition + position;

                Instantiate(_stonePrefab, position, Quaternion.identity, _board);
            }
        }
    }

}
