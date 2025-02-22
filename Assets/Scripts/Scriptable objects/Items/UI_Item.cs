using System.Collections.Generic;
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
        
        [SerializeField] private GameObject _editButtons;
        
        
        private bool _editMode = false;

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
            _editMode = false; 
            _editButtons.SetActive(false);
        }
        
        private void TurnOnEdit()
        {
            _editMode = true; 
            _editButtons.SetActive(true);
        }

        private void OnEditModeEntry()
        {
            //make selectable
        }
    }
}