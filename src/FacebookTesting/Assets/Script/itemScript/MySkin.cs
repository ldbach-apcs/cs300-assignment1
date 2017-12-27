using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySkin : ItemNonQuantiable
{
    public MySkin(string name, string description, int cost) : base(name, description, cost)
    { }
}

public class MyFood : ItemQuantiable
{
    public MyFood(string name, string description, int cost) : base(name, description, cost)
    { }
}