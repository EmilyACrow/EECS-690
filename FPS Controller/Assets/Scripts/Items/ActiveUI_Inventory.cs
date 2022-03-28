using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUI_Inventory : MonoBehaviour
{
    private InventorySystem inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotSlot1;
    private Transform itemSlotSlot2;
    private Transform healthSlot;

    private void Start() {
        itemSlotContainer = transform.Find("InventorySlots");
        itemSlotSlot1 = itemSlotContainer.Find("InventorySlot1");
        itemSlotSlot2 = itemSlotContainer.Find("InventorySlot2");
        healthSlot = itemSlotContainer.Find("InventorySlot3");

    }

    public void setInventory(InventorySystem inventory) {
        
        this.inventory=inventory;
        refreshInventoryItems();
    }

    private void refreshInventoryItems() {
        foreach (InventoryItem item in inventory.getItemList()) {
            RectTransform itemSlot1Transform = Instantiate(itemSlotContainer, itemSlotSlot1).GetComponent<RectTransform>();
            RectTransform itemSlot2Transform = Instantiate(itemSlotContainer, itemSlotSlot2).GetComponent<RectTransform>();
            RectTransform healthSlotTransform = Instantiate(itemSlotContainer, healthSlot).GetComponent<RectTransform>();
            itemSlot1Transform.gameObject.SetActive(true);
            itemSlot2Transform.gameObject.SetActive(true);
            healthSlotTransform.gameObject.SetActive(true);
        }
    }

}