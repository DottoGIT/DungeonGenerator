using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] GameObject InventoryWindow;
    bool isInventoryActive = false;
    
    private void Update()
    {
        if(InputManager.instance.GetInventoryOpen())
        {
            isInventoryActive = !isInventoryActive;
            InventoryWindow.SetActive(isInventoryActive);
        }
    }
}
