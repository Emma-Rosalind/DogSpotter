using Events;
using UnityEngine;
using Constants;

[CreateAssetMenu(fileName = "DogHolder", menuName = "Scriptable Objects/DogHolder")]
public class DogHolder : ScriptableObject
{
    public Sprite sittingSprite;
    public Sprite sleepingSprite;
    
    public DogStates.DogName nickName;
    
}
