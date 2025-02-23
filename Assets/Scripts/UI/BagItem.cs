using Managers;
using Scenes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class BagItem : MonoBehaviour
    {
        [SerializeField] Image sprite;
        [SerializeField] Image shadow;

        private ItemHolder item;

        public void Init(ItemHolder itemInfo)
        {
            item = itemInfo;
            if (itemInfo.sprite == null)
            {
                DiableSlot();
                return;
            }

            sprite.sprite = itemInfo.sprite;
            shadow.sprite  = itemInfo.sprite;
            
        }
        
        //Called by button
        public void PlaceItem()
        {
            //Item is removed from invention by the game view
            GameView.Instance.StartEditModeWithObject(item);
            PopupManager.Instance.Close(PopupManager.DialogName.Bag);
        }


        private void DiableSlot()
        {
            sprite.sprite .GameObject().SetActive(false);
        }
        
    }
}