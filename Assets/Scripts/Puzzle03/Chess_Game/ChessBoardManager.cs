using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.Rendering.HighDefinition;

public class ChessBoardManager : MonoBehaviour
{

    [SerializeField] public Camera ChessCamera;
    [SerializeField] private Material tileMaterial;
    [SerializeField] private float tileSize = 0.5f;
    [SerializeField] private float yOffset = 0.2f;
    [SerializeField] private Vector3 boardCenter = Vector3.zero;
    [SerializeField] private float dragOffset = 1.0f;

    [SerializeField] private GameObject[] prefab;
    [SerializeField] private Material[] teamMaterials;

    private List<Vector2Int> availableChessMove = new List<Vector2Int>();
    public ChessPieces[,] chessPieces;
    private ChessPieces currentlyDragging;
    private GameObject[,] boardTiles;
    private const int TILECOUNT_X = 8;
    private const int TILECOUNT_Y = 8;
    //private Camera currentCamera;
    private Vector2Int currentHover;
    private Vector3 bounds;
    protected Vector3 OriginPoint; //Used for getting the correct position of the chess game to move to. 

    public bool EnableMoves; //Used to toggle in puzzle manager


    //Getters and setters
    public bool GetEnableMoves(){return EnableMoves;}
    public void SetEnableMoves(bool enable){ EnableMoves = enable; }
    public Vector3 GetOriginPoint(){return transform.position;}

    public ChessPieces[,] GetCurrentChessPiecePositions(){ return chessPieces; }

    private void Awake()
    {
        //transform.SetPositionAndRotation(SetWorldPosition, SetRotation);
        //OriginPoint = transform.position; //need to invert for use of camera slerp in Puzzle manager
        GenerateChessGrid(tileSize, TILECOUNT_X, TILECOUNT_Y);

        SpawnUberAllesPuzzlePieces();
        
        PositionAllChessPieces();

    }

    private void Update()
    {
        if (!ChessCamera) //|| !EnableMoves
        {
            ChessCamera = Camera.main;
            return;
        }

        RaycastHit info;

        Ray ray = ChessCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("ChessTiles", "Hover")))
        {


            // Get the indexes of the tile i've hit
            Vector2Int hitPosition = LookUpTileIndex(info.transform.gameObject);

            //Debug.Log(hitPosition);
            
            // If we're hovering a tile after not hovering any tiles
            if (currentHover == -Vector2Int.one)
            {
                currentHover = hitPosition;
                boardTiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }
        
            // If we were already hovering a tile, change the previous one
            if (currentHover != hitPosition)
            {

                boardTiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("ChessTiles");
                currentHover = hitPosition;
                boardTiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }
        
            if (Input.GetMouseButtonDown(0))
            {
                if (chessPieces[hitPosition.x, hitPosition.y] != null)
                {
                    if (EnableMoves == true)
                    {
                        currentlyDragging = chessPieces[hitPosition.x, hitPosition.y];
                        availableChessMove = currentlyDragging.GetAvailableMoves(ref chessPieces, TILECOUNT_X, TILECOUNT_Y);
                    }
                }
            }

            if (currentlyDragging != null && Input.GetMouseButtonUp(0))
            {
                Vector2Int prevPosition = new Vector2Int(currentlyDragging.currentX, currentlyDragging.currentY);
            
                bool GetValidMove = MoveTo(currentlyDragging, hitPosition.x, hitPosition.y);
                if (!GetValidMove){
                    currentlyDragging.transform.position = CalcTileCenter(prevPosition.x, prevPosition.y);
                    currentlyDragging = null;
                }
                else
                {
                    currentlyDragging = null;
                }
            
            }

        }
        else
        {
            if (currentHover != -Vector2Int.one)
            {
                boardTiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("ChessTiles");
                currentHover = - Vector2Int.one;
            }
            
            if(currentlyDragging && Input.GetMouseButtonUp(0))
            {
                currentlyDragging.transform.position = CalcTileCenter(currentlyDragging.currentX, currentlyDragging.currentY);
                currentlyDragging = null;
            }
        }
        
        
        if (currentlyDragging){
            Plane horizontalPlane = new Plane(Vector3.up, Vector3.up * yOffset  );
            float distance = 0.0f;
                if(horizontalPlane.Raycast(ray, out distance)){
                    currentlyDragging.SetPosition(ray.GetPoint(distance) + Vector3.up * dragOffset);
            }
        }
    }

    //Board Generation
    private void GenerateChessGrid(float tileSize, int tileCountX, int tileCountY)
    {
        yOffset += transform.position.y;
        bounds = new Vector3((tileCountX / 2) * tileSize , 0, (tileCountX / 2) * tileSize) + boardCenter;

        boardTiles = new GameObject[tileCountX, tileCountY];
        for (int x = 0; x < tileCountX; x++)
            for (int y = 0; y < tileCountY; y++)
                boardTiles[x, y] = GenerateSingleTile(tileSize, x, y);
    }

    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        GameObject tile = new GameObject(string.Format("X:{0}, Y:{1}", x, y));
        tile.transform.parent = transform;

        //Generate 3d model for tile
        Mesh mesh = new Mesh();
        tile.AddComponent<MeshFilter>().mesh = mesh;
        tile.AddComponent<MeshRenderer>().material = tileMaterial;

        //tilesize is the individual tiles size.
        
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * tileSize, yOffset, y * tileSize) - bounds;
        vertices[1] = new Vector3(x * tileSize, yOffset, (y + 1) * tileSize) - bounds;
        vertices[2] = new Vector3((x + 1) * tileSize, yOffset, y * tileSize) - bounds;
        vertices[3] = new Vector3((x + 1) * tileSize, yOffset, (y + 1) * tileSize) - bounds;

        //triangle rendering order, it depends on the 
        int[] tri_render = new int[] {0, 1, 2, 1, 3, 2 };

        //add these to the tile mesh
        mesh.vertices = vertices;
        mesh.triangles = tri_render;
        mesh.RecalculateNormals();

        tile.layer = LayerMask.NameToLayer("ChessTiles"); //Ensure they are on the chesstile layer.
        tile.AddComponent<BoxCollider>();

        return tile;
    }
    
    private Vector2Int LookUpTileIndex(GameObject hitInfo)
    {
        for (int x = 0; x < TILECOUNT_X; ++x)
          for (int y = 0; y < TILECOUNT_Y; ++y)
                if (boardTiles[x, y] == hitInfo)
                    return new Vector2Int(x, y);
    
        //Impossible case but needed since all possible return paths must be satisfied.
        return -Vector2Int.one;
    }

    private void SpawnUberAllesPuzzlePieces()
    {
        chessPieces = new ChessPieces[TILECOUNT_X, TILECOUNT_Y];

        int whiteTeam = 0;
        int blackTeam = 1;

        chessPieces[0, 0] = SpawnAPiece(ChessPieceType.Rook, whiteTeam);
        chessPieces[1, 0] = SpawnAPiece(ChessPieceType.Knight, whiteTeam);
        chessPieces[2, 0] = SpawnAPiece(ChessPieceType.Bishop, whiteTeam);
        chessPieces[3, 0] = SpawnAPiece(ChessPieceType.King, whiteTeam);
        chessPieces[4, 0] = SpawnAPiece(ChessPieceType.Queen, whiteTeam);
        chessPieces[5, 0] = SpawnAPiece(ChessPieceType.Bishop, whiteTeam);
        chessPieces[6, 0] = SpawnAPiece(ChessPieceType.Knight, whiteTeam);
        chessPieces[7, 0] = SpawnAPiece(ChessPieceType.Rook, whiteTeam);
        for(int i = 0; i < TILECOUNT_X; i++)
        {
            chessPieces[i, 1] = SpawnAPiece(ChessPieceType.Pawn, whiteTeam);
        }

        chessPieces[0, 7] = SpawnAPiece(ChessPieceType.Rook, blackTeam);
        chessPieces[1, 7] = SpawnAPiece(ChessPieceType.Knight, blackTeam);
        chessPieces[2, 7] = SpawnAPiece(ChessPieceType.Bishop, blackTeam);
        chessPieces[3, 7] = SpawnAPiece(ChessPieceType.Queen, blackTeam);
        chessPieces[4, 7] = SpawnAPiece(ChessPieceType.King, blackTeam);
        chessPieces[5, 7] = SpawnAPiece(ChessPieceType.Bishop, blackTeam);
        chessPieces[6, 7] = SpawnAPiece(ChessPieceType.Knight, blackTeam);
        chessPieces[7, 7] = SpawnAPiece(ChessPieceType.Rook, blackTeam);
        for (int i = 0; i < TILECOUNT_X; i++)
        {
            chessPieces[i, 6] = SpawnAPiece(ChessPieceType.Pawn, blackTeam);
        }
    }
    
    private ChessPieces SpawnAPiece(ChessPieceType type, int team)
    {
        ChessPieces cp = Instantiate(prefab[(int)type - 1], transform).GetComponent<ChessPieces>();

        cp.pieceType = type;
        cp.team = team;
        Material[] currMat = cp.GetComponent<MeshRenderer>().materials;
        Material[] newCurrMat = new Material[2] { currMat[0], teamMaterials[team] };
        cp.GetComponent<MeshRenderer>().materials = newCurrMat;
    
        return cp;
    }

    
    private void PositionAllChessPieces()
    {
        for(int x = 0; x < TILECOUNT_X; x++)
        {
            for(int y = 0; y < TILECOUNT_Y; y++)
            {
                if(chessPieces[x, y] != null)
                {
                    PositionSingularPieces(x, y, true);
                }
            }
        }
    }

    private void PositionSingularPieces(int x, int y, bool force = false)
    {
        chessPieces[x, y].currentX = x;
        chessPieces[x, y].currentY = y;
        //chessPieces[x, y].SetPosition(CalcTileCenter(x, y), force);
        chessPieces[x, y].transform.position = new Vector3(x * tileSize, yOffset, y * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2);
            
    }

    private bool MoveTo(ChessPieces cp, int x, int y)
    {
        if (!ContainsValidMove(ref availableChessMove, new Vector2(x, y)))
            return false;
    
        Vector2Int prevPosition = new(cp.currentX, cp.currentY);
    
        if (chessPieces[x,y] != null)
        {
            ChessPieces oldChessPiece = chessPieces[x, y];
            if (cp.team == oldChessPiece.team)
            {
                return false;     
            }
        }
    
    
        chessPieces[x, y] = cp;
        chessPieces[prevPosition.x, prevPosition.y] = null;
    
        PositionSingularPieces(x, y);
    
        return true;
    }


    private bool ContainsValidMove(ref List<Vector2Int> moves, Vector2 pos)
    {
        for(int i = 0; i < moves.Count; i++)
        {
            if (moves[i].x == pos.x && moves[i].y == pos.y)
            {
                return true; 
            }
        }

        return false;
    }

    private Vector3 CalcTileCenter(int x, int y)
    {     
        //Vector3 Center = new Vector3(x * tileSize, yOffset, y * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2);
        return new Vector3(x * tileSize, yOffset, y * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2);
    }

    /*
    private IEnumerator SmoothMove(int x, int y)
    {
        Vector3 desiredPosition = new Vector3(x * tileSize, yOffset, y * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2);

        chessPieces[x, y].transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);

        yield return null;
    }*/

}
