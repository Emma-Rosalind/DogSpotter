using System;
using Events;
using Managers;
using Scenes;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TopBar : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI balanceText;
        [SerializeField] TextMeshProUGUI balanceTreatText;
        private void Start()
        {
            GameEvent.m_BalanceChange.AddListener(OnBalanceChange);
            balanceText.text = PlayerDataManager.Instance._playerData.Balance.ToString();
            balanceTreatText.text = PlayerDataManager.Instance._playerData.PremiumBalance.ToString();
        }

        void OnDestroy()
        {
            GameEvent.m_BalanceChange.RemoveListener(OnBalanceChange);
        }

        private void OnBalanceChange()
        {
            balanceText.text = BalanceManager.Instance.balance.ToString();
            balanceTreatText.text = BalanceManager.Instance.treatBalance.ToString();
        }
    }
}