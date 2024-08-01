/*
 *  This class is responsible for returning ready-to-generate room set array, which is used by
 *  LevelGenerator to place correct room types in given locations
 * 
 *  Example of GenerateRoomSet() output:
 *      [""    ] [""    ] [""    ] [""    ] [""    ]
 *      [""    ] [""    ] ["RD"  ] ["LD"  ] [""    ]
 *      [""    ] ["RD"  ] ["SLUR"] ["LUD" ] [""    ]
 *      [""    ] ["UD"  ] [""    ] ["UR"  ] ["L"   ]
 *      [""    ] ["U"   ] [""    ] [""    ] [""    ]
 *      
 *  Where doors are signed:
 *  S - start
 *  L - left 
 *  U - up
 *  R - right
 *  D - down
*/

using UnityEngine;

public class RoomOrganiser
{
    private int gridWidth;
    private int gridHeight;
    private int[,] grid;
    private string[,] codesGrid;

    public RoomOrganiser(int[,] grid_, int gridWidth_, int gridHeight_)
    {
        if (gridHeight_ < 1 || gridWidth_ < 1)
            throw new System.ArgumentException("Incorrect grid size");

        gridWidth = gridWidth_;
        gridHeight = gridHeight_;
        grid = grid_;
    }

    public string[,] GenerateRoomSet()
    {
        codesGrid = new string[gridWidth, gridHeight];

        for(int i = 0; i < gridHeight; i++)
        {
            for(int j = 0; j < gridWidth; j++)
            {
                if (grid[j, i] == 1)
                    codesGrid[j, i] = GenerateCodeForCell((j, i));
                else
                    codesGrid[j, i] = "";
            }
        }
        return codesGrid;
    }

    public string GridToStr()
    {
        string str = "";
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                str += codesGrid[j, i];
            }
            str += "\n";
        }
        return str;
    }

    private string GenerateCodeForCell((int,int) cell)
    {
        string code = "";
        // Check if its starting cell
        if (cell.Item1 == gridWidth / 2 && cell.Item2 == gridHeight / 2)
            code += "S";
        //  Check for left Door
        if (cell.Item1 - 1 >= 0 && grid[cell.Item1 - 1, cell.Item2] == 1)
            code += "L";
        //  Check for up Door
        if (cell.Item2 + 1 < gridHeight && grid[cell.Item1, cell.Item2 + 1] == 1)
            code += "U";
        // Check for right Door
        if (cell.Item1 + 1 < gridWidth && grid[cell.Item1 + 1, cell.Item2] == 1)
            code += "R";
        //  Check for down Door
        if (cell.Item2 - 1 >= 0 && grid[cell.Item1, cell.Item2 - 1] == 1)
            code += "D";
        return code;
    }

}
