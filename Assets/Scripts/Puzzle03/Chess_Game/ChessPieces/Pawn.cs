using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPieces
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPieces[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> nV = new List<Vector2Int>();

        int direction = (team == 0) ? 1 : -1;

        if (board[currentX, currentY + direction] == null)
        {
            nV.Add(new Vector2Int(currentX, currentY + direction));
        }

        if (board[currentX, currentY + direction] == null)
        {
            if (team == 0 && currentY == 1 && board[currentX, currentY + direction * 2] == null)
            {
                nV.Add(new Vector2Int(currentX, currentY + (direction * 2)));
            }
        }

        if(currentX != tileCountX - 1)
        {
            if (board[currentX + 1, currentY + direction] != null && board[currentX + 1, currentY + direction].team != team )
            {
                nV.Add(new Vector2Int(currentX + 1, currentY + direction));
            }
        }
        if (currentX != 0)
        {
            if (board[currentX - 1, currentY + direction] != null && board[currentX - 1, currentY + direction].team != team)
            {
                nV.Add(new Vector2Int(currentX - 1, currentY + direction));
            }

        }

            return nV;
    }
}
