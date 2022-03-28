using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAssets : MonoBehaviour
{
    public static InventoryAssets Instance { get; private set; }

    public void Awake() {
        Instance = this;
    }

    public Sprite heartSprite;
    public Sprite machineGunSprite;
    public Sprite bulletSprite;

}