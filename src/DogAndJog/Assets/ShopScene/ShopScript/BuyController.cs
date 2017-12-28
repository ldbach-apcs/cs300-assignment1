using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyController : MonoBehaviour {
    //Loading and static call
    public static BuyController buyController;

    Item item;
    int quantity, price, cost;
    public GameObject ItemView, QuantityView;//, button;
    Text text;
    public void Start()
    {
        text = QuantityView.transform.GetChild(0).GetComponent<Text>();
        text.text = "1";
        buyController = this;
        SetNonActive();
    }
    public void SetNonActive()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        CoinUI.coinUI.LoadInfo();
    }

    // Use this for initialization
    public void ItemLoad(Item item)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        this.item = item;
        quantity = 1;
        if (item.GetQuantiable())
        {
            QuantityView.SetActive(true);
            quantity = 1;
            text.text = "1";
        }
        else QuantityView.SetActive(false);
        price = item.GetPrice();
        price = item.GetPrice();
        cost = price;
    }

    public void IncreaseQuantity(int a)
    {
        quantity += a;
        if (quantity < 1) quantity = 1;
        if (quantity > 99) quantity = 99;
        cost = price * quantity;
        text.text = quantity.ToString();
    }
	public void Buy()
    {
        Currency.Decrease(cost);
        SetNonActive();
    }
}
