using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public Wings wings;
    public Reactor reactor;

    public ItemManager(Wings wings, Reactor reactor)
    {
        this.wings = wings;
        this.reactor = reactor;
    }
}

[CreateAssetMenu(fileName = "SOItemManager", menuName = "My Game/SOItemManager")]


public class SOItemManager : ScriptableObject
{
    [SerializeField] private Wings _defaultWings;
    [SerializeField] private Reactor _defaultReactor;

    private ItemManager _itemManager;

    public ItemManager Instance
    {
        get
        {

            if (_itemManager == null)
            {
                ResetInstance();
            }

            return _itemManager;

        }

        private set { }

    }

    public void ResetInstance()
    {
        _itemManager = new ItemManager(_defaultWings, _defaultReactor);
    }
}
