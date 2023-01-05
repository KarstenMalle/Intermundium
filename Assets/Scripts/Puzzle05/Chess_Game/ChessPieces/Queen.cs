using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPieces
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPieces[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> nV = new List<Vector2Int>();

        //Rook going down
        for (int i = currentY - 1; i >= 0; i--)
        {
            if (board[currentX, i] == null)
            {
                nV.Add(new Vector2Int(currentX, i));
            }

            if (board[currentX, i] != null)
            {
                if (board[currentX, i].team != team)
                    nV.Add(new Vector2Int(currentX, i));

                break;
            }
        }

        //Rook going up
        for (int i = currentY + 1; i < tileCountY; i++)
        {
            if (board[currentX, i] == null)
            {
                nV.Add(new Vector2Int(currentX, i));
            }

            if (board[currentX, i] != null)
            {
                if (board[currentX, i].team != team)
                    nV.Add(new Vector2Int(currentX, i));

                break;
            }
        }

        //Rook going left
        for (int i = currentX - 1; i >= 0; i--)
        {
            if (board[currentY, i] == null)
            {
                nV.Add(new Vector2Int(i, currentY));
            }

            if (board[currentY, i] != null)
            {
                if (board[currentY, i].team != team)
                    nV.Add(new Vector2Int(i, currentY));

                break;
            }
        }

        //Rook going Right
        for (int i = currentX + 1; i < tileCountX; i++)
        {
            if (board[currentY, i] == null)
            {
                nV.Add(new Vector2Int(i, currentY));
            }

            if (board[currentY, i] != null)
            {
                if (board[currentY, i].team != team)
                    nV.Add(new Vector2Int(i, currentY));

                break;
            }
        }

        //top right
        for (int x = currentX + 1, y = currentY + 1; x < tileCountX && y < tileCountY; x++, y++)
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
        for (int x = currentX + 1, y = currentY - 1; x < tileCountX && y >= 0; x++, y--)
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