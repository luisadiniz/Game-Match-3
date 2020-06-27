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
    private List<List<StoneView>>_allStones;


    private void Awake()
    {
        _selectedStones = new List<StoneView>();
        _allStones = new List<List<StoneView>>();
    }

    public void PopulateBoard(List<List<int>> board, int width, int height)
    {
        for (int i = 0; i < height; i++)
        {
            GameObject newStoneContainer = Instantiate(_stoneContainerPrefab, _board);
            _allStones.Add(new List<StoneView>());

            for (int j = 0; j < width; j++)
            {
                GameObject newStone = Instantiate(_stonePrefab, newStoneContainer.transform);

                StoneView stoneScript = newStone.GetComponent<StoneView>();
                stoneScript.SetColour(_spritesList[board[i][j]]);
                stoneScript.PosX = i;
                stoneScript.PosY = j;
                stoneScript.OnStoneSelected += OnSelectedStones;

                _allStones[i].Add(stoneScript);
            }
        }
    }

    private void OnSelectedStones(StoneView stone)
    {
        if (_selectedStones.Count == 0 || _selectedStones.Count == 1)
        {
            _selectedStones.Add(stone);
        }

        if (_selectedStones.Count == 2)
        {
            if (_selectedStones[0].PosX == _selectedStones[1].PosX && (Mathf.Abs(_selectedStones[0].PosY - _selectedStones[1].PosY) == 1) || _selectedStones[0].PosY == _selectedStones[1].PosY && (Mathf.Abs(_selectedStones[0].PosX - _selectedStones[1].PosX) == 1))
            {
                Debug.Log("Peças podem ser trocadas");
            }
            else
            {
                _selectedStones[0].OnUnselectStone(_selectedStones[0]);

                _selectedStones[0] = _selectedStones[1];
                _selectedStones.Remove(_selectedStones[1]);

                Debug.Log("Peças não podem ser trocadas");
            }
        }
    }
}
