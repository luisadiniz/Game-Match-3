using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoardViewHandler : MonoBehaviour
{
    public Action<int, int, int, int> OnStonesSelected;

    [SerializeField] private List<Sprite> _spritesList;
    [SerializeField] private GameObject _stonePrefab;
    [SerializeField] private GameObject _stoneContainerPrefab;
    [SerializeField] private Transform _board;

    private List<List<StoneView>> _boardViewList;
    private List<StoneView> _selectedStones;

    private void Awake()
    {
        _selectedStones = new List<StoneView>();
        _boardViewList = new List<List<StoneView>>();
    }

    public void PopulateBoard(List<List<int>> board)
    {
        for (int i = 0; i < board.Count; i++)
        {
            GameObject newStoneContainer = Instantiate(_stoneContainerPrefab, _board);

            List<int> row = board[i];

            _boardViewList.Add(new List<StoneView>());

            for (int j = 0; j < row.Count; j++)
            {
                GameObject newStone = Instantiate(_stonePrefab, newStoneContainer.transform);

                StoneView stoneScript = newStone.GetComponent<StoneView>();
                stoneScript.SetColour(_spritesList[board[i][j]]);
                stoneScript.PosX = i;
                stoneScript.PosY = j;
                stoneScript.OnClick += OnStoneClick;

                _boardViewList[i].Add(stoneScript);
            }
        }
    }

    public void OnStoneClick(StoneView stone)
    {
        if (_selectedStones.Count == 0 || _selectedStones.Count == 1)
        {
            _selectedStones.Add(stone);
        }

        if (_selectedStones.Count == 2)
        {
            if (_selectedStones[0].PosX == _selectedStones[1].PosX && (Mathf.Abs(_selectedStones[0].PosY - _selectedStones[1].PosY) == 1) || _selectedStones[0].PosY == _selectedStones[1].PosY && (Mathf.Abs(_selectedStones[0].PosX - _selectedStones[1].PosX) == 1))
            {
                OnStonesSelected?.Invoke(_selectedStones[0].PosX, _selectedStones[0].PosY, _selectedStones[1].PosX, _selectedStones[1].PosY);
                
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

    public void DestroyMatchedStones(List<Vector2> allMatchedStones)
    {
        for (int i = 0; i < _boardViewList.Count; i++)
        {
            for (int j = 0; j < _boardViewList[i].Count; j++)
            {
                for (int a = 0; a < allMatchedStones.Count; a++)
                {
                    Image stoneImage = _boardViewList[(int)allMatchedStones[a].x][(int)allMatchedStones[a].y].gameObject.GetComponent<Image>();
                    stoneImage.enabled = false;
                }
            }
        }
    }
}
