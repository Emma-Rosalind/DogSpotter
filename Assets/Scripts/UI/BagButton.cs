using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class BagButton : MonoBehaviour
    {
        public void OnButtonClick()
        {
            PopupManager.Instance.Open(PopupManager.DialogName.Bag);
        }
    }
}