using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //Serialize Field
    [Header("Stuff")]
    [SerializeField]
    private GameObject[] prefabs;

    [SerializeField]
    private Material[] playerMaterials;

    [SerializeField]
    private Material[] cardMaterials;

    //Constants
    private const int TEAM_NUM = 4;
    private const int TEAM_SIZE = 4;

    private const float START_CENTER_RADIUS = 10.53f;
    private const float START_OFFSET_RADIUS = 0.4f;
    private const float TILE_SURFACE_OFFSET = 0.6f;

    private const int BOARD_TILE_NUM = 15;
    private const float BOARD_TILE_RADIUS = 8.485f;

    //Objects
    private Piece[,] playerPieces;
    private Tile[,] startTiles;
    private Tile[,] boardTiles;
    private Tile[,] endTiles;

    public enum Color
    {
        Green = 0,
        Blue = 1,
        Red = 2,
        Yellow = 3,
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnAllTiles();
        SpawnAllPieces();
    }

    // Update is called once per frame
    void Update() { }

    private void SpawnAllPieces()
    {
        playerPieces = new Piece[TEAM_NUM, TEAM_SIZE];
        for (int i = 0; i < TEAM_NUM; i++)
        {
            for (int j = 0; j < TEAM_SIZE; j++)
            {
                playerPieces[i, j] = SpawnSinglePiece((Color)i);
                playerPieces[i, j].toTile(startTiles[i, j]);
            }
        }
    }

    private void SpawnAllTiles()
    {
        SpawnStartTiles();
        SpawnBoardTiles();
        SpawnEndTiles();
    }

    private void SpawnStartTiles()
    {
        startTiles = new Tile[TEAM_NUM, TEAM_SIZE];
        for (int i = 0; i < TEAM_NUM; i++)
        {
            Vector3 center = new Vector3(
                START_CENTER_RADIUS * (float)Math.Sin(Math.PI / 2 * i + Math.PI / 4),
                TILE_SURFACE_OFFSET,
                START_CENTER_RADIUS * (float)Math.Cos(Math.PI / 2 * i + Math.PI / 4)
            );

            Debug.Log(center);
            for (int j = 0; j < TEAM_SIZE; j++)
            {
                startTiles[i, j] = SpawnSingleTile();
                Vector3 offset = new Vector3(
                    START_OFFSET_RADIUS * (float)Math.Cos(Math.PI / 2 * j + Math.PI / 4),
                    0,
                    START_OFFSET_RADIUS * (float)Math.Sin(Math.PI / 2 * j + Math.PI / 4)
                );
                startTiles[i, j].transform.position = center + offset;
            }
        }
    }

    private void SpawnBoardTiles()
    {
        boardTiles = new Tile[TEAM_NUM, BOARD_TILE_NUM];

        for (int i = 0; i < TEAM_NUM; i++)
        {
            for (int j = 0; j < BOARD_TILE_NUM; j++)
            {
                Vector3 coord = new Vector3(
                    BOARD_TILE_RADIUS
                        * (float)Math.Sin(Math.PI / 30 * j + Math.PI / 2 * i + Math.PI / 4),
                    TILE_SURFACE_OFFSET,
                    BOARD_TILE_RADIUS
                        * (float)Math.Cos(Math.PI / 30 * j + Math.PI / 2 * i + Math.PI / 4)
                );
                boardTiles[i, j] = SpawnSingleTile();
                boardTiles[i, j].transform.position = coord;
            }
        }
    }

    private void SpawnEndTiles()
    {
        endTiles = new Tile[TEAM_NUM, TEAM_SIZE];
        for (int i = 0; i < TEAM_NUM; i++)
        {
            for (int j = 0; j < TEAM_SIZE; j++)
            {
                endTiles[i, j] = SpawnSingleTile();
            Vector3 coord = new Vector3(
                (0.7f*j+3.536f) * (float)Math.Sin(Math.PI / 2 * i + Math.PI / 4),
                TILE_SURFACE_OFFSET,
                (0.7f*j+3.536f) * (float)Math.Cos(Math.PI / 2 * i + Math.PI / 4)
            );
                endTiles[i, j].transform.position = coord;
            }
        }
    }

    private Piece SpawnSinglePiece(Color c)
    {
        Piece p = Instantiate(prefabs[0], transform).GetComponent<Piece>();
        p.GetComponent<MeshRenderer>().material = playerMaterials[(int)c];

        return p;
    }

    private Tile SpawnSingleTile()
    {
        Tile t = Instantiate(prefabs[1], transform).GetComponent<Tile>();
        return t;
    }
}
