using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs
{
    public abstract class Dialog : MonoBehaviour
    {
        [SerializeField] Button CloseButton;
        
        public abstract PopupManager.DialogName Name { get; }

        void Awake()
        {
            CloseButton.onClick.AddListener(Close);
        }
        
        protected void CloseWithAnimation()
        {
            Close();
        }

        protected void Close()
        {
            PopupManager.Instance.Close(Name);
        }
    }
}