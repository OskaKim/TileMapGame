using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

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
            if (itemDatabase == null)
            {
                itemDatabase = ItemManager.Instance.itemDatabase;
            }

            return itemDatabase;
        }
    }

    private PossessionItemDatabase possessionItemDatabase;

    private void Awake()
    {
        possessionItemDatabase = ItemManager.Instance.possessionItemDatabase;

        pool = new ObjectPool<FieldItem>(
        () => {
            return Instantiate(prefab).GetComponent<FieldItem>();
        },
        (item) => {
            Destroy(item.gameObject);
        });
    }

    public void FieldItemSpawn(string itemName, Vector2 pos)
    {
        FieldItemSpawn(ItemDatabase.GetByName(itemName).index, pos);
    }
    public void FieldItemSpawn(int itemIndex, Vector2 pos)
    {
        // 아이템 설정
        Spawn(pos).SetItem(ItemDatabase.AllItem[itemIndex],
        // 아이템 습득시 처리 콜백
        (item) =>
        {
            pool.PushObject(item.gameObject);
            possessionItemDatabase.Add(item.itemInfo.index, 1);
        });
    }

    public override FieldItem Spawn(Vector2 pos)
    {
        var obj = pool.PopObject();
        var tranf = obj.transform;
        tranf.localPosition = pos;
        tranf.localRotation = Quaternion.identity;
        return obj;
    }
}
