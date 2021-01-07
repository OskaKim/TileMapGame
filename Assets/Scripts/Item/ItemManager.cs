using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData.Json;

namespace GameData
{
    // 아이템 목록
    [System.Serializable]
    public class ItemDatabase
    {
        public List<Item> AllItem;

        // 인덱스 습득
        public Item GetByName(string name)
        {
            return AllItem.Find((x) => x.itemName == name);
        }
    }

    // 소유 아이템
    // TODO : 복수의 캐릭터 대응시키기
    [System.Serializable]
    public class PossessionItemDatabase
    {
        public List<PossessionItemInfo> ItemInfos;

        public PossessionItemInfo GetByItemIndex(int itemIndex)
        {
            return ItemInfos.Find((x) =>  x.itemIndex == itemIndex);
        }
    }

    public class ItemManager : MonoBehaviour
    {
        #region singleton
        private static ItemManager instance;
        public static ItemManager Instance { get => instance; }
        #endregion
        [SerializeField] public List<RuntimeAnimatorController> itemAnimationList;
        [SerializeField] public ItemDatabase itemDatabase;
        [SerializeField] public PossessionItemDatabase possessionItemDatabase;

        private const string itemDataPath = "Data/Item";

        private void Awake()
        {
            #region singleton
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
            #endregion

            itemDatabase = JsonImporter.LoadJsonFile<ItemDatabase>(itemDataPath, "ItemDatabase");
            possessionItemDatabase = JsonImporter.LoadJsonFile<PossessionItemDatabase>(itemDataPath, "PossessionItemDatabase");

        }

        // 인스펙터에서 설정한 값으로 저장하고 싶을 경우 사용.
        // TODO : 유니티 에디터에서 수정 및 버튼으로 사용 가능 하도록 하기.
        private void Save(string fileName, ItemDatabase data)
        {
            // 인덱스값 자동 지정
            var itemList = data.AllItem;
            for (int i = 0; i < itemList.Count; ++i)
            {
                itemList[i].index = i;
            }

            JsonExporter.CreateJsonFile<ItemDatabase>(itemDataPath, fileName, data);
        }
    }
}