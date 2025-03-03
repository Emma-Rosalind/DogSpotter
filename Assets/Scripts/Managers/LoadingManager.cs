using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class LoadingManager : MonoSingle<LoadingManager>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool _ready = false;
    private Queue<Action> _LoadingSteps = new Queue<Action>();
    void Start()
    {
        _LoadingSteps.Enqueue(ReadLocalPlayerData);

        while (_LoadingSteps.Count > 0)
        {
            _LoadingSteps.Dequeue()();
        }

        _ready = true;
    }

    public bool IsReady()
    {
        return _ready;
    }

    private void ReadLocalPlayerData()
    {
        PlayerDataManager.Instance.LoadPlayerData();
    }
    
}
