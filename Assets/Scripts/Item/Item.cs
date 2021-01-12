using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    public enum ItemType
    {
        Equipment,
        Consumables,
        Etc,
    }

    // 각 화폐에 해당하는 인덱스 값
    public enum CurrencyType
    {
        Coin = 5,
        BlueCrystal = 3,
        GreenCrystal = 6,
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


    [System.Serializable]
    public class PossessionItemInfo
    {
        public int index;
        public int itemIndex;
        public int amount;

        public PossessionItemInfo(int index, int itemIndex, int amount)
        {
            this.index = index;
            this.itemIndex = itemIndex;
            this.amount = amount;
        }
    }
}