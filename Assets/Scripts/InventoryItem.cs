using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public Collectable Collectable;
    public int Amount;

    public InventoryItem(Collectable collectable)
    {
        Collectable = collectable;
        Amount = 1;
    }

    public void Add()
    {
        Amount++;
    }


    public bool Decrease()
    {
        if (Amount <= 0) return false;
        Amount--;
        return Amount >= 0;

    }
}