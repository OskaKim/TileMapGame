using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumables,
    Etc
}

[System.Serializable]
public class Item
{
    public int index;
    public ItemType itemType;
    public string itemName;
    public string itemSubscribe;

    public bool Use()
    {
        return false;
    }

    public void DeepCopyParam(Item _item)
    {
        index = _item.index;
        itemType = _item.itemType;
        itemName = _item.itemName;
        itemSubscribe = _item.itemSubscribe;
    }

}
