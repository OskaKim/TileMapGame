using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameData;

public class UIInventory : MonoBehaviour
{
    [SerializeField] GameObject OpenInventoryUIButtonObject;
    [SerializeField] TextMeshProUGUI CoinFund;

    private bool isOpen = false;
    public bool IsOpen
    {
        get
        {
            return isOpen;
        }
        private set
        {
            isOpen = value;
        }
    }

    public void OpenInventory()
    {
        gameObject.SetActive(true);
        OpenInventoryUIButtonObject.SetActive(false);

        // 캐릭터 코인양 적용
        var data = ItemManager.Instance.possessionItemDatabase;
        var possessionCoin = data.GetByItemIndex((int)CurrencyType.Coin);
        CoinFund.text = possessionCoin.amount.ToString();
        IsOpen = true;
    }

    public void CloseInventory()
    {
        gameObject.SetActive(false);
        OpenInventoryUIButtonObject.SetActive(true);
        IsOpen = false;
    }
}
