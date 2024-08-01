/*
 *    This class is responsible for generating grid which is used by RoomOrganiser to generate random room set.
 *    
 *    Example of GenerateGrid() output:
 *        [0] [0] [2] [0] [0]               [ ] [ ] [ ] [ ] [ ]
 *        [0] [2] [1] [1] [2]               [ ] [ ] [X] [X] [ ]
 *        [2] [1] [1] [1] [0]       ->      [ ] [X] [X] [X] [ ]
 *        [2] [1] [2] [1] [1]               [ ] [X] [ ] [X] [X]
 *        [0] [1] [0] [0] [0]               [ ] [X] [ ] [ ] [ ]
 *       
 *    Where:
 *    1 - tile
 *    0 - empty
 *    2 - forbidden tile
 */

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Aseprite;

public class GridCreator
{
    private int gridWidth;
    private int gridHeight;
    private int maxCells;
    private int minCells;
    private float gapChance;

    private int currCells = 0;
    private int[,] grid;
    List<(int, int)> allowedTiles;

    public GridCreator(int gridWidth_, int gridHeight_, int minCells_, int maxCells_, float gapChance_)
    {
        if (minCells_ > gridHeight_ * gridWidth_ || minCells_ < 1)
            throw new System.ArgumentException("MinCells out of range");
        if (maxCells_ < minCells_)
            throw new System.ArgumentException("MaxCells out of range");
        if (gapChance_ > 1 || gapChance_ < 0)
            throw new System.ArgumentException("GapChance out of range");
        if (gridHeight_ < 1 || gridWidth_ < 1)
            throw new System.ArgumentException("Incorrect grid size");

        gridWidth = gridWidth_;
        gridHeight = gridHeight_;
        minCells = minCells_;
        maxCells = maxCells_;
        gapChance = gapChance_;
    }

    public int[,] GenerateGrid()
    {
        // Init new grid
        grid = new int[gridWidth, gridHeight];
        allowedTiles = new List<(int, int)>();

        // Place starting cell
        var startCell = (gridWidth / 2, gridHeight / 2);
        SpawnCell(startCell);
        allowedTiles.AddRange(FindAllowedNeighbours(startCell));

        // Start cell-placing loop
        while (currCells < maxCells)
        {
            // remove duplicates from allowed tiles
            allowedTiles = allowedTiles.Distinct().ToList();

            // place cell at random allowed tile
            (int,int) cellToSpawn;
            if(allowedTiles.Count > 0)
            {
                int rng = Random.Range(0, allowedTiles.Count);
                cellToSpawn = allowedTiles[rng];

            }
            else if(currCells < minCells) // force spawn cells to avoid spawn locks
            {
                cellToSpawn = FindRandomLockedCell();
             }
            else
            {
                break;
            }
            SpawnCell(cellToSpawn);
            allowedTiles.AddRange(FindAllowedNeighbours(cellToSpawn));
        }
        return grid;
    }

    private List<(int,int)> FindAllowedNeighbours((int,int) cell)
    { 
        var retList = new List<(int, int)>();

        // Right
        if (cell.Item1 + 1 < gridWidth && grid[cell.Item1 + 1, cell.Item2] == 0)
            retList.Add((cell.Item1 + 1, cell.Item2));
        // Left
        if (cell.Item1 - 1 >= 0 && grid[cell.Item1 - 1, cell.Item2] == 0)
            retList.Add((cell.Item1 - 1, cell.Item2));
        // Up
        if (cell.Item2 + 1 < gridHeight && grid[cell.Item1, cell.Item2 + 1] == 0)
            retList.Add((cell.Item1, cell.Item2 + 1));
        // Down
        if (cell.Item2 - 1 >= 0 && grid[cell.Item1, cell.Item2 - 1] == 0)
            retList.Add((cell.Item1, cell.Item2 - 1));

        return retList;
    }

    private bool IsInAllowedTiles((int,int) cell)
    {
        foreach(var tile in allowedTiles)
        {
            if (tile.Item1 == cell.Item1 && tile.Item2 == cell.Item2)
                return true;
        }
        return false;
    }

    private void RemoveFromAllowedTiles((int,int) cell)
    {
        var retList = new List<(int, int)>();
        foreach (var tile in allowedTiles)
        {
            if (tile.Item1 != cell.Item1 || tile.Item2 != cell.Item2)
                retList.Add(tile);
        }
        allowedTiles = retList;
    }
    private (int,int) FindRandomLockedCell()
    {
        var lockedCells = new List<(int, int)>();

        for(int row = 0; row < gridHeight; row++)
        {
            for(int col = 0; col < gridWidth; col++)
            {
                if (grid[col, row] == 2)
                    lockedCells.Add((col, row));
            }
        }
        
        int rng = Random.Range(0, lockedCells.Count);
        return lockedCells[rng];
    }

    private void SpawnCell((int,int) cell)
    {
        ChangeCell(cell, 1);
        MakeGapsInNeighbours(cell);
        RemoveFromAllowedTiles(cell);
        currCells++;
    }

    private void ChangeCell((int,int) cell, int value)
    {
        grid[cell.Item1, cell.Item2] = value;
    }

    private void MakeGapsInNeighbours((int,int) source)
    {
        /* 
         *  This function makes random gaps in given neighbours which forbid them to be included in allowedTiles list
        */
        foreach (var cell in FindAllowedNeighbours(source))
        {
            float rng = Random.Range(0f, 1f);
            if(rng < gapChance && !IsInAllowedTiles(cell))
            {
                ChangeCell(cell, 2);
            }
        }
    }

    public string GridToStr()
    {
        string str = "";
        for (int row = gridHeight - 1; row >= 0; row--)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                str += grid[col, row].ToString();
            }
            str += "\n";
        }
        return str;
    }
}
