#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameData;
using GameData.Json;
using UnityEngine.Tilemaps;

/// <summary>
/// �� �ۼ��� ������
/// ��� : �ʵ� ������, NPC �� ��ġ ���� �� �ۼ� ����
/// </summary>
[CustomEditor(typeof(MapLoader))]
public class MapEditor : Editor
{
    private MapLoader mapLoader = null;
    private Tilemap[] tileMaps = null;

    private static bool isMapEditMode = true;
    private const string itemDataPath = "Data/Item";
    private const string mapDataPath = "Data/Map";
    private List<Item> allItems = null;
    private List<string> itemNameList = null;
    private static int selectedIndex = 0;
    private static List<GameObject> createdItems = new List<GameObject>();

    // TODO : �ٸ� ������Ʈ�� Ŭ���ϰ� ���ƿ����� ������ ���°� �����Ǹ� ������
    private void OnEnable()
    {
        mapLoader = (MapLoader)target;

        // ���� �����ϴ� ��� Ÿ�ϸ� ����
        tileMaps = FindObjectsOfType<Tilemap>();

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

        selectedIndex = EditorGUILayout.Popup("select item", selectedIndex, itemNameList.ToArray());

        if (GUILayout.Button("create item")) CreateItem();
        if (GUILayout.Button("Destroy All items")) DestroyItems();
        if (GUILayout.Button("Destroy Last item")) DestroyLastItem();
        if (GUILayout.Button("Save All TileMap")) SaveAllTileMap();
        if (GUILayout.Button("Load All TileMap")) LoadAllTileMap();
    }
    private void CreateItem()
    {
        if(createdItems == null) createdItems = new List<GameObject>();

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
        if (createdItems.Count == 0)
        {
            createdItems = new List<GameObject>();
            return;
        }

        var lastIndex = createdItems.Count - 1;
        DestroyImmediate(createdItems[lastIndex]);
        createdItems.RemoveAt(lastIndex);
    }

    private void SetTile(string tileMapName, TileBase tile, Vector3Int vec)
    {
        var tileMap = tileMaps.Where(x => x.name == tileMapName).Single();
        tileMap.SetTile(vec, tile);
    }

    private void SaveAllTileMap()
    {
        foreach (var map in tileMaps)
        {
            var allPositions = map.cellBounds.allPositionsWithin;

            var tilePos = new List<Vector2Int>();
            var tileNames = new List<string>();

            foreach (var pos in allPositions)
            {
                var localPlace = new Vector3Int(pos.x, pos.y, 0);

                if(map.HasTile(localPlace))
                {
                    var tile = map.GetTile(localPlace);
                    tilePos.Add((Vector2Int)localPlace);
                    tileNames.Add(tile.name);
                }
            }


            TileMapData tileMapData = new TileMapData();
            tileMapData.tilePos = tilePos;
            tileMapData.tileNames = tileNames;
            JsonExporter.CreateJsonFile<TileMapData>(mapDataPath, map.name, tileMapData);
        }
    }

    private void LoadAllTileMap()
    {
        var grid = GameObject.Find("Grid").transform;
        LoadTileMap("Ground", grid);
        LoadTileMap("Object", grid);
    }

    private void LoadTileMap(string name, Transform parent = null)
    {
        var o = Instantiate(mapLoader.transform.Find("TileMapEmpty"));
        o.name = name;
        o.parent = parent;
        var tileMap = o.GetComponent<Tilemap>();

        var data = JsonImporter.LoadJsonFile<TileMapData>(mapDataPath, name);
        var tileNames = data.tileNames;
        var tilePos = data.tilePos;
        var length = tileNames.Count;

        for(int i = 0; i < length; ++i)
        {
            tileMap.SetTile((Vector3Int)tilePos[i], (TileBase)Resources.Load("Tileset/" + tileNames[i]));
        }
    }
}

[System.Serializable]
class TileMapData
{
    // Ÿ�� ��ġ ����Ʈ
    public List<Vector2Int> tilePos;
    // Ÿ�� �̸� ����Ʈ
    public List<string> tileNames;
}


#endif