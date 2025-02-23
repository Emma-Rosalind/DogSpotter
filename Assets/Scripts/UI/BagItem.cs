using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BagItem : MonoBehaviour
    {
        [SerializeField] Image sprite;
        [SerializeField] Image shadow;

        public void init(ItemHolder itemInfo)
        {
            if (itemInfo.sprite == null)
            {
                DiableSlot();
                return;
            }

            sprite.sprite = itemInfo.sprite;
            shadow.sprite  = itemInfo.sprite;
            
        }


        private void DiableSlot()
        {
            sprite.sprite .GameObject().SetActive(false);
        }
    }
}