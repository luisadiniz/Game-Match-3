using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private BoardViewHandler _boardView;

    int width = 7;
    int height = 5;
    private List<List<int>> _board;

    private List<List<int>> _grid = new List<List<int>>
    {
        new List<int> { 1, 2, 3, 4, 5, 4, 3 },
        new List<int> { 5, 4, 2, 2, 0, 0, 2 },
        new List<int> { 3, 2, 2, 0, 1, 3, 1 },
        new List<int> { 0, 2, 3, 2, 3, 5, 1 },
        new List<int> { 4, 3, 2, 1, 5, 1, 1 }
    };

    private List<Vector2> _initialMatchedStonePosition = new List<Vector2>();
    private List<Vector2> _finalMatchedStonePosition = new List<Vector2>();

    private void Start()
    {
        _board = new List<List<int>>();
        _boardView.PopulateBoard(CreateBoard());

        PrintGrid("Inicial");

        SwapStones(3,2 , 4,2);

        PrintGrid("Swaped");

        //CheckCombinations();
    }

    private void Update()
    {
        CheckCombinations();
    }

    public List<List<int>> CreateBoard()
    {
        for (int i = 0; i < width; i++)
        {
            _board.Add(new List<int>());

            for (int j = 0; j < height; j++)
            {
                _board[i].Add(Random.Range(0,5));
            }
        }

        string grid = "";
        for (int i = 0; i < _board.Count; i++)
        {
            for (int j = 0; j < _board[i].Count; j++)
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

        return _board;
    }

    private void SwapStones(int i1, int j1, int i2, int j2)
    {
        //Debug.Log("First Stone Before Swap: " + _board[i1][j1]);
        //Debug.Log("Second Stone Before Swap: " + _allStones[i1][j2]);

        //int tempPosition = _board[i1][j1];

        //_board[i1][j1] = _board[i1][j2];
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

    private List<Vector2> GetCombinations()
    {
        List<Vector2> matchedStones = new List<Vector2>();
        List<Vector2> allMatchedStones = new List<Vector2>();


        for (int i = 0; i < _grid.Count; i++)
        {
            for (int j = 0; j < _grid[i].Count; j++)
            {
                if ((j + 1) < _grid[i].Count && _grid[i][j] == _grid[i][j + 1])
                {
                    matchedStones.Add(new Vector2(i, j));
                    matchedStones.Add(new Vector2(i, j + 1));
                }
                else
                {
                    if (matchedStones.Count > 2)
                    {
                        for (int y = 0; y < matchedStones.Count; y++)
                        {
                            int gridX = (int)matchedStones[y].x;
                            int gridY = (int)matchedStones[y].y;

                            allMatchedStones.Add(new Vector2(gridX, gridY));
                        }
                    }
                    matchedStones.Clear();
                }
            }

        }

        for (int j = 0; j < width; j++)
        {
            for (int i = 0; i < _grid.Count; i++)
            {
                if ((i + 1) < _grid.Count && _grid[i][j] == _grid[i + 1][j])
                {
                    matchedStones.Add(new Vector2(i, j));
                    matchedStones.Add(new Vector2(i + 1, j));
                }
                else
                {
                    if (matchedStones.Count > 2)
                    {
                        for (int y = 0; y < matchedStones.Count; y++)
                        {
                            int gridX = (int)matchedStones[y].x;
                            int gridY = (int)matchedStones[y].y;

                            allMatchedStones.Add(new Vector2(gridX, gridY));
                        }
                    }
                    matchedStones.Clear();
                }
            }
        }

        return allMatchedStones;
    }

    private void CheckCombinations()
    {
        List<Vector2> allMatchedStones = GetCombinations();

        PrintGrid("Combinations");

        if (allMatchedStones.Count > 0)
        {
            SetMatches(allMatchedStones);
            PrintGrid("Set Matches");

            GetInitialMatchedStonesPosition();

            for (int i = 0; i < height; i++)
            {
                UpdateGridValues();
            }
            PrintGrid("Updated After Combinations");

            CreateNewStones();
            PrintGrid("With New Stones");
        }

        PrintLists();
    }

    private void SetMatches(List<Vector2> allMatchedStones)
    {
        for (int e = 0; e < allMatchedStones.Count; e++)
        {
            int gridX = (int)allMatchedStones[e].x;
            int gridY = (int)allMatchedStones[e].y;

            _grid[gridX][gridY] = -1;
        }
    }

    private void GetInitialMatchedStonesPosition()
    {
        for (int j = 0; j < width; j++)
        {
            for (int i = _grid.Count - 1; i >= 0; i--)
            {
                if (_grid[i][j] == -1)
                {
                    _initialMatchedStonePosition.Add(new Vector2(i, j));
                }
            }
        }
    }
                  
    private void UpdateGridValues()
    {
        for (int j = 0; j < width; j++)
        {
            for (int i = _grid.Count - 1; i >= 0 ; i--)
            {
                if (_grid[i][j] == -1 && (i - 1) >= 0)
                {
                    SwapStones(i, j, i - 1, j);
                }
            }
        }
    }

    private void CreateNewStones()
    {
        for (int j = 0; j < width; j++)
        {
            for (int i = 0; i < _grid.Count; i++)
            {
                if (_grid[i][j] == -1)
                {
                    _finalMatchedStonePosition.Add(new Vector2(i, j));

                    _grid[i][j] = Random.Range(0, 5);
                }
            } 
        }
    }

    private void PrintLists()
    {
        for (int i = 0; i < _initialMatchedStonePosition.Count; i++)
        {
            Debug.LogError("Inicial: " + _initialMatchedStonePosition[i]);
        }

        for (int i = 0; i < _finalMatchedStonePosition.Count; i++)
        {
            Debug.LogError("Final: " + _finalMatchedStonePosition[i]);
        }
    }
}
