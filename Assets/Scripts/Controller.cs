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

    //Constants
    private const int TEAM_NUM = 4;
    private const int TEAM_SIZE = 4;

    private const float START_CENTER_RADIUS = 10.53f;
    private const float START_OFFSET_RADIUS = 0.4f;
    private const float SURFACE_OFFSET = 0.6f;

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
                SURFACE_OFFSET,
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
                startTiles[i, j].GetComponent<MeshRenderer>().material = playerMaterials[(int)i];
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
                    SURFACE_OFFSET,
                    BOARD_TILE_RADIUS
                        * (float)Math.Cos(Math.PI / 30 * j + Math.PI / 2 * i + Math.PI / 4)
                );
                boardTiles[i, j] = SpawnSingleTile();
                boardTiles[i, j].transform.position = coord;
                boardTiles[i, j].GetComponent<MeshRenderer>().material = playerMaterials[(int)i];
            }
        }
    }

    private void SpawnEndTiles()
    {
        endTiles = new Tile[TEAM_NUM, TEAM_SIZE];
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
