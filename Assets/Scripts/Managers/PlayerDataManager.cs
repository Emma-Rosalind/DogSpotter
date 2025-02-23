using System.Collections.Generic;
using Events;
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
        
        private const string  Transform_Data = "TransformPlayerData";
        private const string  Player_Data = "PlayerData";
        private const string  Cloud_TS = "LastCloudSave";
        
        public PlayerData _playerData {private set; get; }
        public TransformPlayerData _transformPlayerData {private set; get; }


        public void UpdateBalance(int balance)
        {
            _playerData.Balance = balance;
            StorePlayerData();
        }
        
        public void UpdatePremiumBalance(int balance)
        {
            _playerData.PremiumBalance = balance;
            StorePlayerData();
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
        
        public bool LoadPlayerData()
        {
            if (!ReadLocalPlayerData())
            {
                //Try to load from online
                _playerData = new PlayerData();
                _transformPlayerData = new TransformPlayerData();
                return false;
            }
            else
            {
                return true;
            }
        }


        // Returns false if it is a new user
        private bool ReadLocalPlayerData()
        {
            if (!PlayerPrefs.HasKey(Player_Data)) return false;
            
            _playerData= JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(Player_Data));
            _transformPlayerData = JsonUtility.FromJson<TransformPlayerData>(PlayerPrefs.GetString(Transform_Data));
            return true;
        }

        private void StorePlayerData(bool forceOnlineSave = true)
        {
            var playerString = JsonUtility.ToJson(_playerData);
            var posString = JsonUtility.ToJson(_transformPlayerData);
            
            PlayerPrefs.SetString(Player_Data, playerString);
            PlayerPrefs.SetString(Transform_Data, posString);
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