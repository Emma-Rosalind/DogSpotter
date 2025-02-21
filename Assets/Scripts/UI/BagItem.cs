using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class BagItem : MonoBehaviour
    {
        [SerializeField] Sprite _sprite;


        public void init(ItemHolder itemInfo)
        {
            if (itemInfo.sprite == null)
            {
                DiableSlot();
                return;
            }

            _sprite = itemInfo.sprite;
        }


        private void DiableSlot()
        {
            _sprite.GameObject().SetActive(false);
        }
    }
}