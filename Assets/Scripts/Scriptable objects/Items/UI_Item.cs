using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Scriptable_objects.Items
{
    public class UI_Item : MonoBehaviour
    {
        private ItemHolder _itemInfo;
        [SerializeField] private Image icon;
        [SerializeField] private Transform dogAnchor;
        
        [SerializeField] private List<CircleCollider2D> colliders = new List<CircleCollider2D>();
        [SerializeField] private DragableItem dragHandler;
        
        [SerializeField] private GameObject editButtons;
        

        private void Init()
        {
            icon.sprite = _itemInfo.sprite;
        }

        public void SpawnFromItem(ItemHolder info)
        {

            TurnOnEdit();
            _itemInfo = info;
            Init();
        }
        
        public void DestroyItem()
        {
            //Add back to inventory
        }
        
        public void PlaceItem()
        {
            TurnOffEdit();
        }

        private void TurnOffEdit()
        {
            editButtons.SetActive(false);
            dragHandler.enabled = false;
        }
        
        private void TurnOnEdit()
        {
            editButtons.SetActive(true);
            dragHandler.enabled = true;
        }

        private void OnEditModeEntry()
        {
            //make selectable
        }
        
        private void OnOtherItemSelected()
        {
            TurnOffEdit();
        }
    }
}