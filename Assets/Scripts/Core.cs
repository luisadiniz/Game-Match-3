using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    int width = 5;
    int height = 5;
    //private List<List<int>> _allStones;

    private List<int> _grid = new List<int>
    {
        3, 0, 3, 1, 0,
        2, 0, 1, 0, 1,
        0, 2, 1, 3, 1,
        2, 2, 3, 3, 0,
        2, 3, 2, 0, 3
    };

    private void Start()
    {
        //_allStones = new List<List<int>>();

        PrintGrid("Inicial");
        SwapStones();

        PrintGrid("Swaped");
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

    private void SwapStones()
    {
        //Debug.Log("First Stone Before Swap: " + _allStones[i1][j1]);
        //Debug.Log("Second Stone Before Swap: " + _allStones[i1][j2]);

        //int tempPosition = _allStones[i1][j1];

        //_allStones[i1][j1] = _allStones[i1][j2];
        //_allStones[i1][j2] = tempPosition;

        //Debug.Log("First Stone After Swap: " + _allStones[i1][j1]);
        //Debug.Log("Second Stone After Swap: " + _allStones[i1][j2]);

        int tempPosition = _grid[10];

        _grid[10] = _grid[11];
        _grid[11] = tempPosition;
    }

    private void PrintGrid(string name)
    {
        string grid = "";
        //for (int i = 0; i < _allStones.Count; i++)
        //{
        //    for (int j = 0; j < _allStones[i].Count; j++)
        //    {
        //        grid += $"[{_allStones[i][j]}]";
        //        if (j < _allStones[i].Count - 1)
        //        {
        //            grid += ",";
        //        }
        //    }
        //    grid += "\n";
        //}

        for (int i = 0; i < _grid.Count; i++)
        {
            grid += $"[{_grid[i]}], ";
            if((i+1)% width == 0)
            {
                grid += "\n";
            }
        }

        Debug.Log($"Grid {name}: \n" + grid);
    }
}
