using System.Collections.Generic;
using UnityEngine;
using Events;
using Managers;
using Scriptable_objects.Items;
using UnityEngine.UI;

namespace Scenes
{
    public class GameView : MonoSingle<GameView>
    {
       
        [SerializeField] Transform itemLayer;
        [SerializeField] Transform editLayer;
        
        [SerializeField] ScrollRect scroll;
        
        [SerializeField] GameObject itemPrefab;



        private bool _editMode = false;
        private float _ogVertPos;
        private float _ogHoriPos;

        void Start()
        {
            _ogVertPos = scroll.verticalNormalizedPosition;
            _ogHoriPos = scroll.horizontalNormalizedPosition;
    
            //Gameview loading steps, move later
            var placementData = InventoryManager.Instance.LoadInventory();
            PlaceItemsOnStart(placementData);
            var missedDogs =  DogManager.Instance.LoadDogs();
            DogManager.Instance.AddNewDogs();
            //send missed dogs to ui view
        }

        public void StartEditModeWithObject(ItemHolder item)
        {
            var id = InventoryManager.Instance.RemoveToPlace(item.key, editLayer.position);
            //spawn object
            var obj = Instantiate(itemPrefab, editLayer);
            var newItem = obj.GetComponent<UI_Item>();
            newItem.SpawnFromItem(item, id);
              
            //Reset scroll rect
            scroll.verticalNormalizedPosition = _ogVertPos;
            scroll.horizontalNormalizedPosition = _ogHoriPos;
            StartEditMode();
        }
        
        public void PlaceItemsOnStart(List<InventoryManager.ItemData> itemsPlaced )
        {
            foreach (var item in itemsPlaced)
            {
                //spawn object
                var obj = Instantiate(itemPrefab, editLayer);
                obj.transform.position = new Vector3(item.position[0], item.position[1]);
                var newItem = obj.GetComponent<UI_Item>();
                newItem.SpawnOnStart(InventoryManager.Instance.GetItemHolder(item.key), item.id);
            }
        }
        
        public void StartEditMode()
        {
            GameEvent.EditMode.Invoke(true);
            _editMode = true;
        }
        
        public void EndEditMode()
        {
            GameEvent.EditMode.Invoke(false);
            _editMode = false;
        }
        
    }
}
