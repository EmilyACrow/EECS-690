using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem {

    public ItemType itemType;
    public int amount;
    public int health;
    public enum ItemType {
        Bullet,
        MachineGun,
        Heart,
    }

    public Sprite getSprite() {
        switch(itemType){
        default:
        case ItemType.Bullet: return InventoryAssets.Instance.bulletSprite;
        case ItemType.Heart: return InventoryAssets.Instance.heartSprite;
        case ItemType.MachineGun: return InventoryAssets.Instance.machineGunSprite;
        }
    }

}
