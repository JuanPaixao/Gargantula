using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestButton : MonoBehaviour {

    public ActionObject ActObj;
    public bool isItem;

    public GameObject InputItem;
    public int amount;
    GameObject holdingItem;
    int Amount;

    private void Start()
    {
        if (isItem)
        {
            holdingItem = InputItem;
            InputItem = null;
        }
        else
        {
            Amount = amount;
            amount = 0;
        }
    }

    public void OpenChest(StatBehaviour stats)
    {
        ActObj.Action();
        if (isItem) stats.setItem(holdingItem);
        else stats.putMoney(Amount);
        stats.ChestRelease();
        Destroy(this.gameObject);
    }
}
