using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController pc;
    GameObject dog;
    MyAnimation curAnimation;
    MySkin curSkin;
    int curAffectionPoint;
    int level;//Does not use player prefs.
	// Use this for initialization
    
	void Start () {
		
	}
    //Level 0: 0, Level 1: 50, Level 2: 50 + 55
    //Level 3: 50 + 55 + 60, Level 4: 50 + 55 + 60 + 65
    //One Afp require one 
    int MinimumAfPLevel(int lv)
    {
        return (int)(lv * lv + lv * 19) * 5 / 2;
    }

    int GetLevel(int afP)
    {
        double lv;
        lv = Mathf.Sqrt(afP * (0.4f) + 90.25f) - 9.5;
        return (int)lv;
    }
    // Update is called once per frame
    void Update () {
		
	}
}

public class Currency
{
    int gained;
    static Currency currency;
    Currency()
    {
        gained = PlayerPrefs.GetInt("PetCurrency", 40000);
        if (gained == 0) Update();
    }

    public static void Update()
    {
        PlayerPrefs.SetInt("PetCurrency", GetValue());
        PlayerPrefs.Save();
    }
    public static int GetValue()
    {
        if (currency == null) {
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
        Update();
        return true;
    }

    public static bool CheckEnough(int val)
    {
        return (GetValue() >= val);
    }
}