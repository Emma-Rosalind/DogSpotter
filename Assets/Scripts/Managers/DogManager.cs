using System;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Managers
{
    public class DogManager : MonoSingle<DogManager>
    {
        [SerializeField] public List<DogHolder> _allDogs = new List<DogHolder>();
        [HideInInspector] public Dictionary<DogStates.DogName, DogHolder> _allDogDic = new Dictionary<DogStates.DogName, DogHolder>();
        
        
        public class DogData
        {
            public DogStates.DogName key;
            public DateTime endTime;
        }


        void Start()
        {
            //Create dictionary of items
            foreach (var item in _allDogs)
            {
                _allDogDic.Add(item.key, item);
            }
        }

        //Returns missed dog since last login
        public int LoadDogs()
        {
            Debug.Log("Loading Dogs");
            return 1;
        }
        
        public void AddNewDogs()
        {
            Debug.Log("Loading Dogs");
        }
    }
}