using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public static CharacterInventory instance;
    public CharacterStats charStats;

    public Image[] itemBar;
    public GameObject inventoryGameObject;
    [SerializeField]
    public Dictionary<int, InventoryEntry> itemsInInventory = new Dictionary<int, InventoryEntry>();
    public InventoryEntry inventoryEntry;

    bool addItemComplete = true;
    int invCapacity = 5;
    int idItem = 1;
    void Start()
    {
        instance = this;
        inventoryEntry = new InventoryEntry(null, null);
        itemsInInventory.Clear();
        ItemManager.onStore = StoreItem;
        CharacterStats.onClickHB = ClickItem;
        GameObject foundStats = GameObject.FindGameObjectWithTag("Player");
        charStats = foundStats.GetComponent<CharacterStats>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            DisplayInventory();
        if (!addItemComplete)
            TryPickItem();

    }
    public void StoreItem(ItemManager item) {
        addItemComplete = false;
        inventoryEntry.itemManager = item;
        inventoryEntry.hotBarSprite = item.itemsDefinition.itemIcon;
        item.gameObject.SetActive(false);
    }
    public void TryPickItem() 
    {
        if (itemsInInventory.Count == 0)
        {
            addItemComplete = AddItem();

        }
        else
        {
            if (itemsInInventory.Count == invCapacity)
            {
                inventoryEntry.itemManager.gameObject.SetActive(true);
            }
            else 
            {
                addItemComplete = AddItem();
            }
        }
    }
    public bool AddItem() {
        
        itemsInInventory.Add(idItem, new InventoryEntry(Instantiate(inventoryEntry.itemManager), inventoryEntry.hotBarSprite));
        DestroyObject(inventoryEntry.itemManager.gameObject);

        itemBar[idItem - 1].color = new Color(1,1,1,1);
        itemBar[idItem - 1].sprite = itemsInInventory[idItem].hotBarSprite;
        itemsInInventory[idItem].hotBarSlot = idItem;
    
        if (idItem <= itemsInInventory.Count) 
        {
            idItem++;
        }
        

        return true;
    }

    public void ClickItem(int itemID) 
    {
        foreach (KeyValuePair<int,InventoryEntry> ie in itemsInInventory) 
        {
            if (ie.Value.hotBarSlot == itemID) 
            {
                ie.Value.itemManager.UseItem();
                itemsInInventory.Remove(ie.Key);
                itemBar[itemID - 1].sprite = null;
                itemBar[itemID - 1].color = new Color(1,1,1,0);
                break;
            }
        }
    }
    void DisplayInventory() 
    {
        if (inventoryGameObject.activeSelf == true)
            inventoryGameObject.SetActive(false);
        else
            inventoryGameObject.SetActive(true);
    }
}
