using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class Puzzle3Manager : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera ChessCamera;
    [SerializeField] private VideoPlayer VidPlayer;
    
    [SerializeField] private VideoClip[] PuzzleClips;
    [SerializeField] private GameObject ChessManager;
    [SerializeField] private float CameraTransitionSpeed;
    
    private ChessBoardManager chessBoardManager;
    private bool PlayingChess = false;
    ChessPieces[,] chessboard;
    private bool isItSolved = false;
    private float tmp_sensitivity;

    //5 solution positions, 2 values x and y, with both a true or false value to confirm solution.
    private int[,,] Solutions = new int[5, 2, 3]
    {
        { { 0, 1, 0}, { 3, 1, 0} }, //(x, chesspiece type ,true/false) (y, chesspiece type, true/false)
        { { 1, 1, 0}, { 3, 1, 0} }, //(x, chesspiece type, true/false) (y, chesspiece type, true/false)
        { { 2, 1, 0}, { 3, 1, 0} }, //(x, chesspiece type, true/false) (y, chesspiece type, true/false)
        { { 3, 1, 0}, { 3, 1, 0} }, //(x, chesspiece type, true/false) (y, chesspiece type, true/false)
        { { 4, 1, 0}, { 3, 1, 0} }  //(x, chesspiece type, true/false) (y, chesspiece type, true/false)
    };

    private int tmp_currX, tmp_currY;

    private void Awake()
    {

        chessBoardManager = ChessManager.GetComponent<ChessBoardManager>();
        VidPlayer.isLooping = true;

        //chessBoard = chessBoardManager.GetCurrentChessPiecePositions();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ChessCamera == null)
        {
            Debug.LogError("Main Camera Not Connected");
        }

        chessboard = chessBoardManager.chessPieces; 

        RaycastHit raycastHit;
        Ray Raycast = ChessCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(Raycast, out raycastHit, 10, LayerMask.GetMask("ChessPuzzle"))){

            if (PlayingChess == false && Input.GetKeyDown(KeyCode.E))
            {
                PlayingChess = !PlayingChess;

                //Debug.Log("Start Playing chess");

                Pause();

                MainCamera.enabled = false;
                chessBoardManager.ChessCamera.enabled = true;

                chessBoardManager.SetEnableMoves(true);
            }else if(Input.GetKeyDown(KeyCode.E) && PlayingChess == true)
            {
                PlayingChess = false;
                //Debug.Log("Stop Playing Chess");
                chessBoardManager.SetEnableMoves(false);
                MainCamera.enabled = true;
                chessBoardManager.ChessCamera.enabled = false;
            
                Resume();
            }
        }

        checkSolution();
        ChangeVideo();
        MatchingSolution();

        IsItSolved();
    }

    public void Resume()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        FirstPersonLook.sensitivity = tmp_sensitivity;
    }

    public void Pause()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        tmp_sensitivity = FirstPersonLook.sensitivity;
        FirstPersonLook.sensitivity = 0f;
    }

    private void checkSolution()
    {
        for(int x = 0; x < 8; x++) //check all rows and columns
        {
            for (int y = 0; y < 8; y++)
            {
                //current values from all specific chess piece
                if (chessboard[x, y] != null)
                {
                     tmp_currX = chessboard[x, y].currentX;
                     tmp_currY = chessboard[x, y].currentY;
                }
                else
                {
                    continue;
                }

                for (int i = 0; i < 5; ++i)
                {   
                    //does this point match with any of our known solutions, correct type too?
                    if (Solutions[i, 0, 0] == x && Solutions[i, 1, 0] == y && (int)chessboard[x,y].pieceType == Solutions[i, 0, 1] )
                    {
                        //if yes
                        Solutions[i, 0, 2] = 1;
                        Solutions[i, 1, 2] = 1;
                        //print((Solutions[i, 0, 1], Solutions[i, 1, 1]));
                    }
                    else if(Solutions[i, 0, 0] != tmp_currX && Solutions[i, 1, 0] != tmp_currY && (int)chessboard[x, y].pieceType != Solutions[i, 0, 1])
                    {
                        //if no
                        //Solutions[i, 0, 2] = 0;
                        //Solutions[i, 1, 2] = 0;
                        continue;
                    }
                }
            }
        }
    }
    
    private void ChangeVideo()
    {
        if(Solutions[0, 0, 2] != 1 && Solutions[0, 1, 2] != 1)
        {
            
            //Debug.Log( (Solutions[0, 0, 2], Solutions[0, 1, 2] ));
            
            Debug.Log("Playing first video hint");
            //VidPlayer.clip = PuzzleClips[0];
            //VidPlayer.Play();
        }
        else if ( (Solutions[1, 0, 2] != 1 && Solutions[1, 1, 2] != 1) && (Solutions[0, 0, 2] == 1 && Solutions[0, 1, 2] == 1) ) //If the current is false but the previous are true
        {
            Debug.Log("Playing Second video hint");
            //VidPlayer.clip = PuzzleClips[1];
            //VidPlayer.Play();
        }
        else if ((Solutions[2, 0, 2] != 1 && Solutions[2, 1, 2] != 1) && (Solutions[1, 0, 2] == 1 && Solutions[1, 1, 2] == 1) )
        {
            Debug.Log("Playing Third video hint");
            //VidPlayer.clip = PuzzleClips[2];
            //VidPlayer.Play();

        }
        else if ( (Solutions[3, 0, 2] != 1 && Solutions[3, 1, 2] != 1) && (Solutions[2, 0, 2] == 1 && Solutions[2, 1, 2] == 1))
        {
            Debug.Log("Playing Fourth video hint");
            //VidPlayer.clip = PuzzleClips[3];
            //VidPlayer.Play();
        }
        else if (Solutions[4, 0, 2] != 1 && Solutions[4, 1, 2] != 1 && (Solutions[3, 0, 2] == 1 && Solutions[3, 1, 2] == 1)) 
        {
            Debug.Log("Playing Fifth video hint");
            //VidPlayer.clip = PuzzleClips[4];
            //VidPlayer.Play();
        }
        else
        {
            Debug.Log("Playing First video hint");
            //VidPlayer.clip = PuzzleClips[0];
            //VidPlayer.Play();
        }
    }

    private void MatchingSolution()
    {
        /*Since the definition have been checked with type, 
         * here we just confirm all the bricks are on the correct coordinate*/
        if ( (Solutions[0, 0, 2] == 1 && Solutions[0, 1, 2] == 1) &&
             (Solutions[1, 0, 2] == 1 && Solutions[1, 1, 2] == 1) &&
             (Solutions[2, 0, 2] == 1 && Solutions[2, 1, 2] == 1) &&
             (Solutions[3, 0, 2] == 1 && Solutions[3, 1, 2] == 1) &&
             (Solutions[4, 0, 2] == 1 && Solutions[4, 1, 2] == 1) )
        {
            //Debug.Log(Solutions);
            isItSolved = true;
        }  
    }

    public void IsItSolved()
    {
        if (isItSolved)
        {
            Debug.Log(isItSolved);
            return;
        }
    }
}
