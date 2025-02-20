using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class SettingsButton : MonoBehaviour
    {
        public void OnButtonClick()
        {
            PopupManager.Instance.Open(PopupManager.DialogName.Settings);
        }
    }
}