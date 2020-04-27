using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    int width = 7;
    int height = 5;
    //private List<List<int>> _allStones;

    private List<List<int>> _grid = new List<List<int>>
    {
        new List<int> { 3, 3, 3, 2, 5, 5, 5 },
        new List<int> { 2, 2, 1, 1, 0, 1, 0 },
        new List<int> { 2, 0, 1, 1, 3, 1, 0 },
        new List<int> { 1, 2, 3, 3, 3, 1, 1 },
        new List<int> { 2, 2, 2, 1, 2, 3, 0 }
    };

    private void Start()
    {
        //_allStones = new List<List<int>>();

        PrintGrid("Inicial");

        SwapStones(2,0 , 2,1);

        PrintGrid("Swaped");

        CheckCombinations();
        PrintGrid("Combinations");
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
                            _grid[gridX][gridY] = -1;
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
                            _grid[gridX][gridY] = -1;
                        }
                    }
                    _matchedStones.Clear();
                }
            }
        }

    }

}
