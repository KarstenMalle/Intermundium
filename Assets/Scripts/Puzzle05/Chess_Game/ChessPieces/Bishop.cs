using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPieces
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPieces[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> nV = new List<Vector2Int>();


        //top right
        for (int x = currentX + 1, y = currentY + 1; x < tileCountX && y < tileCountY; x++, y++)
        {
            if (board[x,y] == null)
            {
                nV.Add(new Vector2Int(x, y));
            }
            else
            {
                if(board[x, y] != null)
                {
                    nV.Add(new Vector2Int(x, y));

                    break;
                }
            }
        }

        //top left
        for (int x = currentX - 1, y = currentY + 1; x >= 0 && y < tileCountY; x--, y++)
        {
            if (board[x, y] == null)
            {
                nV.Add(new Vector2Int(x, y));
            }
            else
            {
                if (board[x, y] != null)
                {
                    nV.Add(new Vector2Int(x, y));

                    break;
                }
            }
        }

        //bottom right
        for (int x = currentX + 1, y = currentY - 1; x < tileCountX &&  y >= 0  ; x++, y--)
        {
            if (board[x, y] == null)
            {
                nV.Add(new Vector2Int(x, y));
            }
            else
            {
                if (board[x, y] != null)
                {
                    nV.Add(new Vector2Int(x, y));

                    break;
                }
            }
        }

        //bottom left
        for (int x = currentX - 1, y = currentY - 1; x >= 0 && y >= 0; x--, y--)
        {
            if (board[x, y] == null)
            {
                nV.Add(new Vector2Int(x, y));
            }
            else
            {
                if (board[x, y] != null)
                {
                    nV.Add(new Vector2Int(x, y));

                    break;
                }
            }
        }

        return nV;
    }

}