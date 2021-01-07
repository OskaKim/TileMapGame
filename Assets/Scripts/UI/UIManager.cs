using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI���� �ݱ� �� �� ����
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
