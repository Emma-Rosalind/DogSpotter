using System;
using Managers;
using UI;
using UnityEngine;

namespace Dialogs
{
    public class BagDialog : Dialog
    {
        
        [SerializeField] private GameObject _itemSlotPrefab;
        [SerializeField] private Transform _LayoutParent;
        public override PopupManager.DialogName Name => PopupManager.DialogName.Bag;

        private void Start()
        {
            var allItems = InventoryManager.Instance._allItemsDic;
            foreach (var item in InventoryManager.Instance.itemInventory)
            {
                var obj =Instantiate(_itemSlotPrefab, _LayoutParent);
                obj.GetComponent<BagItem>().init(allItems[item]);
            }
        }
    }
}