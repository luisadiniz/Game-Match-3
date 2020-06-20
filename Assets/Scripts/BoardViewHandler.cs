using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewHandler : MonoBehaviour
{
    [SerializeField] private List<Sprite> _spritesList;
    [SerializeField] private GameObject _stonePrefab;
    [SerializeField] private GameObject _stoneContainerPrefab;
    [SerializeField] private Transform _board;

    private List<StoneView> _selectedStones;

    private void Start()
    {
        _selectedStones = new List<StoneView>();
    }

    public void PopulateBoard(List<List<int>> board, int width, int height)
    {
        for (int i = 0; i < height; i++)
        {
            GameObject newStoneContainer = Instantiate(_stoneContainerPrefab, _board);

            for (int j = 0; j < width; j++)
            {
                GameObject newStone = Instantiate(_stonePrefab, newStoneContainer.transform);

                StoneView stoneScript = newStone.GetComponent<StoneView>();
                stoneScript.SetColour(_spritesList[board[i][j]]);
                stoneScript.OnStoneSelected += OnSelectedStones;
            }
        }
    }

    private void OnSelectedStones(StoneView stone)
    {
        if (_selectedStones.Count == 0)
        {
            _selectedStones.Add(stone);
        }
        else if (_selectedStones.Count == 1)
        {
            // checar se as pedras sao adjascentes
        }
    }
}
