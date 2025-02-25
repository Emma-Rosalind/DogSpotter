using System.Collections.Generic;
using System.Linq;
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
        
        public class ItemData
        {
            public ItemStates.ItemName key;
            public int[] position;
            public int id;
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

            Debug.Log("Loading inventory");
            
            //Create inventory tracking
            foreach (var item in PlayerDataManager.Instance._playerData.ItemInventory)
            {
                Debug.Log("Added from loading: " + item.Key);
                itemInventory.Add(item.Key);
            }
            
            //remove placed items
            foreach (var item in PlayerDataManager.Instance._transformPlayerData.ItemInventory)
            {
                Debug.Log("Placed from loading: " + item.key);
                itemsPlaced.Add(item);
                itemInventory.Remove(item.key);
            }
        }
        
        public void AddToInventoryFromShop(ItemStates.ItemName key)
        {
            Debug.Log("Add to Inventory From Shop: " + key);
            itemInventory.Add(key);
            PlayerDataManager.Instance.UpdateItems(true);
        }
        
        public void AddToInventoryFromFloor(ItemStates.ItemName key, int id)
        {
            itemInventory.Add(key);
            Debug.Log("Add to Inventory From Floor: " + key);
            var item = itemsPlaced.First(x => x.id == id);
            itemsPlaced.Remove(item);
            PlayerDataManager.Instance.UpdateItems();
        }
        
        public int RemoveToPlace(ItemStates.ItemName key, Vector2 place)
        {
            var count = itemsPlaced.Count(x => x.key == key);
            var item = new ItemData();
            item.key = key;
            item.position = new int[2] { (int)place.x, (int)place.y};
            item.id = count;
            itemsPlaced.Add(item);
            itemInventory.Remove(key);

            Debug.Log("Remove from Inventory: " + key);
            PlayerDataManager.Instance.UpdateItems();
            
            return count;
        }
        
        public void UpdateFromFloor(ItemStates.ItemName key, int id, Vector2 position)
        {
            var item = itemsPlaced.First(x => x.id == id);
            item.position =  new int[2] { (int)position.x, (int)position.y};;
            PlayerDataManager.Instance.UpdateItems();
        }

        public Dictionary<ItemStates.ItemName, int> GetOwnedItems()
        {
            var dict = new Dictionary<ItemStates.ItemName, int>();
            foreach (var itemName in itemInventory.Where(itemName => !dict.TryAdd(itemName, 1)))
            {
                Debug.Log("Owns: " + itemName);
                dict[itemName] += 1;
            }
            
            foreach (var item in itemsPlaced.Where(item => !dict.TryAdd(item.key, 1)))
            {
                Debug.Log("Owns: " + item.key);
                dict[item.key] += 1;
            }
            
            return dict;
        }
        
        public List<ItemData> GetPlacedItems()
        {
            return itemsPlaced;
        }

    }
}