using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public static class GameEvent 
    {
        //Scene movement
        public static UnityEvent<bool> EditMode = new UnityEvent<bool> ();
        public static UnityEvent EditItemSelected = new UnityEvent ();
        
        
        //Balance
        public static UnityEvent m_BalanceChange = new UnityEvent();
        
    }
}