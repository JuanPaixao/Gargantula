using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverbutton : MonoBehaviour {

    public bool OneTime;
    bool isActive;
    public ActionObject ActObj;

    private void Start()
    {
        isActive = false;
    }

    public bool LeverToggle()
    {
        isActive = !isActive;
        if (isActive)
        {
            ActObj.Action();
            if (OneTime) Destroy(this);
        }
        else ActObj.Deaction();
        return (isActive);
    }
}
