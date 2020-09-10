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

        //todo: checar combinações após o board ser criado

        _boardView.OnStonesSelected += OnStonesSelected;
    }

    private void OnStonesSelected(int i1, int j1, int i2, int j2)
    {
        _core.SwapStones(i1, j1, i2, j2);
        _boardView.DoSwap();

        CheckCombinations(_core.GetCombinations());
    }

    public void CheckCombinations(List<Vector2> allMatchedStones)
    {
        _core.PrintGrid("Combinations");

        if (allMatchedStones.Count > 0)
        {
            //seta os matches para -1
            _core.SetMatches(allMatchedStones);
            _core.PrintGrid("Set Matches");

            // pega a posição inicial das pedras e armazena numa lista
            _core.GetInitialMatchedStonesPosition();

            for (int i = 0; i < 5; i++)
            {
                _core.UpdateGridValues();
            }
            _core.PrintGrid("Updated After Combinations");

            _core.CreateNewStones();
            _core.PrintGrid("With New Stones");
        }
    }


}
