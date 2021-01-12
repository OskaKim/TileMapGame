using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

[RequireComponent(typeof(Animator))]
public class FieldItem : MonoBehaviour
{
    public Item itemInfo;
    public SpriteRenderer image;
    public Animator animator;
    private Action<FieldItem> getItemCallback;

    public void SetItem(Item _item, Action<FieldItem> getItemCallback)
    {
        itemInfo.DeepCopyParam(_item);

        animator.runtimeAnimatorController = ItemManager.Instance.itemAnimationList[itemInfo.index];
        this.getItemCallback = getItemCallback;
    }

    public Item GetItemInfo()
    {
        getItemCallback(this);
        return itemInfo;
    }
}
