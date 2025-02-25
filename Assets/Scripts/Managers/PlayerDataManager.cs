using System.Collections.Generic;
using Events;
using Newtonsoft.Json;
using UnityEngine;

namespace Managers
{
    public class PlayerDataManager : MonoSingle<PlayerDataManager>
    {
        
        //Player data that is periodicly uploaded
        public class PlayerData
        {
            public int Balance;
            public int PremiumBalance;
            public Dictionary<DogStates.DogName, int> DogCounter = new Dictionary<DogStates.DogName, int>();
            public Dictionary<ItemStates.ItemName, int> ItemInventory = new Dictionary<ItemStates.ItemName, int>();
        }
        
        //Extra data that is only stored locally
        public class TransformPlayerData
        {
            public Dictionary<DogStates.DogName, int[]> DogPositions = new Dictionary<DogStates.DogName, int[]>();
            public List<InventoryManager.ItemData> ItemInventory = new List<InventoryManager.ItemData>();
        }
        
        private const string  TransformData = "TransformPlayerData";
        private const string  Player_Data = "PlayerData";
        private const string  CloudTs = "LastCloudSave";
        
        public PlayerData _playerData {private set; get; }
        public TransformPlayerData _transformPlayerData {private set; get; }


        public void UpdateBalance(int balance, bool needsCloud = false)
        {
            _playerData.Balance = balance;
            StorePlayerData(needsCloud);
        }
        
        public void UpdatePremiumBalance(int balance, bool needsCloud = false)
        {
            _playerData.PremiumBalance = balance;
            StorePlayerData(needsCloud);
        }
        
        public void UpdateItems(bool needsCloud = false)
        {
            _playerData.ItemInventory = InventoryManager.Instance.GetOwnedItems();
            _transformPlayerData.ItemInventory = InventoryManager.Instance.GetPlacedItems();
            StorePlayerData(needsCloud);
        }
        
        public void UpdateDogPos(DogStates.DogName dogName, Transform pos)
        {
           var array = new int[2] {(int)pos.position.x, (int)pos.position.y};
            _transformPlayerData.DogPositions[dogName] = array;
            StorePlayerData();
        }
        
        public void RemoveDog(DogStates.DogName dogName)
        {
            _transformPlayerData.DogPositions.Remove(dogName);
            StorePlayerData();
        }

        public void LoadPlayerData()
        {
            if (ReadLocalPlayerData()) return;
            
            //may be a new user, check online
            if (ReadOnlinePlayerData())
            {
                //copy online data to local
                StorePlayerData(false);
            }
            else
            {
                //make a new user
                _playerData = new PlayerData()
                {
                    Balance = 200,
                    PremiumBalance = 10,
                };
                _transformPlayerData = new TransformPlayerData();
                StorePlayerData(false);
                Debug.Log("New User Made");
            }
        }


        // Returns false if it is a new user
        private bool ReadLocalPlayerData()
        {
            if (!PlayerPrefs.HasKey(Player_Data)) return false;
            
            _playerData= JsonConvert.DeserializeObject<PlayerData>(PlayerPrefs.GetString(Player_Data));
            _transformPlayerData = JsonConvert.DeserializeObject<TransformPlayerData>(PlayerPrefs.GetString(TransformData));
            Debug.Log("Loaded Player Data Locally");
            Debug.Log("Loaded Transforms: " + PlayerPrefs.GetString(TransformData));
            Debug.Log("Loaded Player: " + PlayerPrefs.GetString(Player_Data));
            return true;
        }
        
        private bool ReadOnlinePlayerData()
        {
            return false;
        }

        private void StorePlayerData(bool forceOnlineSave = true)
        {
            Debug.Log("New data stored");
            var playerString = JsonConvert.SerializeObject(_playerData);
            var posString = JsonConvert.SerializeObject(_transformPlayerData);
            Debug.Log("Stored Player: " + playerString);
            Debug.Log("Stored Transforms: " + posString);
            PlayerPrefs.SetString(Player_Data, playerString);
            PlayerPrefs.SetString(TransformData, posString);
            PlayerPrefs.Save();

            if (forceOnlineSave || TimeToUpdateOnline())
            {
                StorePlayerDataOnline();
            }

        }

        private bool TimeToUpdateOnline()
        {
            return false;
        }

        private void StorePlayerDataOnline()
        {
            //Update online player data

        }
        
    }
}