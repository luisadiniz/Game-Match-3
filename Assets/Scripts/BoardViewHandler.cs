using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewHandler : MonoBehaviour
{
    [SerializeField] private List<Sprite> _spritesList;

    private void Start()
    {

    }

    public void PopulateBoard(List<List<int>> board)
    {
        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Count; j++)
            {

            }
        }
    }
}
