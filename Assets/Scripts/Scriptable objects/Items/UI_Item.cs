using System;
using System.Collections.Generic;
using Events;
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
        [SerializeField] private GameObject confirmButton;
        [SerializeField] private Button selectButton;
        [Header("Colors")]
        [SerializeField] private Color selectColor;
        [SerializeField] private Color deleteColor;

        private ItemHolder _itemInfo;
        private int _id;
        private bool collided = false;
        private bool editing = false;
        
        
        private void Start()
        {
            GameEvent.EditMode.AddListener(OnEditChange);
        }

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
        
        
        private void SelectForEdit()
        {
            GameEvent.EditItemSelected.Invoke();
            TurnOnEdit();
        }
        
        private void OnOtherItemSelected()
        {
            TurnOffAndAttemptPlace();
            selectButton.enabled = true;
        }

        private void TurnOffEdit()
        {
            editButtons.SetActive(false);
            dragHandler.enabled = false;
            icon.color = Color.white;
            editing = false;
            GameEvent.EditItemSelected.RemoveListener(OnOtherItemSelected);
        }
        
        private void TurnOffAndAttemptPlace()
        {
            TurnOffEdit();
            if (!collided)
            {
                PlaceItem();
            }
            else
            {
                DestroyItem();
            }
        }
        
        private void TurnOnEdit()
        {
            editButtons.SetActive(true);
            dragHandler.enabled = true;
            icon.color = selectColor;
            editing = true;
            GameEvent.EditItemSelected.AddListener(OnOtherItemSelected);
        }
        
        private void OnEditChange(bool value)
        {
            if (value)
            {
                selectButton.enabled = true;
                if (!editing)
                {
                    selectButton.onClick.AddListener(SelectForEdit);
                }
            }
            else if(editing)
            {
                selectButton.enabled = false;
                TurnOffAndAttemptPlace();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (editing)
            {
                collided = true;
                confirmButton.gameObject.SetActive(false);
            }
            icon.color = deleteColor;
            
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (editing)
            {
                collided = false;
                confirmButton.gameObject.SetActive(true);
            }

            icon.color = (editing ? selectColor : Color.white);
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
            InventoryManager.Instance.UpdateFromFloor(_itemInfo.key, _id, transform.position);
        }

        #endregion
    }
}