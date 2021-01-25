using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryEntry 
{
    public ItemManager itemManager;
    public int hotBarSlot;
    public Sprite hotBarSprite;

    public InventoryEntry(ItemManager itemManager, Sprite hotBarSprite) 
    {
        this.hotBarSlot = 0;
        this.itemManager = itemManager;
        this.hotBarSprite = hotBarSprite;
    }
}
