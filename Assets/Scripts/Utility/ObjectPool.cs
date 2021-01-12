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
    /// ������Ʈ Ǯ��
    /// </summary>
    /// <param name="createObject">������Ʈ Ǯ�� ��������� ����(����)</param>
    /// <param name="destroyObject">������Ʈ Ǯ�� �ִ�ũ���϶� ����(����)</param>
    /// <param name="maxSize">�ִ�ũ�� ����</param>
    public ObjectPool(Func<T> createObject, Action<T> destroyObject, int maxSize = 10)
    {
        this.maxSize = maxSize;
        this.createObject = createObject;
        this.destroyObject = destroyObject;
    }

    // ������Ʈ�� Ǯ���� ������
    public T PopObject()
    {
        // ������Ʈ Ǯ�� �������
        if(poolQueue.Count == 0)
        {
            return createObject();
        }

        var poppedObject = poolQueue.Dequeue();
        poppedObject.SetActive(true);
        return poppedObject.GetComponent<T>();
    }

    // ������Ʈ�� Ǯ�� �ֱ�
    public void PushObject(GameObject item)
    {

        // ������Ʈ Ǯ�� �ִ�ũ��
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