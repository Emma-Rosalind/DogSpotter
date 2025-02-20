using UnityEngine;

namespace Components.UI.Dogs
{
    public class Dog : MonoBehaviour
    {
        [SerializeField] private Sprite dogSprite;
        [SerializeField] private Sprite hatSprite;
        
        private DogHolder info;

        public void init(DogHolder info, bool sitting = true)
        {
            this.info = info;
            if (sitting)
            {
                SetUpSitting();
            }
            else
            {
                SetUpSleeping();
            }
        }


        private void SetUpSitting()
        {
            dogSprite = info.sittingSprite;
        }
        
        private void SetUpSleeping()
        {
            dogSprite = info.sleepingSprite;
        }
    }
}