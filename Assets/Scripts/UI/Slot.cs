using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isOccupied { get; private set; } = false;
    public UIItem item { get; private set; }
    

    public int OccupySlot(UIItem item_)
    {
        if(isOccupied)
        {
            return 1;
        }
        else
        {
            isOccupied = true;
            item = item_;
            return 0;
        }
    }

    public int FreeSlot()
    {
        if(isOccupied == false)
        {
            isOccupied = true;
            item = null;
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
