using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class StatusCanvas : MonoBehaviour {

    public Canvas can, mobileCanvas;
    public Text damageModel, DamageDisplay, Pot, Money;
    public Image HealthBar, StaminaBar;
    public StatBehaviour stat;

	// Use this for initialization
	void Start () {
        can = GetComponent<Canvas>();
        stat.magic = this;
        if(Pot)Pot.text = stat.qPotions + "/" + stat.maxPotions;
    }
	
	// Update is called once per frame
	void Update () {

        HealthBar.fillAmount = stat.plife;
        StaminaBar.fillAmount = stat.pstamina;
        if(Money) Money.text = stat.wallet.ToString();
        if(Pot) Pot.text = stat.qPotions + "/" + stat.maxPotions;
        if ( mobileCanvas && mobileCanvas.gameObject.activeInHierarchy) mobileCanvas.transform.position = stat.transform.position;
    }

    public void DisplayDamage(int damage)
    {
        DamageDisplay = Instantiate(damageModel, can.transform);
        DamageDisplay.enabled = true;
        if (damage == 0)
        {
            DamageDisplay.text = "0";
            DamageDisplay.color = Color.blue;
        }
        else
        {
            DamageDisplay.text = damage.ToString();
            DamageDisplay.color = Color.red;
        }
        Destroy(DamageDisplay, 1);
    }

    public void OnInteract(bool v)
    {
        mobileCanvas.gameObject.SetActive(v);
    }
}
