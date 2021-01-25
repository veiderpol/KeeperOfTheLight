using System;
using UnityEngine;


public class ItemManager : MonoBehaviour
{
    public Items_SO itemsDefinition;
    public CharacterStats characterStats;
    #region Delegates
    public static Action<ItemManager> onStore;
    public static Action<int> onHealth;
    public static Action<int> onDamage;
    #endregion


    public void UseItem() 
    {
        switch (itemsDefinition.itemType) { 
            case ItemTypeDefinitions.HEALTH:
                onHealth(itemsDefinition.itemAmount);
                break;
            case ItemTypeDefinitions.AMMO:
                onDamage(itemsDefinition.itemAmount);
                break;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && onStore != null)
        {
            onStore(this);
        }
    }
}
