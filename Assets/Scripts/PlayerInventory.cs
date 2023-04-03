using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public PlayerObjectCarrier objectCarrier;
    private Dictionary<string, InventoryItem> _inventory = new Dictionary<string, InventoryItem>();
    private GameManager _gameManager;

    private void Start()
    {
        var items = inventoryPanel.GetComponentsInChildren<InventorySlot>();
      //  _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Push(Collectable collectable)
    {
        var key = collectable.collectableName;
        if (_inventory.ContainsKey(key))
        {
            _inventory[key].Add();
        }
        else
        {
            _inventory[key] = new InventoryItem(collectable);
        }

        UpdateUI();
    }

    public void UseCollectable(string key)
    {
        if (objectCarrier.CanCarry())
        {
            if (_inventory[key].Decrease())
            {
                objectCarrier.CarryObject(_inventory[key].Collectable);
                GameManager.instance.HideInventory();
                if (_inventory[key].Amount == 0)
                {
                    _inventory.Remove(key);
                }
            }
            else
            {
                _inventory.Remove(key);
            }
        }

        UpdateUI();
    }

    public void Show()
    {
        inventoryPanel.SetActive(true);
    }

    public void Hide()
    {
        inventoryPanel.SetActive(false);
    }

    void UpdateUI()
    {
        var inventorySlots = inventoryPanel.GetComponentsInChildren<InventorySlot>();
        for (var index = 0; index < _inventory.Count; index++)
        {
            var item = _inventory.ElementAt(index);
            inventorySlots[index].SetItem(item.Value, this);
        }

        for (var index = _inventory.Count; index < inventorySlots.Length; index++)
        {
            inventorySlots[index].Clear();
        }
    }
}