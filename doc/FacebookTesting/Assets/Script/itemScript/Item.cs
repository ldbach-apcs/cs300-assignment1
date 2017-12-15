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
}

public abstract class ItemQuantiable : Item
{
    short count; //For Quantiable Objects.
    protected ItemQuantiable(string name, string description, int cost) : base(name, description, cost, true)
    { }
}
public abstract class ItemNonQuantiable : Item
{
    bool avaiability;
    protected ItemNonQuantiable(string name, string description, int cost) : base(name, description, cost, false)
    { }
    public void SetAvaiability(bool ava)
    {
        avaiability = ava;
    }
}