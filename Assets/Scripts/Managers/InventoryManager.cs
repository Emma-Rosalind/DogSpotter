using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Managers
{
    public class InventoryManager : MonoSingle<InventoryManager>
    {
        [SerializeField] public List<ItemHolder> _allItems = new List<ItemHolder>();
        [HideInInspector] public Dictionary<ItemStates.ItemName, ItemHolder> _allItemsDic = new Dictionary<ItemStates.ItemName, ItemHolder>();
        
        //Items placed
        [HideInInspector] public List<ItemData> itemsPlaced  { private set; get; }
        //Items not places
        [HideInInspector] public List<ItemStates.ItemName> itemInventory { private set; get; }


        [SerializeField]
        public class ItemData
        {
            public ItemStates.ItemName key;
            public Vector2 position;
        }
        

        void Start()
        {
            
            itemInventory = new List<ItemStates.ItemName>();
            itemsPlaced = new List<ItemData>();
            //Create dictionary of items
            foreach (var item in _allItems)
            {
                _allItemsDic.Add(item.key, item);
            }
            
            //Create inventory tracking
            //foreach (var item in PlayerDataManager.Instance._playerData.itemInventory)
            //{
            //    itemInventory.Add(item.Key);
           // }
            
           // foreach (var item in PlayerDataManager.Instance._transformPlayerData.itemInventory)
            //{
            //    itemsPlaced.Add(item);
            //    itemInventory.Remove(item.key);
           // }
            
            //Temp while no player data
            AddToInventory(ItemStates.ItemName.BlueCarpet);

        }
        
        public void AddToInventory(ItemStates.ItemName key)
        {
            itemInventory.Add(key);
        }

    }
}