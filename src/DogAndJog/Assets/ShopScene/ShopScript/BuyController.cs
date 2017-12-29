using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyController : MonoBehaviour {
    //Loading and static call
    public static BuyController buyController;

    Item item;
    int quantity, price, cost;
    public GameObject ItemView, QuantityView, ShowAfterBuy,buttonText;//, button;
    Text text;
    bool owned;
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
        transform.GetChild(2).gameObject.SetActive(false);
        CoinUI.coinUI.LoadInfo();
    }

    // Use this for initialization
    public void ItemLoad(Item item)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
        this.item = item;
        quantity = 1;
        owned = false;
        buttonText.GetComponent<Text>().text = "Buy";
        if (item.GetQuantiable())
        {
            QuantityView.SetActive(true);
            quantity = 1;
            text.text = "1";
        }
        else
        {
            QuantityView.SetActive(false);
            owned = ((ItemNonQuantiable)item).GetAvaiability();
            if (owned)
                buttonText.GetComponent<Text>().text = "Equip";
        }

        ItemView.transform.GetChild(0).GetComponent<Text>().text = item.GetName();
        ItemView.transform.GetChild(2).GetComponent<Text>().text = item.GetPrice().ToString();
        ItemView.transform.GetChild(3).GetComponent<Text>().text = item.GetDescription();
        price = item.GetPrice();
        cost = price;
        AfterBuy();
    }

    public void IncreaseQuantity(int a)
    {
        quantity += a;
        if (quantity < 1) quantity = 1;
        if (quantity > 99) quantity = 99;
        cost = price * quantity;
        text.text = quantity.ToString();//UpdateFood
        AfterBuy();
    }
    public void AfterBuy()
    {
        int afterBuy = Currency.GetValue() - cost;
        Color color = Color.green;
        //Afterbuy part
        if (afterBuy < 0) color = Color.red;
        ShowAfterBuy.GetComponent<Text>().text = afterBuy.ToString();
        ShowAfterBuy.GetComponent<Text>().color = color;
        ShowAfterBuy.transform.GetChild(0).GetComponent<Image>().color = color;
    }
	public void Buy()
    {
        if (owned)
        {
            //Set PlayerReference
        }
        Currency.Decrease(cost);
        /*if (item.GetItemType()=="Food")
            DatabaseReader.Instance().BuyFood(item.GetName(), quantity);
        if (item.GetItemType() == "Skin")
            DatabaseReader.Instance().BuySkin();*/
        SetNonActive();
    }
}