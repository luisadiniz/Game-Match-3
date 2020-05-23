using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    int width = 7;
    int height = 5;
    //private List<List<int>> _allStones;

    private List<List<int>> _grid = new List<List<int>>
    {
        new List<int> { 1, 2, 3, 4, 5, 4, 3 },
        new List<int> { 5, 4, 2, 2, 1, 0, 1 },
        new List<int> { 3, 2, 2, 0, 1, 2, 3 },
        new List<int> { 0, 2, 3, 2, 4, 5, 4 },
        new List<int> { 4, 3, 2, 1, 0, 1, 2 }
    };

    private void Start()
    {
        //_allStones = new List<List<int>>();

        PrintGrid("Inicial");

        SwapStones(3,2 , 4,2);

        PrintGrid("Swaped");

        CheckCombinations();
        PrintGrid("Combinations");

        UpdateGridValues();
        PrintGrid("Updated Grid After Combinations");
    }

    //private void CreateBoard()
    //{
    //    for (int i = 0; i < width; i++)
    //    {
    //        _allStones.Add(new List<int>());

    //        for (int j = 0; j < height; j++)
    //        {
    //            _allStones[i].Add(Random.Range(0,4));
    //        }
    //    }
    //}

    private void SwapStones(int i1, int j1, int i2, int j2)
    {
        //Debug.Log("First Stone Before Swap: " + _allStones[i1][j1]);
        //Debug.Log("Second Stone Before Swap: " + _allStones[i1][j2]);

        //int tempPosition = _allStones[i1][j1];

        //_allStones[i1][j1] = _allStones[i1][j2];
        //_allStones[i1][j2] = tempPosition;

        //Debug.Log("First Stone After Swap: " + _allStones[i1][j1]);
        //Debug.Log("Second Stone After Swap: " + _allStones[i1][j2]);

        int tempPosition = _grid[i1][j1];

        _grid[i1][j1] = _grid[i2][j2];
        _grid[i2][j2] = tempPosition;
    }

    private void PrintGrid(string name)
    {
        string grid = "";
        for (int i = 0; i < _grid.Count; i++)
        {
            for (int j = 0; j < _grid[i].Count; j++)
            {
                grid += $"[{_grid[i][j]}]";
                if (j < _grid[i].Count - 1)
                {
                    grid += ",";
                }
            }
            grid += "\n";
        }

        Debug.Log($"Grid {name}: \n" + grid);
    }

    private void CheckCombinations()
    {
        List<Vector2> _matchedStones = new List<Vector2>();
        List<Vector2> _allMatchedStones = new List<Vector2>();

        for (int i = 0; i < _grid.Count; i++)
        {
            for (int j = 0; j < _grid[i].Count; j++)
            {
                if ((j + 1) < _grid[i].Count && _grid[i][j] == _grid[i][j + 1])
                {
                    _matchedStones.Add(new Vector2(i, j));
                    _matchedStones.Add(new Vector2(i, j + 1));
                }
                else
                {
                    if (_matchedStones.Count > 2)
                    {
                        for (int y = 0; y < _matchedStones.Count; y++)
                        {
                            int gridX = (int)_matchedStones[y].x;
                            int gridY = (int)_matchedStones[y].y;

                            _allMatchedStones.Add(new Vector2(gridX, gridY));
                        }
                    }
                    _matchedStones.Clear();
                }
            }

        }

        for (int j = 0; j < width; j++)
        {
            for (int i = 0; i < _grid.Count; i++)
            {
                if ((i + 1) < _grid.Count && _grid[i][j] == _grid[i+1][j])
                {
                    _matchedStones.Add(new Vector2(i, j));
                    _matchedStones.Add(new Vector2(i + 1, j));
                }
                else
                {
                    if (_matchedStones.Count > 2)
                    {
                        for (int y = 0; y < _matchedStones.Count; y++)
                        {
                            int gridX = (int)_matchedStones[y].x;
                            int gridY = (int)_matchedStones[y].y;

                            _allMatchedStones.Add(new Vector2(gridX, gridY));
                        }
                    }
                    _matchedStones.Clear();
                }
            }
        }

        for (int e = 0; e < _allMatchedStones.Count; e++)
        {
            int gridX = (int)_allMatchedStones[e].x;
            int gridY = (int)_allMatchedStones[e].y;

            _grid[gridX][gridY] = -1;

        }

    }

    private void UpdateGridValues()
    {
        for (int j = 0; j < width; j++)
        {
            for (int i = _grid.Count; i >= 0 ; i--)
            {
                if (_grid[i][j] == -1 && (i - 1) >= 0 && _grid[i - 1][j] != -1)
                {
                    SwapStones(i, j, i - 1, j);
                }
            }
        }
    }

}
