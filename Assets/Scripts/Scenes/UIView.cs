using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using UnityEngine;
using UnityEngine.UI;
using Events;
using Unity.VisualScripting;

namespace Scenes
{
    public class UIView : MonoSingle<UIView>
    {
        [SerializeField] private GameObject _topBar;
        [SerializeField] private GameObject _sideBar;
        [SerializeField] private GameObject _editBar;

        private void Start()
        {
            GameEvent.EditMode.AddListener(OnEditChange);
        }

        private void OnEditChange(bool value)
        {
            if (value) StartEditMode();
            if (!value) EndEditMode();
        }

        private void StartEditMode()
        {
            _topBar.SetActive(false);
            _sideBar.SetActive(false);
            _editBar.SetActive(true);
        }
        
        private void EndEditMode()
        {
            _topBar.SetActive(true);
            _sideBar.SetActive(true);
            _editBar.SetActive(false);
        }

        public void OnEditBackClicked()
        {
            GameView.Instance.EndEditMode();
        }
        
        public void OnShopClicked()
        {
            PopupManager.Instance.Open(PopupManager.DialogName.Store);
        }

    }
}

