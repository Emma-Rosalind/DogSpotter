using System.Collections.Generic;
using UnityEngine;
using Events;
using UnityEngine.UI;

namespace Scenes
{
    public class GameView : MonoSingle<GameView>
    {
       
        [SerializeField] Transform _itemLayer;
        [SerializeField] Transform _editLayer;
        
        [SerializeField] ScrollRect _scroll;



        private bool _editMode = false;


        public void StartEditModeWithObject()
        {
            //spawn object
            
            //Reset scroll rect
            StartEditMode();
        }
        
        public void StartEditMode()
        {
            //send event
            _editMode = true;
        }
        
        private void EndEditMode()
        {
            //send event
            _editMode = false;
        }
        
    }
}
