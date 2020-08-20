using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Core _core;
    private BoardViewHandler _boardView;
    private 

    void Start()
    {
        _core = new Core();

        List<List<int>> board = _core.CreateBoard();
        _boardView.PopulateBoard(board);
    }

    void Update()
    {
        
    }
}
