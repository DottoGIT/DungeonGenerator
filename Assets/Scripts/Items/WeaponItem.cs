using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectableItem", menuName = "ScriptableObjects/Items/Weapon", order = 2)]
public class WeaponItem : AbstractItem
{
    [SerializeField] private float damage;
    [SerializeField] private float durability;
}
