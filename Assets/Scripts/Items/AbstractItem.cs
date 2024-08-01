using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class AbstractItem : ScriptableObject
{
    [SerializeField] private string label;
    [SerializeField] private string description;
    [SerializeField] private int price;
    [SerializeField] private int UISizeX = 1;
    [SerializeField] private int UISizeY = 1;
    [SerializeField] GameObject image;

    public string GetLabel()
    {
        return label;
    }

    public string GetDescription()
    {
        return description;
    }

    public int GetPrice()
    {
        return price;
    }

    public GameObject GetImage()
    {
        return image;
    }

    public int GetUISizeX()
    {
        return UISizeX;
    }

    public int GetUISizeY()
    {
        return UISizeY;
    }
}
