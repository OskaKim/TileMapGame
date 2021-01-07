using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI열고 닫기 등 할 예정
public class UIManager : MonoBehaviour
{
    [SerializeField] private UIInventory uiInventory;

    public void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            if (uiInventory.IsOpen) uiInventory.CloseInventory();
            else uiInventory.OpenInventory();
        }
    }   
}
