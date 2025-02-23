using System;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Scriptable_objects.Items
{
    public class UI_Item : MonoBehaviour
    {
       
        [SerializeField] private Image icon;
        [SerializeField] private Transform dogAnchor;
        
        [SerializeField] private DragableItem dragHandler;
        [Header("Buttons")]
        [SerializeField] private GameObject editButtons;
        [SerializeField] private GameObject selectButton;
        [Header("Colors")]
        [SerializeField] private Color selectColor;
        [SerializeField] private Color deleteColor;

        private ItemHolder _itemInfo;
        private int _id;
        private bool collided = false;
        private bool editing = false;
        

        private void Init()
        {
            icon.sprite = _itemInfo.sprite;
        }

        public void SpawnFromItem(ItemHolder info, int id)
        {
            TurnOnEdit();
            _itemInfo = info;
            _id = id;
            Init();
        }
        

        private void TurnOffEdit()
        {
            editButtons.SetActive(false);
            dragHandler.enabled = false;
            icon.color = Color.white;
            editing = false;
        }
        
        private void TurnOnEdit()
        {
            editButtons.SetActive(true);
            dragHandler.enabled = true;
            icon.color = (CheckForCollisions() ? deleteColor : selectColor );
            editing = true;
        }

        private void OnEditModeEntry()
        {
            //make selectable
        }
        
        private void OnOtherItemSelected()
        {
            TurnOffEdit();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (editing)
            {
                collided = true;
                selectButton.gameObject.SetActive(false);
            }
            icon.color = deleteColor;
            
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (editing)
            {
                collided = false;
                selectButton.gameObject.SetActive(true);
            }

            icon.color = (editing ? selectColor : Color.white);
        }

        private bool CheckForCollisions()
        {
            return false;
        }

        #region Buttons

        public void DestroyItem()
        {
            InventoryManager.Instance.AddToInventoryFromFloor(_itemInfo.key, _id);
            Destroy(gameObject);
        }
        
        public void PlaceItem()
        {
            TurnOffEdit();
        }

        #endregion
    }
}