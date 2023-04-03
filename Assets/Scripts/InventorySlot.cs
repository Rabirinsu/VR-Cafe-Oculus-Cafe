using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private InventoryItem _item;
    public Image image;
    public Text nameText;
    private PlayerInventory _inventory;


    public void SetItem(InventoryItem item, PlayerInventory inventory)
    {
        _item = item;
        _inventory = inventory;
        image.gameObject.SetActive(true);
        nameText.gameObject.SetActive(true);
        image.sprite = item.Collectable.icon;
        nameText.text = item.Collectable.collectableName + "(" + item.Amount + ")";
    }

    public void Clear()
    {
        _item = null;
        image.sprite = null;
        nameText.text = "";
        image.gameObject.SetActive(false);
        nameText.gameObject.SetActive(true);
    }

    public void Use()
    {
        if ( _item != null)//OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) &&
        {
            _inventory.UseCollectable(_item.Collectable.collectableName);
            GameManager.instance.HideInventory();
        }
    }
}