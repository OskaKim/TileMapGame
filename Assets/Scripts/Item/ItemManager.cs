using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDatabase
{
    public List<Item> AllItem;
}

public class ItemManager : MonoBehaviour
{
    #region singleton
    private static ItemManager instance;
    public static ItemManager Instance { get => instance; }
    #endregion

    [SerializeField] public List<RuntimeAnimatorController> itemAnimationList;
    [SerializeField] public ItemDatabase itemDatabase;

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

        itemDatabase = JsonImporter.LoadJsonFile<ItemDatabase>(Application.dataPath, "ItemDatabase");
    }

    // 인스펙터에서 설정한 값으로 저장하고 싶을 경우 사용.
    // TODO : 유니티 에디터에서 수정 및 버튼으로 사용 가능 하도록 하기.
    private void Save(string fileName, ItemDatabase data)
    {
        // 인덱스값 자동 지정
        var itemList = data.AllItem;
        for(int i = 0; i < itemList.Count; ++i)
        {
            itemList[i].index = i;
        }

        JsonExporter.CreateJsonFile<ItemDatabase>(Application.dataPath, fileName, data);
    }
}
