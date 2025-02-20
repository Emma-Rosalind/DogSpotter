using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public static class GameEvent 
    {
        //Scene movement
        public static UnityEvent m_ToKitchen = new UnityEvent();
        public static UnityEvent m_ToCafe = new UnityEvent();
        
        
        //Balance
        public static UnityEvent m_BalanceChange = new UnityEvent();
        
    }
}