using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Spawner<FieldItem>
{
    // 필드 아이템 스폰시 필요한 정보 모음
    [System.Serializable]
    public struct FieldItemSpawnInfo
    {
        public string name;
        public Vector2 pos;
    }

    private ItemDatabase itemDatabase;
    private ItemDatabase ItemDatabase
    {
        get
        {
            if(itemDatabase == null)
            {
                itemDatabase = ItemManager.Instance.itemDatabase;
            }

            return itemDatabase;
        }
    }

    public void FieldItemSpawn(string itemName, Vector2 pos)
    {
        FieldItemSpawn(ItemDatabase.AllItem.FindIndex(x => x.itemName == itemName), pos);
    }
    public void FieldItemSpawn(int itemIndex, Vector2 pos)
    {
        Spawn(pos).SetItem(ItemDatabase.AllItem[itemIndex]);
    }
}
