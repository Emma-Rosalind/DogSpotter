using System;
using Managers;
using Scenes;
using TMPro;
using UI;
using UnityEngine;

namespace Dialogs
{
    public class PurchaseConfDialog : Dialog
    {
        
        [SerializeField] private TextMeshProUGUI body;
        public override PopupManager.DialogName Name => PopupManager.DialogName.PurchaseConfirm;
        
        private const string textBase = "Would you like to purchase ";
        private ItemHolder _item;

        public void Init(ItemHolder itemHolder)
        {
            _item = itemHolder;
            var bodyText = textBase;
            var numPrice = _item.GetPrice();
            var isTreat = _item.PriceInTreats();

            bodyText += $"{_item.name} for {numPrice} {(isTreat ? "treats" : "good boy points" )}?";
            body.text = bodyText;
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
            PopupManager.Instance.Close(Name);
        }
    }
}