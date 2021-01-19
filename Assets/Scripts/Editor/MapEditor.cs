#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using GameData;
using GameData.Json;
/// <summary>
/// 맵 작성용 에디터
/// 기능 : 필드 아이템, NPC 등 위치 수정 등 작성 예정
/// </summary>
[CustomEditor(typeof(MapLoader))]
public class MapEditor : Editor
{
    private MapLoader mapLoader = null;
    
    private static bool isMapEditMode = true;
    private const string itemDataPath = "Data/Item";
    private List<Item> allItems = null;
    private List<string> itemNameList = null;
    private static int selectedIndex = 0;
    private static List<GameObject> createdItems = new List<GameObject>();

    // TODO : 다른 오브젝트를 클릭하고 돌아왔을때 이전의 상태가 보전되면 좋겠음
    private void OnEnable()
    {
        mapLoader = (MapLoader)target;

        // 여기서 아이템 전체 리스트 습득
        var itemDatabase = JsonImporter.LoadJsonFile<ItemDatabase>(itemDataPath, "ItemDatabase");
        allItems = itemDatabase.AllItem;

        itemNameList = new List<string>();
        foreach (var item in allItems)
        {
            itemNameList.Add(item.itemName);
        }
    }

    public override void OnInspectorGUI()
    {
        isMapEditMode = EditorGUILayout.Toggle("MapEditMode", isMapEditMode);

        if (!isMapEditMode)
        {
            base.OnInspectorGUI();
            return;
        }

        selectedIndex = EditorGUILayout.Popup("select item", selectedIndex, itemNameList.ToArray());

        if (GUILayout.Button("create item")) CreateItem();
        if (GUILayout.Button("Destroy All items")) DestroyItems();
        if (GUILayout.Button("Destroy Last item")) DestroyLastItem();
    }
    private void CreateItem()
    {
        if(createdItems == null) createdItems = new List<GameObject>();

        var newItemName = itemNameList[selectedIndex];
        // 여기서 선택한 아이템을 생성
        var newItem = new GameObject("item_" + createdItems.Count);
        var sr = newItem.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load($"Items/Origin/{newItemName}Moving", typeof(Sprite)) as Sprite;
        createdItems.Add(newItem);
    }

    private void DestroyItems()
    {
        if (createdItems == null) return;

        foreach (var i in createdItems)
        {
            if (i == null) continue;
            DestroyImmediate(i);
        }
        createdItems = new List<GameObject>();
    }

    private void DestroyLastItem()
    {
        if (createdItems == null) return;
        if (createdItems.Count == 0)
        {
            createdItems = new List<GameObject>();
            return;
        }

        var lastIndex = createdItems.Count - 1;
        DestroyImmediate(createdItems[lastIndex]);
        createdItems.RemoveAt(lastIndex);
    }
}

#endif