using Events;
using UnityEngine;
using Constants;

[CreateAssetMenu(fileName = "ItemHolder", menuName = "Scriptable Objects/ItemHolder")]
public class ItemHolder : ScriptableObject
{
    public Sprite sprite;
    
    public ItemStates.ItemName key;
    public string fullName;
    public string desciption;

    public int coinPrice;
    public int treatPrice;

    public int GetPrice()
    {
        return treatPrice > 0 ? treatPrice : coinPrice;
    }
    
    public bool PriceInTreats()
    {
        return treatPrice > 0;
    }

}
