using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

[RequireComponent(typeof(Animator))]
public class FieldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;
    public Animator animator;
    private Action<FieldItem> getItemCallback;

    public void SetItem(Item _item, Action<FieldItem> getItemCallback)
    {
        item.DeepCopyParam(_item);

        animator.runtimeAnimatorController = ItemManager.Instance.itemAnimationList[item.index];
        this.getItemCallback = getItemCallback;
    }

    public Item GetItem()
    {
        getItemCallback(this);
        return item;
    }
}
