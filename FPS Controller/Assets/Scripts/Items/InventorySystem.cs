using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventorySystem
{
    public List<InventoryItem> inventoryList;

    public InventorySystem() {
        inventoryList = new List<InventoryItem>();

        addItem(new InventoryItem { itemType = InventoryItem.ItemType.MachineGun, amount=1, health=100});
        Debug.Log("Inventory Activated...");
    }

    public void addItem(InventoryItem item) {
        inventoryList.Add(item);
    }

    public List<InventoryItem> getItemList() {
        return inventoryList;
    }
}