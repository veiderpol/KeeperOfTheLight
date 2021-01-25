using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypeDefinitions {HEALTH, AMMO };

[CreateAssetMenu(fileName ="NewItem", menuName ="Item/New Item", order = 1)]
public class Items_SO : ScriptableObject
{
    public string itemName = "New Item";
    public ItemTypeDefinitions itemType = ItemTypeDefinitions.HEALTH;
    public int itemAmount = 0;
    public Sprite itemIcon = null;

    public bool isEquiped = false;

}
