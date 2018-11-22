using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : StatBehaviour {

    public Transform target;
    public Slider healthBar;

    void Start()
    {
        StartStatBehavior();
        healthBar.maxValue = life;
        healthBar.value = life;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = life;
    }
}
