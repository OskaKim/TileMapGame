using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] GameObject InventoryUIObject;
    [SerializeField] GameObject OpenInventoryUIButtonObject;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(InventoryUIObject.activeSelf)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    public void OpenInventory()
    {
        InventoryUIObject.SetActive(true);
        OpenInventoryUIButtonObject.SetActive(false);
    }

    public void CloseInventory()
    {
        InventoryUIObject.SetActive(false);
        OpenInventoryUIButtonObject.SetActive(true);
    }
}
