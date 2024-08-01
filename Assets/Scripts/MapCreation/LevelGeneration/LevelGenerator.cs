/*
 * This class is responsible for spawning random rooms according to room sets given by RoomOrganiser
*/

using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [Header("Grid Creator Settings")]
    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;
    [SerializeField] int maxCells;
    [SerializeField] int minCells;
    [Range(0, 1)] [SerializeField] float gapChance;

    [Header("Spawning Parameters")]
    [SerializeField] Transform spawnerParent;
    [SerializeField] float roomWidth;
    [SerializeField] float roomHeight;

    [Header("Level Prefabs")]
    [SerializeField] List<GameObject> StartRoom = new List<GameObject>();
    [SerializeField] List<GameObject> Left = new List<GameObject>();
    [SerializeField] List<GameObject> Up = new List<GameObject>();
    [SerializeField] List<GameObject> Right = new List<GameObject>();
    [SerializeField] List<GameObject> Down = new List<GameObject>();
    [SerializeField] List<GameObject> LeftUp = new List<GameObject>();
    [SerializeField] List<GameObject> LeftRight = new List<GameObject>();
    [SerializeField] List<GameObject> LeftDown = new List<GameObject>();
    [SerializeField] List<GameObject> UpRight = new List<GameObject>();
    [SerializeField] List<GameObject> UpDown = new List<GameObject>();
    [SerializeField] List<GameObject> RightDown = new List<GameObject>();
    [SerializeField] List<GameObject> UpRightDown = new List<GameObject>();
    [SerializeField] List<GameObject> LeftRightDown = new List<GameObject>();
    [SerializeField] List<GameObject> LeftUpDown = new List<GameObject>();
    [SerializeField] List<GameObject> LeftUpRight = new List<GameObject>();
    [SerializeField] List<GameObject> LeftUpRightDown = new List<GameObject>();

    float offsetX;
    float offsetY;


    private void Start()
    {
        // Initialize variables
        offsetX = -1 * gridWidth * roomWidth / 2;
        offsetY = -1 * gridHeight * roomHeight / 2;

        // Start Generating
        GenerateLevel();
    }

    public void RenerateLevel()
    {
        DestroyLevel();
        Start();
    }

    private void GenerateLevel()
    {
        // Generate rooms code array
        GridCreator gridCreator = new GridCreator(gridWidth, gridHeight, minCells, maxCells, gapChance);
        RoomOrganiser roomOrganiser = new RoomOrganiser(gridCreator.GenerateGrid(), gridWidth, gridHeight);
        string[,] codesArray = roomOrganiser.GenerateRoomSet();
        // Spawn according rooms
        for (int i = 0; i < gridHeight; i++)
        {
            for(int j = 0; j < gridWidth; j++)
            {
                string roomType = codesArray[j, i];
                if (roomType != "")
                {
                    SpawnTile(j, i, roomType);
                }
            }
        }
    }

    private void DestroyLevel()
    {
        foreach(Transform child in spawnerParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void SpawnTile(int posX, int posY, string code)
    {
        var position = new Vector3((posX * roomWidth) + offsetX, (posY * roomHeight) + offsetY, 0);
        GameObject tile = Instantiate(GetTileObject(code), position, Quaternion.identity);
        tile.transform.SetParent(spawnerParent);
    }

    private GameObject GetTileObject(string code)
    {
        List<GameObject> roomsToTakeFrom;
        switch (code)
        {
            case "L": 
                roomsToTakeFrom = Left;
                break;
            case "U": 
                roomsToTakeFrom = Up;
                break;
            case "R": 
                roomsToTakeFrom = Right;
                break;
            case "D": 
                roomsToTakeFrom = Down;
                break;
            case "LU": 
                roomsToTakeFrom = LeftUp;
                break;
            case "LR": 
                roomsToTakeFrom = LeftRight;
                break;
            case "LD": 
                roomsToTakeFrom = LeftDown;
                break;
            case "UR": 
                roomsToTakeFrom = UpRight;
                break;
            case "UD": 
                roomsToTakeFrom = UpDown;
                break;
            case "RD": 
                roomsToTakeFrom = RightDown;
                break;
            case "URD": 
                roomsToTakeFrom = UpRightDown;
                break;
            case "LRD": 
                roomsToTakeFrom = LeftRightDown;
                break;
            case "LUD": 
                roomsToTakeFrom = LeftUpDown;
                break;
            case "LUR": 
                roomsToTakeFrom = LeftUpRight;
                break;
            case "LURD": 
                roomsToTakeFrom = LeftUpRightDown;
                break;
            default:
                roomsToTakeFrom = StartRoom;
                break;
        }
        int rng = Random.Range(0, roomsToTakeFrom.Count);
        return roomsToTakeFrom[rng];
    }

}
