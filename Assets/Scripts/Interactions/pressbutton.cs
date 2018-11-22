using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class pressbutton : MonoBehaviour {
    public bool bytime;
    public float triggertime;
    bool ispress;
    public float counttime;
    public ActionObject ActObj;
    List<Collider> activers;

    private void Start()
    {
        ispress = false;
        counttime = 0;
        activers = new List<Collider>();
    }

    private void Update()
    {
        if (bytime && !ispress)
        { 
            if(counttime > 0) counttime -= Time.deltaTime;
            else ActObj.Deaction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
        {
            if (activers.Count == 0)
            {
                ispress = true;
                if (bytime) counttime = triggertime;
                ActObj.Action();
            }
            if(!activers.Contains(other))activers.Add(other);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box")
        {
            if (activers.Contains(other)) activers.Remove(other);
            if (activers.Count == 0)
            {
                ispress = false;
                if (!bytime) ActObj.Deaction();
            }
        }
    }
}
