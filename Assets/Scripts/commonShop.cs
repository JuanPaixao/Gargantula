using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class commonShop:MonoBehaviour{

    public StatBehaviour stats;
    public Text pLife, pStamina, pAttack, pPotion,walletView;
    public Text vLife, vStamina, vAttack, vPotion;

    int nLife =0, nStamina=0, nAttack=0, PotionPrice= 100;
    int[] LifePrice = { 0, 450, 850 } , StaminaPrice = {0, 450, 850}, AttackPrice= {0, 500, 900 };
    int[] LifeUpgrade = { 0, 100, 100 }, StaminaUpgrade = { 0, 100, 100 }, AttackUpgrade = { 0, 10, 10 };

    private void OnEnable()
    {
        graphicBuild();
    }


    public int LifeUpgradePrice { get { return (LifePrice[nLife+1]); } }
    public int StataminUpgradePrice { get { return (StaminaPrice[nStamina + 1]); } }
    public int AttackUpgradePrice { get { return (AttackPrice[nAttack + 1]); } }

    public int LifeUpgradeValue { get { return (LifeUpgrade[nLife + 1]); } }
    public int StataminUpgradeValue { get { return (StaminaUpgrade[nStamina + 1]); } }
    public int AttackUpgradeValue { get { return (AttackUpgrade[nAttack + 1]); } }

    public void BuyLifeUpgrade()
    {
        if (nLife+1 < LifePrice.Length)
        {
            if (stats.takeMoney(LifePrice[nLife + 1]))
            {
                stats.upgradeStats("LifeXC000", LifeUpgrade[nLife + 1]);
                nLife++;
                graphicBuild();
            }
        }
    }
    public void BuyStaminaUpgrade()
    {
        if (nStamina+1 < StaminaPrice.Length)
        {
            if (stats.takeMoney(StaminaPrice[nStamina + 1]))
            {
                stats.upgradeStats("StaminaXD010", StaminaUpgrade[nStamina + 1]);
                nStamina++;
                graphicBuild();
            }
        }
    }
    public void  BuyAttackUpgrade()
    {
        if (nAttack+1 < AttackPrice.Length)
        {
            if (stats.takeMoney(AttackPrice[nAttack + 1]))
            {
                stats.upgradeStats("AttackXE100", AttackUpgrade[nAttack + 1]);
                nAttack++;
                graphicBuild();
            }
        }
    }
    public void BuyPotion()
    {
        if (stats.takeMoney(PotionPrice))
        {
            if (stats.getpotion())
            {
                graphicBuild();
            }
        }
    }

    public void OpenShop()
    {
        this.gameObject.SetActive(true);
    }
    public void CloseShop()
    {
        this.gameObject.SetActive(false);
    }


    private void graphicBuild()
    {
        if (nLife + 1 < LifePrice.Length) pLife.text = LifePrice[nLife + 1].ToString()+"g";
        else pLife.text = "0g";
        if (nStamina + 1 < StaminaPrice.Length) pStamina.text = StaminaPrice[nStamina + 1].ToString()+"g";
        else pStamina.text = "0g";
        if (nAttack + 1 < AttackPrice.Length) pAttack.text = AttackPrice[nAttack + 1].ToString()+"g";
        else pAttack.text = "0g";
        pPotion.text = PotionPrice + "g";
        walletView.text = stats.wallet + "g";

        if (nLife + 1 < LifeUpgrade.Length) vLife.text = "+"+LifeUpgrade[nLife + 1];
        else vLife.text = "MAX";
        if (nStamina + 1 < StaminaUpgrade.Length) vStamina.text = "+"+StaminaUpgrade[nStamina + 1];
        else vStamina.text = "MAX";
        if (nAttack + 1 < AttackUpgrade.Length) vAttack.text = "+"+AttackUpgrade[nAttack + 1];
        else vAttack.text = "MAX";
        vPotion.text = stats.qPotions+" / "+ stats.maxPotions;
    }

}
