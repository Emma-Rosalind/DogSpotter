using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class PopupManager : MonoSingle<PopupManager>
{
    [Serializable]
    private class DialogSettings
    {
        public DialogName name;
        public GameObject prefab;
    }

    public enum DialogName
    {
        Settings = 0,
        Store = 1,
        Book = 3,
        Bag = 4,
        PurchaseConfirm = 5,
        
    }
    [SerializeField] private Transform popupContainer;
    [SerializeField] private GameObject scrim;
    [SerializeField] private List<DialogSettings> dialogs = new List<DialogSettings>();
    
    //All possible dialogs for easy lookup
    private Dictionary<DialogName, GameObject> _dialogIndex = new Dictionary<DialogName, GameObject>();
    //open dialogs and their refs
    private Dictionary<DialogName, GameObject> _openDialogs = new Dictionary<DialogName, GameObject>();

    void Start()
    {
        //create dictionary for easy access
        foreach (var dialog in dialogs)
        {
            _dialogIndex.Add(dialog.name, dialog.prefab);
        }
    }

    public GameObject Open(DialogName dialogName)
    {
        if (IsOpen(dialogName) || !_dialogIndex.ContainsKey(dialogName)) return null;
        
        var obj = Instantiate(_dialogIndex[dialogName], popupContainer);
        _openDialogs.Add(dialogName, obj);
        scrim.SetActive(true);
        return obj;
    }
    
    public void Close(DialogName dialogName)
    {
        if (!_openDialogs.ContainsKey(dialogName)) return;
        
        Destroy(_openDialogs[dialogName].gameObject);
        _openDialogs.Remove(dialogName);
        
        if(_openDialogs.Count <= 0) scrim.SetActive(false);
    }
    
    private bool IsOpen(DialogName dialogName)
    {
        return _openDialogs.ContainsKey(dialogName);
    }
}
