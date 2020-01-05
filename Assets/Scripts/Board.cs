using System.Collections;
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

    private List<List<Stone>> _allStones;
    private List<Stone> _selectedStones;
    private string _swapDirection;

    private void Start()
    {
        _allStones = new List<List<Stone>>();
        _selectedStones = new List<Stone>();
        CreateBoard();
    }

    private void CreateBoard()
    {
       float newStoneWidth = _stonePrefab.GetComponent<RectTransform>().rect.width;
       float newStoneHeight = _stonePrefab.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < width; i++)
        {
            _allStones.Add(new List<Stone>());

            for (int j = 0; j < height; j++)
            {
                Vector3 position = new Vector3(i * (newStoneWidth + _distanceBetweenStones), j * (newStoneHeight + _distanceBetweenStones));
                position = _board.transform.localPosition + position;
                GameObject newStone = Instantiate(_stonePrefab, position, Quaternion.identity, _board);

                Stone stoneScript = newStone.GetComponent<Stone>();
                _allStones[i].Add(stoneScript);

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
        
        if (_selectedStones.Count == 2)
        {
            CompareSelectedStones();
        }
    }

    private void CompareSelectedStones()
    {

        if (_selectedStones[0].PosX == _selectedStones[1].PosX && (Mathf.Abs(_selectedStones[0].PosY - _selectedStones[1].PosY) == 1))
        {
            Debug.Log("Peças podem ser trocadas");
            _swapDirection = "vertical";
            SwapStones();
            CheckCombinations();
        }

        else if (_selectedStones[0].PosY == _selectedStones[1].PosY && (Mathf.Abs(_selectedStones[0].PosX - _selectedStones[1].PosX) == 1))
        {
            Debug.Log("Peças podem ser trocadas");
            _swapDirection = "horizontal";
            SwapStones();
            CheckCombinations();
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

        int tempPosX = _selectedStones[0].PosX;
        int tempPosY = _selectedStones[0].PosY;
        _selectedStones[0].PosX = _selectedStones[1].PosX;
        _selectedStones[0].PosY = _selectedStones[1].PosY;

        _selectedStones[1].PosX = tempPosX;
        _selectedStones[1].PosY = tempPosY;
    }

    private void CheckCombinations()
    {
        Debug.Log("First: " + _allStones[_selectedStones[0].PosX][_selectedStones[0].PosY]);
        Debug.Log("Second: " + _allStones[_selectedStones[0].PosX][_selectedStones[0].PosY]);

        List<Stone> _matchedStones = new List<Stone>();

        if (_swapDirection == "horizontal")
        {
            int index = 1;

            while (_selectedStones[0].PosY + index < _allStones[_selectedStones[0].PosX].Count && _selectedStones[0].Color == _allStones[_selectedStones[0].PosX][_selectedStones[0].PosY + index].Color)
            {
                _matchedStones.Add(_allStones[_selectedStones[0].PosX][_selectedStones[0].PosY + index]);

                if (!_matchedStones.Contains(_allStones[_selectedStones[1].PosX][_selectedStones[1].PosY]))
                {
                    _matchedStones.Add(_allStones[_selectedStones[1].PosX][_selectedStones[1].PosY]);
                }

                for (int i = 0; i < _matchedStones.Count; i++)
                {
                    _matchedStones[i].OnMatchTree();
                }

                index++;
            }

            index = -1;

            while (_selectedStones[0].PosY + index >= 0 && _selectedStones[0].Color == _allStones[_selectedStones[0].PosX][_selectedStones[0].PosY + index].Color)
            {
                _matchedStones.Add(_allStones[_selectedStones[0].PosX][_selectedStones[0].PosY + index]);

                if (!_matchedStones.Contains(_allStones[_selectedStones[1].PosX][_selectedStones[1].PosY]))
                {
                    _matchedStones.Add(_allStones[_selectedStones[1].PosX][_selectedStones[1].PosY]);
                }

                for (int i = 0; i < _matchedStones.Count; i++)
                {
                    _matchedStones[i].OnMatchTree();
                }

                index--;
            }
        }
        else if (_swapDirection == "vertical")
        {
            int index = 1;

            while (_selectedStones[0].PosX + index < _allStones[_selectedStones[0].PosX].Count && _selectedStones[0].Color == _allStones[_selectedStones[0].PosX + index][_selectedStones[0].PosY].Color)
            {
                _matchedStones.Add(_allStones[_selectedStones[0].PosX + index][_selectedStones[0].PosY]);

                if (!_matchedStones.Contains(_allStones[_selectedStones[1].PosX][_selectedStones[1].PosY]))
                {
                    _matchedStones.Add(_allStones[_selectedStones[1].PosX][_selectedStones[1].PosY]);
                }

                for (int i = 0; i < _matchedStones.Count; i++)
                {
                    _matchedStones[i].OnMatchTree();
                }

                index++;
            }

            index = -1;

            while (_selectedStones[0].PosX + index >= 0 && _selectedStones[0].Color == _allStones[_selectedStones[1].PosX + index][_selectedStones[1].PosY].Color)
            {
                _matchedStones.Add(_allStones[_selectedStones[1].PosX + index][_selectedStones[1].PosY]);

                if (!_matchedStones.Contains(_allStones[_selectedStones[1].PosX][_selectedStones[1].PosY]))
                {
                    _matchedStones.Add(_allStones[_selectedStones[1].PosX][_selectedStones[1].PosY]);
                }

                for (int i = 0; i < _matchedStones.Count; i++)
                {
                    _matchedStones[i].OnMatchTree();
                }

                index--;
            }
        }
        else
        {
            //devolver peças pro lugar original
        }

        _selectedStones.Clear();

    }

}
