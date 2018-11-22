using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDropper : ActionObject {

    Rigidbody rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    public override void Action()
    {
        if (rig) rig.isKinematic = false;
    }
}
