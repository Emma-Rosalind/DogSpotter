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


        public void StartEditModeWithObject(ItemHolder item)
        {
            InventoryManager.Instance.RemoveToPlace(item.key, editLayer.position);
            //spawn object
            var obj = Instantiate(itemPrefab, editLayer);
            var newItem = obj.GetComponent<UI_Item>();
            newItem.SpawnFromItem(item);
              
            //Reset scroll rect
            StartEditMode();
        }
        
        public void StartEditMode()
        {
            //send event
            _editMode = true;
        }
        
        private void EndEditMode()
        {
            //send event
            _editMode = false;
        }
        
    }
}
