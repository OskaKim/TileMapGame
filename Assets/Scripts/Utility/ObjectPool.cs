using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>
{
    private readonly Queue<GameObject> poolQueue = new Queue<GameObject>();
    private readonly int maxSize;
    private readonly Func<T> createObject;
    private readonly Action<T> destroyObject;

    /// <summary>
    /// 오브젝트 풀링
    /// </summary>
    /// <param name="createObject">오브젝트 풀이 비어있을때 수행(생성)</param>
    /// <param name="destroyObject">오브젝트 풀이 최대크기일때 수행(삭제)</param>
    /// <param name="maxSize">최대크기 지정</param>
    public ObjectPool(Func<T> createObject, Action<T> destroyObject, int maxSize = 10)
    {
        this.maxSize = maxSize;
        this.createObject = createObject;
        this.destroyObject = destroyObject;
    }

    // 오브젝트를 풀에서 꺼내기
    public T PopObject()
    {
        // 오브젝트 풀이 비어있음
        if(poolQueue.Count == 0)
        {
            return createObject();
        }

        var poppedObject = poolQueue.Dequeue();
        poppedObject.SetActive(true);
        return poppedObject.GetComponent<T>();
    }

    // 오브젝트를 풀에 넣기
    public void PushObject(GameObject item)
    {

        // 오브젝트 풀이 최대크기
        if (poolQueue.Count >= maxSize)
        {
            destroyObject(item.GetComponent<T>());
        }
        else
        {
            item.SetActive(false);
            poolQueue.Enqueue(item);
        }
    }
}