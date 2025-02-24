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
