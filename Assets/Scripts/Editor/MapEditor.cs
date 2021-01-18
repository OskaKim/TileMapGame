#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using GameData;
using GameData.Json;
/// <summary>
/// �� �ۼ��� ������
/// ��� : �ʵ� ������, NPC �� ��ġ ���� �� �ۼ� ����
/// </summary>
[CustomEditor(typeof(MapLoader))]
public class MapEditor : Editor
{
    private MapLoader mapLoader = null;
    
    private bool isShowPreview = true;
    private bool isMapEditMode = true;
    private const string itemDataPath = "Data/Item";
    private List<Item> allItems = null;
    private List<string> itemNameList = null;
    private int selectedIndex = 0;
    private List<GameObject> createdItems = null;

    // TODO : �ٸ� ������Ʈ�� Ŭ���ϰ� ���ƿ����� ������ ���°� �����Ǹ� ������
    private void OnEnable()
    {
        mapLoader = (MapLoader)target;

        createdItems = new List<GameObject>();

        // ���⼭ ������ ��ü ����Ʈ ����
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

        isShowPreview = EditorGUILayout.Toggle("show preview", isShowPreview);
        selectedIndex = EditorGUILayout.Popup("select item", selectedIndex, itemNameList.ToArray());

        if (GUILayout.Button("create item")) CreateItem();
        if (GUILayout.Button("Destroy All items")) DestroyItems();
        if (GUILayout.Button("Destroy Last item")) DestroyLastItem();
    }
    private void CreateItem()
    {
        var newItemName = itemNameList[selectedIndex];
        // ���⼭ ������ �������� ����
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

        var lastIndex = createdItems.Count - 1;
        DestroyImmediate(createdItems[lastIndex]);
        createdItems.RemoveAt(lastIndex);
    }
}

#endif