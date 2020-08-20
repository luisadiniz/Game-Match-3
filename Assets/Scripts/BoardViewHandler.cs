using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoardViewHandler : MonoBehaviour
{
    public Action<int, int, int, int> OnSwap;

    [SerializeField] private List<Sprite> _spritesList;
    [SerializeField] private GameObject _stonePrefab;
    [SerializeField] private GameObject _stoneContainerPrefab;
    [SerializeField] private Transform _board;

    private List<StoneView> _selectedStones;

    private void Awake()
    {
        _selectedStones = new List<StoneView>();
    }

    public void PopulateBoard(List<List<int>> board)
    {
        for (int i = 0; i < board.Count; i++)
        {
            GameObject newStoneContainer = Instantiate(_stoneContainerPrefab, _board);

            List<int> row = board[i];

            for (int j = 0; j < row.Count; j++)
            {
                GameObject newStone = Instantiate(_stonePrefab, newStoneContainer.transform);

                StoneView stoneScript = newStone.GetComponent<StoneView>();
                stoneScript.SetColour(_spritesList[board[i][j]]);
                stoneScript.PosX = i;
                stoneScript.PosY = j;
                stoneScript.OnStoneSelected += OnSelectedStones;
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

                OnSwap.Invoke(_selectedStones[0].PosX, _selectedStones[0].PosY, _selectedStones[1].PosX, _selectedStones[1].PosY);
                DoSwap();
                ClearSelectedStones();
            }
            else
            {
                _selectedStones[0].OnUnselectStone();

                _selectedStones[0] = _selectedStones[1];
                _selectedStones.Remove(_selectedStones[1]);
            }
        }
        else if(_selectedStones.Count > 2)
        {
            ClearSelectedStones();
        }
    }

    public void DoSwap()
    {
        Vector3 firstStonePosition = new Vector3(_selectedStones[1].transform.position.x, _selectedStones[1].transform.position.y);
        _selectedStones[0].transform.DOMove(firstStonePosition, 0.5f, false);

        Vector3 secondStonePosition = new Vector3(_selectedStones[0].transform.position.x, _selectedStones[0].transform.position.y);
        _selectedStones[1].transform.DOMove(secondStonePosition, 0.5f, false);

        int tempPosX = _selectedStones[0].PosX;
        int tempPosY = _selectedStones[0].PosY;
        _selectedStones[0].PosX = _selectedStones[1].PosX;
        _selectedStones[0].PosY = _selectedStones[1].PosY;

        _selectedStones[1].PosX = tempPosX;
        _selectedStones[1].PosY = tempPosY;
    }

    private void ClearSelectedStones()
    {
        for (int i = 0; i < _selectedStones.Count; i++)
        {
            _selectedStones[i].OnUnselectStone();
        }
        _selectedStones.Clear();
    }
}
