using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour {
    public static CoinUI coinUI;
    public void Start()
    {
        coinUI = this;
        LoadInfo();
    }

    public void LoadInfo()
    {
        GetComponent<Text>().text = Currency.GetValue().ToString();
    }
}
