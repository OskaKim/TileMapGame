using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    protected ObjectPool<T> pool;

    public virtual T Spawn(Vector2 pos)
    {
        return Instantiate(prefab, pos, Quaternion.identity).GetComponent<T>();
    }
}
