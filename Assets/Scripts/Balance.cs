using Events;
using TMPro;
using UnityEngine;

namespace Components.UI
{
    public class Balance : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_Text;
        void Start()
        {
            GameEvent.m_BalanceChange.AddListener(UpdateBalace);
            UpdateBalace(BalanceManager.Instance.balance);
        }

        private void OnDestroy()
        {
            GameEvent.m_BalanceChange.RemoveListener(UpdateBalace);
        }
        
        void UpdateBalace()
        {
            UpdateBalace(BalanceManager.Instance.balance);
        }
        
        void UpdateBalace(int newBalance)
        {
            m_Text.text = newBalance.ToString("C0");
        }
    }
}