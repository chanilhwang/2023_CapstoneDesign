using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public string name;
    
    public virtual void Func()
    {
        //기능
    }
}

public class ShopSystemManager : MonoBehaviour
{
    public List<Item> items; //아이템들
    // apply
    // randomize picking
    // show / hide

    void Start()
    {
        
    }
}
