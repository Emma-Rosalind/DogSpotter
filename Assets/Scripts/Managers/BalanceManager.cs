using UnityEngine;
using Events;
using Managers;

public class BalanceManager : MonoSingle<BalanceManager>
{

    public int balance {get; private set; }
    public int treatBalance {get; private set; }
    
    void Start()
    {
        balance = PlayerDataManager.Instance._playerData.Balance;
        treatBalance = PlayerDataManager.Instance._playerData.PremiumBalance;
        GameEvent.m_BalanceChange.Invoke();
    }

    public void BuyItem(int amount, bool isTreat)
    {
        if (isTreat)
        {
            SubTreatBalance(amount);
        }
        else
        {
            SubBalance(amount);
        }
    }

    private void AddBalance(int change, bool needsCloud = false)
    {
        balance += change;
        BalanceEvent();
        PlayerDataManager.Instance.UpdateBalance(balance, needsCloud);
    }
    
    private void AddTreatBalance(int change, bool needsCloud = false)
    {
        treatBalance += change;
        BalanceEvent();
        PlayerDataManager.Instance.UpdatePremiumBalance(treatBalance, needsCloud);
    }
    
    private void SubBalance(int change) => AddBalance(change * -1);
    private void SubTreatBalance(int change) => AddTreatBalance(change * -1);

    
    private void BalanceEvent()
    {
        GameEvent.m_BalanceChange.Invoke();
    }
}
