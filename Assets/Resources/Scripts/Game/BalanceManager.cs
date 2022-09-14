using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceManager : MonoBehaviour
{
    public int ValueBalance;

    public TextMeshProUGUI BalanceText;

    void Start()
    {
        BalanceText.text = ValueBalance.ToString("c");
    }
    
    public void UpdateBalance(int value)
    {
        ValueBalance = ValueBalance + value;
        BalanceText.text = ValueBalance.ToString();
    }
}
