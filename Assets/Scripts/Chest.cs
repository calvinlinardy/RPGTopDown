using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    [SerializeField] Sprite emptyChestSprite = null;
    [SerializeField] int pesosAmount = 10;
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChestSprite;
            Debug.Log("Grant " + pesosAmount + " pesos!");
        }
    }
}
