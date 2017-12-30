using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    //Public Set Item Based On Type -> Easier To Find
    string name;
    string description;
    int cost; //Cost for Animation is 0
    bool quantiable;
    public Sprite img;

    public void SetImg(Sprite _img) {
        img = _img;
    }



    protected Item(string name, string description, int cost, bool quantiable)
    {
        this.name = name;
        this.description = description;
        this.cost = cost;
        this.quantiable = quantiable;
    }
    public string GetName()
    {
        return name;
    }
    public string GetDescription()
    {
        return description;
    }
    public int GetPrice()
    {
        return cost;
    }
    public bool GetQuantiable()
    {
        return quantiable;
    }//If the item is countable or only care about avaiability.
    public abstract string GetItemType();
}

public abstract class ItemQuantiable : Item
{
    int quantity; //For Quantiable Objects.
    protected ItemQuantiable(string name, string description, int cost, int quantity) : base(name, description, cost, true)
    {
        this.quantity = quantity;
    }
    public override abstract string GetItemType();
}
public abstract class ItemNonQuantiable : Item
{
    bool avaiability;
    protected ItemNonQuantiable(string name, string description, int cost, bool avaiability) : base(name, description, cost, false)
    {
        this.avaiability = avaiability;
    }
    public void SetAvaiability(bool ava)
    {
        avaiability = ava;
    }
    public bool GetAvaiability()
    {
        return avaiability;
    }
    public override abstract string GetItemType();
}

public class MySkin : ItemNonQuantiable
{
    public MySkin(string name, string description, int cost, bool avaiability) : base(name, description, cost, avaiability)
    { }
    public override string GetItemType()
    {
        return "Skin";
    }
}

public class MyFood : ItemQuantiable
{
    int power;
    public MyFood(string name, string description, int cost, int quantity, int power) : base(name, description, cost, quantity)
    {
        this.power = power;
    }
    public override string GetItemType()
    {
        return "Food";
    }
}

public class MyAnimation : ItemNonQuantiable
{
    public MyAnimation(string name, string description, int cost, bool avaiability) : base(name, description, cost, avaiability)
    { }
    public override string GetItemType()
    {
        return "Animation";
    }
}