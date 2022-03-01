// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class InventorySystem : MonoBehaviour
// {
//     private Dictionary<InventoryItem,InventoryItemData> _itemDictionary;
//     public List<InventoryItem> inventory { get; private set; }

//     // Awake is called before the first frame update
//     void Awake()
//     {
//         _itemDictionary = new Dictionary<InventoryItem, InventoryItemData>();
//         inventory = new List<InventoryItem>();
//     }

//     public void Add(InventoryItemData item) {
//         // if (_itemDictionary.TryGetValue(item, out InventoryItem val)) {
//         //     val.AddToStack();
//         // } else {
//         //     InventoryItem newItem = new InventoryItem(item);
//         //     inventory.Add(newItem);
//         //     _itemDictionary.Add(item, newItem);
//         // }
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
