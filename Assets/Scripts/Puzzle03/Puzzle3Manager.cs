using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Puzzle3Manager : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private VideoPlayer VidPlayer;
    [SerializeField] private VideoClip[] PuzzleClips;
    [SerializeField] private GameObject ChessManager;
    [SerializeField] private List<Tuple<int, int>> Solutions; //values must match a 8x8 chessboard

    private Vector3 OldMainCamPosition;
    private Vector3 NewMainCamPosition;
    private Vector3 ChessBoardOrigin;
    private ChessBoardManager chessBoardManager;

    private bool PlayingChess = false;

    private void Awake()
    {
        chessBoardManager = ChessManager.GetComponent<ChessBoardManager>();
    }
    
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCamera == null)
        {
            throw new Exception("MainCameraNotConnected");
        }

        //View at
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlayingChess = !PlayingChess;

            if(PlayingChess != true)
            {
                MainCamera.transform.position = OldMainCamPosition;
            }
            else
            {
                OldMainCamPosition = MainCamera.transform.position;
                ChessBoardOrigin = chessBoardManager.GetOriginPoint();
            }
        }
    }
    
    IEnumerator CameraLerpToChess()
    {
        MainCamera.transform.position = Vector3.Slerp(MainCamera.transform.position, ChessBoardOrigin, 2f);
        //MainCamera.transform.rotation = Quaternion.LookRotation();


        yield return null;
    }
}
