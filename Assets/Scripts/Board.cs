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
    private List<Stone> _matchedStones;
    private string _swapDirection;

    private void Start()
    {
        _allStones = new List<List<Stone>>();
        _selectedStones = new List<Stone>();
        _matchedStones = new List<Stone>();

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
            _selectedStones.Clear();
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

        _allStones[_selectedStones[0].PosX][_selectedStones[0].PosY] = _selectedStones[0];
        _allStones[_selectedStones[1].PosX][_selectedStones[1].PosY] = _selectedStones[1];
    }

    private void CheckCombinations()
    {

        if (_swapDirection == "horizontal")
        {
            HandleHorizontalCombinations(0);
            HandleHorizontalCombinations(1);
        }
        else if (_swapDirection == "vertical")
        {
            HandleVerticalCombinations(0);
            HandleVerticalCombinations(1);
        }

        else
        {
            //devolver peças pro lugar original
        }

        _selectedStones.Clear();
    }

    private void HandleMatchedStones(int selectedIndex)
    {
        if (!_matchedStones.Contains(_selectedStones[selectedIndex]))
        {
           _matchedStones.Add(_selectedStones[selectedIndex]);
        }

        if(_matchedStones.Count >= 3)
        for (int i = 0; i < _matchedStones.Count; i++)
            {
                _matchedStones[i].OnMatchTree();
        }

        _matchedStones = new List<Stone>();

    }

    private void HandleHorizontalCombinations(int selectedindex)
    {
        int index = 1;
        while (_selectedStones[selectedindex].PosY + index < _allStones[_selectedStones[selectedindex].PosX].Count && _selectedStones[selectedindex].Color == _allStones[_selectedStones[selectedindex].PosX][_selectedStones[selectedindex].PosY + index].Color)
        {
            _matchedStones.Add(_allStones[_selectedStones[selectedindex].PosX][_selectedStones[selectedindex].PosY + index]);
            index++;
        }

        index = -1;
        while (_selectedStones[selectedindex].PosY + index >= 0 && _selectedStones[selectedindex].Color == _allStones[_selectedStones[selectedindex].PosX][_selectedStones[selectedindex].PosY + index].Color)
        {
            _matchedStones.Add(_allStones[_selectedStones[selectedindex].PosX][_selectedStones[selectedindex].PosY + index]);
            index--;
        }
        HandleMatchedStones(selectedindex);
    }

    private void HandleVerticalCombinations(int selectedIndex)
    {
        int index = 1;
        while (_selectedStones[selectedIndex].PosX + index < _allStones[_selectedStones[selectedIndex].PosX].Count && _selectedStones[selectedIndex].Color == _allStones[_selectedStones[selectedIndex].PosX + index][_selectedStones[selectedIndex].PosY].Color)
        {
            _matchedStones.Add(_allStones[_selectedStones[0].PosX + index][_selectedStones[selectedIndex].PosY]);
            index++;
        }

        index = -1;
        while (_selectedStones[selectedIndex].PosX + index >= 0 && _selectedStones[selectedIndex].Color == _allStones[_selectedStones[selectedIndex].PosX + index][_selectedStones[selectedIndex].PosY].Color)
        {
            _matchedStones.Add(_allStones[_selectedStones[selectedIndex].PosX + index][_selectedStones[selectedIndex].PosY]);
            index--;
        }
        HandleMatchedStones(selectedIndex);
    }

}
