using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FieldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;
    public Animator animator;

    public void SetItem(Item _item)
    {
        item.DeepCopyParam(_item);

        animator.runtimeAnimatorController = ItemManager.Instance.itemAnimationList[item.index];
    }

    public Item GetItem()
    {
        DestroyItem();
        return item;
    }

    public void DestroyItem()
    {
        // TODO : object pool
        Destroy(gameObject);
    }
}
