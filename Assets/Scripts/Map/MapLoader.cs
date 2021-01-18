using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private ItemSpawner itemSpawner;

    // TODO : 차후에 데이터 IO를 통해 사용할 예정. 지금은 그냥 테스트용
    [SerializeField] public List<ItemSpawner.FieldItemSpawnInfo> fieldItemSpawnInfos;

    private void Awake()
    {
        foreach(var info in fieldItemSpawnInfos)
        {
            itemSpawner.FieldItemSpawn(info.name, info.pos, info.amount);
        }
    }
}
