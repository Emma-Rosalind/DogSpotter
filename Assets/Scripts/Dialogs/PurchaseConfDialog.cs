using System;
using Managers;
using Scenes;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs
{
    public class PurchaseConfDialog : Dialog
    {
        
        [SerializeField] private TextMeshProUGUI body;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private Image priceIcon;
        [SerializeField] private Sprite treat;
        public override PopupManager.DialogName Name => PopupManager.DialogName.PurchaseConfirm;
        
        private const string textBase = "Would you like to buy ";
        private ItemHolder _item;

        public void Init(ItemHolder itemHolder)
        {
            _item = itemHolder;
            var bodyText = textBase;
            var numPrice = _item.GetPrice();

            bodyText += $"{_item.name} for ";
            body.text = bodyText;
            price.text = numPrice.ToString();
            if (_item.PriceInTreats()) priceIcon.sprite = treat;
                
            PopupManager.Instance.Hide(PopupManager.DialogName.Store);
        }

        public void OnYesClicked()
        {
            BalanceManager.Instance.BuyItem(_item.GetPrice(),  _item.PriceInTreats());
            PopupManager.Instance.Close(PopupManager.DialogName.Store);
            PopupManager.Instance.Close(Name);
            InventoryManager.Instance.AddToInventoryFromShop(_item.key);
            GameView.Instance.StartEditModeWithObject(_item);
        }
        
        public void OnNoClicked()
        {
            PopupManager.Instance.Show(PopupManager.DialogName.Store);
            PopupManager.Instance.Close(Name);
        }
    }
}