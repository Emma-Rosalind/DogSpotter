using UnityEngine;
using Events;

public class BalanceManager : MonoSingle<BalanceManager>
{

    public int balance {get; private set; }
    
    public float ToppingsPrice {get; private set; }

    void Start()
    {
        balance = PlayerPrefs.GetInt("balance");
        PlayerPrefs.SetInt("Balance", 0);
    }

    public void BuyTopping(float amount)
    {
        ToppingsPrice += amount;
    }

    public void AddBalance(int change)
    {
        balance = balance + change;
        UpdateBalance(balance);
    }
    
    public void SubBalance(int change)
    {
        balance = balance - change;
        UpdateBalance(balance);
    }
    
    private void UpdateBalance(int newBalance)
    {
        PlayerPrefs.SetInt("Balance", newBalance);
        PlayerPrefs.Save();
        
        GameEvent.m_BalanceChange.Invoke();
    }
}
