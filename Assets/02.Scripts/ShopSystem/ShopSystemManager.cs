using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public string name;
    
    public virtual void Func()
    {
        //���
    }
}

public class ShopSystemManager : MonoBehaviour
{
    public List<Item> items; //�����۵�
    // apply
    // randomize picking
    // show / hide

    void Start()
    {
        items = new List<Item>();
    }

    void ApplyToCharacter()
    {
        //using MMfeedback
    }

    List<Item> PickNRandomly(int num, int seed)
    {
        List<Item> pickedItem = new List<Item>();

        


        return pickedItem;
    }

    void ShowUI()
    {

    }

    void HideUI()
    {

    }
}
