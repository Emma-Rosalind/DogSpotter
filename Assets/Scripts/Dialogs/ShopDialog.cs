using System;
using Managers;
using UI;
using UnityEngine;

namespace Dialogs
{
    public class ShopDialog : Dialog
    {
        
        [SerializeField] private GameObject _itemSlotPrefab;
        [SerializeField] private Transform _LayoutParent;
        public override PopupManager.DialogName Name => PopupManager.DialogName.Store;

        private void Start()
        {
            var allItems = InventoryManager.Instance._allItemsDic;
            foreach (var item in allItems)
            {
                var obj =Instantiate(_itemSlotPrefab, _LayoutParent);
                obj.GetComponent<ShopItem>().Init(item.Value);
            }
        }
    }
}