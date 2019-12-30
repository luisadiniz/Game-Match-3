﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Board : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private GameObject _stonePrefab;
    [SerializeField] private Transform _board;
    [SerializeField] private int _distanceBetweenStones;

    private List<Stone> _selectedStones;

    private void Start()
    {
        _stonesList = new List<GameObject>();
        _selectedStones = new List<Stone>();
        CreateBoard();
    }

    private void Update()
    {
        
    }

    private void CreateBoard()
    {
       float newStoneWidth = _stonePrefab.GetComponent<RectTransform>().rect.width;
       float newStoneHeight = _stonePrefab.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 position = new Vector3(i * (newStoneWidth + _distanceBetweenStones), j * (newStoneHeight + _distanceBetweenStones));
                position = _board.transform.localPosition + position;
                GameObject newStone = Instantiate(_stonePrefab, position, Quaternion.identity, _board);

                Stone stoneScript = newStone.GetComponent<Stone>();
                stoneScript.PosX = i;
                stoneScript.PosY = j;
                stoneScript.OnStoneSelected += OnSelectedStone;
            }
        }
    }

    private void OnSelectedStone(Stone stone)
    {
        if (_selectedStones.Count == 0 || _selectedStones.Count == 1)
        {
            _selectedStones.Add(stone);
        }
        else
        {
            _selectedStones.Clear();
            _selectedStones.Add(stone);
        }

        if (_selectedStones.Count == 2)
        {
            CompareSelectedStones();
        }


    }

    private void CompareSelectedStones()
    {

        if (_selectedStones[0].PosX == _selectedStones[1].PosX && ((_selectedStones[0].PosY == _selectedStones[1].PosY + 1) || (_selectedStones[0].PosY == _selectedStones[1].PosY - 1)))
        {
            Debug.Log("Peças podem ser trocadas");

            SwapStones();
        }

        else if (_selectedStones[0].PosY == _selectedStones[1].PosY && ((_selectedStones[0].PosX == (_selectedStones[1].PosX + 1)) || (_selectedStones[0].PosX == (_selectedStones[1].PosX - 1))))
        {
            Debug.Log("Peças podem ser trocadas");

            SwapStones();
        }

        else
        {
            Debug.Log("Peças não podem ser trocadas");
        }

    }

    private void SwapStones()
    {
        Vector3 firstStonePosition = new Vector3(_selectedStones[1].transform.position.x, _selectedStones[1].transform.position.y);
        _selectedStones[0].transform.DOMove(firstStonePosition, 0.5f, false);

        Vector3 secondStonePosition = new Vector3(_selectedStones[0].transform.position.x, _selectedStones[0].transform.position.y);
        _selectedStones[1].transform.DOMove(secondStonePosition, 0.5f, false);

        _selectedStones[0].PosX = _selectedStones[1].PosX;
        _selectedStones[0].PosY = _selectedStones[1].PosY;
    }

    private void CheckCombinations()
    {
        
    }
}
