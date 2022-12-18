using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardManager : MonoBehaviour
{
    [SerializeField] private Material tileMaterial;
    [SerializeField] private float tileSize = 0.5f;
    [SerializeField] private float yOffset = 0.2f;
    [SerializeField] private Vector3 boardCenter = Vector3.zero;


    //[SerializeField] private Vector3 SetWorldPosition;
    //[SerializeField] private Quaternion SetRotation;

    private GameObject[,] boardTiles;
    private const int TILECOUNT_X = 8;
    private const int TILECOUNT_Y = 8;
    private Camera currentCamera;
    private Vector2Int currentHover;
    private Vector3 bounds;
    protected Vector3 OriginPoint; //Used for getting the correct position of the chess game to move to. 

    protected bool EnableMoves; //Used to toggle in puzzle manager


    //Getters and setters
    bool GetEnableMoves(){return EnableMoves;}
    void SetEnableMoves(bool enable){ EnableMoves = enable; }

    public Vector3 GetOriginPoint(){return OriginPoint;}

    private void Awake()
    {
        //transform.SetPositionAndRotation(SetWorldPosition, SetRotation);
        OriginPoint = transform.position; //need to invert for use of camera slerp in Puzzle manager
        GenerateChessGrid(1, TILECOUNT_X, TILECOUNT_Y);
    }

    private void Update()
    {
        if (!currentCamera)
        {
            currentCamera = Camera.main;
        }

        ChessUpdate();
    }

    IEnumerator ChessUpdate()
    {
        RaycastHit hitInfo;
        Ray raycast = currentCamera.ScreenPointToRay(transform.position);
        if (Physics.Raycast(raycast, out hitInfo, 100, LayerMask.GetMask("ChessTiles")))
        {
            //Get tile indices
            Vector2Int hitposition = LookUpTileIndex(hitInfo.transform.gameObject);

            if (currentHover != -Vector2Int.one)
            {
                currentHover = hitposition;
                boardTiles[hitposition.x, hitposition.y].layer = LayerMask.NameToLayer("Hover");
            }
            if (currentHover != hitposition)
            {
                boardTiles[hitposition.x, hitposition.y].layer = LayerMask.NameToLayer("ChessTile");
                currentHover = hitposition;
                boardTiles[hitposition.x, hitposition.y].layer = LayerMask.NameToLayer("Hover");
            }

        }
        else
        {
            if (currentHover != -Vector2Int.one)
            {
                boardTiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("ChessTile");
                currentHover = -Vector2Int.one;
            }
        }

        yield return null;
    }


    //Board Generation
    private void GenerateChessGrid(float tileSize, int tileCountX, int tileCountY)
    {
        yOffset += transform.position.y;
        bounds = new Vector3(  (tileCountX / 2) * tileSize , 0, (tileCountX / 2) * tileSize) + boardCenter;

        boardTiles = new GameObject[tileCountX, tileCountY];
        for (int x = 0; x < tileCountX; x++)
            for (int y = 0; y < tileCountY; y++)
                boardTiles[x, y] = GenerateSingleTile(tileSize, x, y);
    }
    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        GameObject tile = new GameObject(string.Format("X:{0}, Y:{1}", x, y));
        tile.transform.parent = transform;

        //Some positioning crap
        if(x != TILECOUNT_X)
        {
            if(y != TILECOUNT_Y)
            {
                if (x == 0 && y == 0)
                {
                    //the initial line one
                    tile.transform.position = new Vector3(transform.position.x, 0, transform.position.y);
                }
                //here we place the normal fields
                tile.transform.position = new Vector3(transform.position.x + tileSize, 0, transform.position.y);
            }
            else
            {
                //Impossible case - Done changing all positions
                Debug.Log("finished creating chess board");
            }
        }
        else
        {
            if(x == TILECOUNT_X)
            {
                tile.transform.position = new Vector3(transform.position.x + tileSize, 0, transform.position.y);
            }
            else
            {
                //X tile end - change y value down 1, reset x to 0
                tile.transform.position = new Vector3(transform.position.x + tileSize, 0, transform.position.y + tileSize);
            }
            
        }

        tile.layer = LayerMask.GetMask(("ChessTiles")); //Ensure they are on the chesstile layer.

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

        tile.AddComponent<BoxCollider>();

        return tile;
    }

    //
    private Vector2Int LookUpTileIndex(GameObject hitInfo)
    {
        for (int x = 0; x < TILECOUNT_X; ++x)
            for (int y = 0; y < TILECOUNT_Y; ++y)
                if (boardTiles[x, y] == hitInfo)
                    return new Vector2Int(x, y);

        //Impossible case but needed since all possible return paths must be satisfied.
        return -Vector2Int.one;
    }


}
