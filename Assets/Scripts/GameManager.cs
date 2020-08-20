using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private BoardViewHandler _boardView;
    private Core _core;

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
