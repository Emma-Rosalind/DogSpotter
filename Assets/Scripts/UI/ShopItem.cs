using Dialogs;
using Managers;
using Scenes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] Image sprite;
        [SerializeField] Image shadow;

        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private Image coin;
        [SerializeField] private Sprite bone;

        private ItemHolder item;
        private int numPrice;
        private bool isTreat = false;

        public void Init(ItemHolder itemInfo)
        {
            item = itemInfo;
            if (itemInfo.sprite == null)
            {
                DisableSlot();
                return;
            }

            sprite.sprite = itemInfo.sprite;
            shadow.sprite  = itemInfo.sprite;
            SetPrice();
        }

        private void SetPrice()
        {
            numPrice = item.GetPrice();
            isTreat = item.treatPrice > 0 ? true : false;
            price.text = numPrice.ToString("f0");
        
            if (item.treatPrice > 0)
            {
                coin.sprite = bone;
            }
        }

        //Called by button
        public void BuyItem()
        {
            if (isTreat && numPrice > BalanceManager.Instance.treatBalance) return;
            if (!isTreat && numPrice > BalanceManager.Instance.balance) return;
            
            var pop = PopupManager.Instance.Open(PopupManager.DialogName.PurchaseConfirm);
            if (pop != null)
            {
                pop.GetComponent<PurchaseConfDialog>().Init(item);
            }
        }


        private void DisableSlot()
        {
            sprite.sprite .GameObject().SetActive(false);
        }
        
    }
}