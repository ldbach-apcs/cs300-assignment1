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

public class Currency
{
    int gained = -1;
    static Currency currency;
    Currency()
    {
        // gained = PlayerPrefs.GetInt("PetCurrency", 2000);
        if (gained == -1) gained = DatabaseReader.Instance().money;
    }

    public static void Update()
    {
        // PlayerPrefs.SetInt("PetCurrency", GetValue());
        // PlayerPrefs.Save();
    }
    public static int GetValue()
    {
        if (currency == null)
        {
            currency = new Currency();
        }
        return currency.gained;
    }

    public static void Increase(int inc)
    {
        if (inc < 0) return; //Error
        currency.gained += inc;
        Update();
    }

    public static bool Decrease(int dec)
    {
        if (dec < 0) return false; //Error
        if (!CheckEnough(dec)) return false;

        currency.gained -= dec;
        DatabaseReader.Instance().money = currency.gained;
        Update();
        return true;
    }

    public static bool CheckEnough(int val)
    {
        return (GetValue() >= val);
    }
}