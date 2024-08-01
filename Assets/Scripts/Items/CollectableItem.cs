using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectableItem", menuName = "ScriptableObjects/Items/Collectable", order = 1)]
public class CollectableItem : AbstractItem
{
    [SerializeField] private int maxStack;

    public int GetMaxStack()
    {
        return maxStack;
    }
}
