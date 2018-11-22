using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Destructible : ActionObject{

    public int durability;

    public override void Action()
    {
        base.Action();
        GetComponent<Collider>().isTrigger = true;
    }

    public void inflictDamage(int dmg)
    {
        durability -= dmg;
        if (durability<=0)
        {
            Action();
            //Destroy(this.gameObject);
        }
    }

}
