using System.Collections.Generic;
using UnityEngine;

public class Core
{
    const int WIDTH = 7;
    const int HEIGHT = 5;
    private List<List<int>> _board = new List<List<int>>();
    private List<Vector2> _initialMatchedStonePosition = new List<Vector2>();
    private List<Vector2> _finalMatchedStonePosition = new List<Vector2>();

    
    public List<List<int>> CreateBoard()
    {
        for (int i = 0; i < HEIGHT; i++)
        {
            _board.Add(new List<int>());

            for (int j = 0; j < WIDTH; j++)
            {
                _board[i].Add(Random.Range(0,5));
            }
        }

        string board = "";
        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                board += $"[{_board[i][j]}]";
                if (j < WIDTH - 1)
                {
                    board += ",";
                }
            }
            board += "\n";
        }

        Debug.Log("Board : \n" + board);

        return _board;
    }

    public void SwapStones(int i1, int j1, int i2, int j2)
    {
        PrintGrid("Before Swap");

        int tempPosition = _board[i1][j1];

        _board[i1][j1] = _board[i2][j2];
        _board[i2][j2] = tempPosition;

        PrintGrid("After Swap");
    }

    public void PrintGrid(string name)
    {
        string grid = "";
        for (int i = 0; i < _board.Count; i++)
        {
            for (int j = 0; j < _board[i].Count; j++)
            {
                grid += $"[{_board[i][j]}]";
                if (j < _board[i].Count - 1)
                {
                    grid += ",";
                }
            }
            grid += "\n";
        }

        Debug.Log($"Grid {name}: \n" + grid);
    }

    public List<Vector2> GetCombinations()
    {
        List<Vector2> matchedStones = new List<Vector2>();
        List<Vector2> allMatchedStones = new List<Vector2>();


        for (int i = 0; i < _board.Count; i++)
        {
            for (int j = 0; j < _board[i].Count; j++)
            {
                if ((j + 1) < _board[i].Count && _board[i][j] == _board[i][j + 1])
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

        for (int j = 0; j < WIDTH; j++)
        {
            for (int i = 0; i < _board.Count; i++)
            {
                if ((i + 1) < _board.Count && _board[i][j] == _board[i + 1][j])
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

    public void SetMatches(List<Vector2> allMatchedStones)
    {
        for (int e = 0; e < allMatchedStones.Count; e++)
        {
            int gridX = (int)allMatchedStones[e].x;
            int gridY = (int)allMatchedStones[e].y;

            _board[gridX][gridY] = -1;
        }
    }

    public void GetInitialMatchedStonesPosition()
    {
        for (int j = 0; j < WIDTH; j++)
        {
            for (int i = _board.Count - 1; i >= 0; i--)
            {
                if (_board[i][j] == -1)
                {
                    _initialMatchedStonePosition.Add(new Vector2(i, j));
                }
            }
        }
    }
                  
    public void UpdateGridValues()
    {
        for (int j = 0; j < WIDTH; j++)
        {
            for (int i = _board.Count - 1; i >= 0 ; i--)
            {
                if (_board[i][j] == -1 && (i - 1) >= 0)
                {
                    SwapStones(i, j, i - 1, j);
                }
            }
        }
    }

    public void CreateNewStones()
    {
        for (int j = 0; j < WIDTH; j++)
        {
            for (int i = 0; i < _board.Count; i++)
            {
                if (_board[i][j] == -1)
                {
                    _finalMatchedStonePosition.Add(new Vector2(i, j));

                    _board[i][j] = Random.Range(0, 5);
                }
            } 
        }
    }

    public void PrintLists()
    {
        for (int i = 0; i < _initialMatchedStonePosition.Count; i++)
        {
            Debug.Log("Inicial: " + _initialMatchedStonePosition[i]);
        }

        for (int i = 0; i < _finalMatchedStonePosition.Count; i++)
        {
            Debug.Log("Final: " + _finalMatchedStonePosition[i]);
        }
    }
}
