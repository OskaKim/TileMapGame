using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : CharacterModelBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 아이템 습득
        if (collision.CompareTag("FieldItem"))
        {
            var fieldItem = collision.GetComponent<FieldItem>();
            Debug.Log(fieldItem.GetItem().itemName);
        }
    }
}
