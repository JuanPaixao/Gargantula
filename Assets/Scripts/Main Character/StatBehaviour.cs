using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class StatBehaviour : MonoBehaviour
{
    public float OLife, OStamina;
    public int OAttack1, OAttack2, StaminaPerUpdate = 5;
    public StatusCanvas magic;
    float tLife, cLife;
    int Attack1, Attack2, QPotions, MaxPotions, Wallet;
    float tStamina, cStamina;
    bool IsLanded, OnLedge, IsDead, IsDefend, OnClimb, OnLever, OnChest, OnShop;
    public leverbutton leverObj;
    public chestButton chestObj;

    /// <summary>
    /// money amount
    /// </summary>
    public int wallet { get { return (Wallet); } }
    /// <summary>
    /// current potion amount
    /// </summary>
    public int qPotions { get { return (QPotions); } }
    /// <summary>
    /// Max potions amount
    /// </summary>
    public int maxPotions { get { return (MaxPotions); } }
    /// <summary>
    /// Life Porcentage (0.0f to 1.0f)
    /// </summary>
    public float plife { get { return ((cLife / tLife)); } }
    /// <summary>
    /// Stamina Porcentage (0.0f to 1.0f)
    /// </summary>
    public float pstamina { get { return ((cStamina / tStamina)); } }
    /// <summary>
    /// Number of current Life 
    /// </summary>
    public float life { get { return (cLife); } }
    /// <summary>
    /// Numbeer of current Stamina
    /// </summary>
    public float stamina { get { return (cStamina); } }
    /// <summary>
    /// return if object is on Ledge area
    /// </summary>
    public bool onLedge { get { return (OnLedge); } }
    /// <summary>
    /// return if object is on climb area
    /// </summary>
    public bool onClimb { get { return (OnClimb); } }
    /// <summary>
    ///  return if object is landed
    /// </summary>
    public bool isLanded { get { return (IsLanded); } }
    /// <summary>
    /// return if object is dead (life == 0)
    /// </summary>
    public bool isDead { get { return (IsDead); } }
    /// <summary>
    /// return if object is defend
    /// </summary>
    public bool isDefend { get { return (IsDefend); } }
    /// <summary>
    /// return if object is OnLever
    /// </summary>
    public bool onLever { get { return (OnLever); } }
    /// <summary>
    /// return if object is Onchest
    /// </summary>
    public bool onChest { get { return (OnChest); } }
    /// <summary>
    /// return if object is OnShop
    /// </summary>
    public bool onShop { get { return (OnShop); } }

    /// <summary>
    /// Grabhands = object who will holds de main object
    /// </summary>
    public GameObject grabhands;


    // Use this for initialization
    public void StartStatBehavior()
    {
        MaxPotions = 7;
        QPotions = 0;

        Wallet = 1000;
        if (OAttack1 <= 0) Attack1 = 1;
        else Attack1 = OAttack1;
        if (OAttack2 <= 0) Attack2 = 2;
        else Attack2 = OAttack2;
        IsDefend = IsLanded = IsDead = OnLedge = OnClimb = OnLever = OnChest = OnShop = false;
        if (OLife <= 0) tLife = cLife = 100;
        else tLife = cLife = OLife;
        if (OStamina <= 0) tStamina = cStamina = 100;
        else tStamina = cStamina = OStamina;
    }

    // Update is called once per frame
    public void UpdateStatus()
    {
        if (!grabhands.activeInHierarchy && !IsDefend)
        {
            if (cStamina < tStamina) cStamina += Time.deltaTime * StaminaPerUpdate;
        }
        else if (grabhands.activeInHierarchy)
        {
            cStamina -= Time.deltaTime * 20;
        }

        if (cStamina <= 0)
        {
            cStamina = 0;
            if (grabhands.activeInHierarchy) releaseLedge();
            setDefend(false);
        }

        //if (cLife < tLife) cLife += Time.deltaTime;

    }

    // Stats action! 
    /// <summary>
    /// climb Ladder
    /// </summary>
    public void climbLadder()
    {
        cStamina -= 0.5f;
    }
    /// <summary>
    /// Grab Ledges
    /// </summary>
    public void grabLedge()
    {
        if (OnLedge) grabhands.SetActive(true);
    }
    /// <summary>
    /// Release Ledges
    /// </summary>
    public void releaseLedge()
    {
        if (grabhands.activeInHierarchy) grabhands.SetActive(false);
    }

    /// <summary>
    /// is no more Landed
    /// </summary>
    public void LandedToogle()
    {
        IsLanded = false;
    }
    /// <summary>
    /// Grab the interactive object front of a player
    /// </summary>
    public void grabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward + new Vector3(0, 0.5f, 0), out hit, 2))
        {
            /*
            if (hit.collider.tag == "GetAble")
            {
                objectGrabed = hit.collider.gameObject;
                if (objectGrabed.GetComponent<Rigidbody>() != null)
                {
                    objectGrabed.GetComponent<Rigidbody>().detectCollisions = false;
                    objectGrabed.GetComponent<Rigidbody>().isKinematic = true;
                }
                objectGrabed.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                objectGrabed.transform.parent = grabedHolder.transform;
                objectGrabed.transform.localPosition = new Vector3(0, 0, 0);
            }
            */
        }
    }
    /// <summary>
    /// Release the interactive object front of a player
    /// </summary>
    public void ReleaseObject()
    {
        /*
        objectGrabed.transform.parent = null;
        objectGrabed.transform.localScale = new Vector3(1, 1, 1);
        objectGrabed.transform.position = transform.position+ new Vector3(0,1,0);
        if (objectGrabed.GetComponent<Rigidbody>() != null)
        {
            objectGrabed.GetComponent<Rigidbody>().detectCollisions = true;
            objectGrabed.GetComponent<Rigidbody>().isKinematic = false;
        }
        objectGrabed = null;
        */
    }
    /// <summary>
    /// Set active to defend
    /// </summary>
    /// <param name="active"> is the object defend?</param>
    public void setDefend(bool active)
    {
        IsDefend = active;
    }

    public void updateBaseDamage()
    {
        if (OAttack1 <= 0) Attack1 = 1;
        else Attack1 = OAttack1;
        if (OAttack2 <= 0) Attack2 = 2;
        else Attack2 = OAttack2;
    }

    /// <summary>
    /// Spend 30 of stamina to do a weak attack
    /// </summary>
    public void WeakAttack()
    {
        cStamina -= 30;
    }

    /// <summary>
    /// Spend 40 of stamina to do a strong attack
    /// </summary>
    public void StrongAttack()
    {
        cStamina -= 40;
    }

    /// <summary>
    /// Apply 1 type of damage at enamy stats;
    /// </summary>
    /// <param name="Enemy">Enemy whos will take damage</param>
    /// <returns>returns damage inflict</returns>
    public int ApplyDamage1(StatBehaviour Enemy)
    {
        int damegeDeal = Enemy.TakeDamage(Attack1);
        return (damegeDeal);
    }
    /// <summary>
    /// Apply 2 type of damage at enamy stats;
    /// </summary>
    /// <param name="Enemy">Enemy whos will take damage</param>
    /// <returns>returns damage inflict</returns>
    public int ApplyDamage2(StatBehaviour Enemy)
    {
        int damageDeal = Enemy.TakeDamage(Attack2);
        return (damageDeal);
    }
    /// <summary>
    /// Apply damage at yourself current life
    /// </summary>
    /// <param name="eAttack">Number of damege taken</param>
    /// <returns>returns the damage taken damage </returns>
    private int TakeDamage(int eAttack)
    {
        int damage = 0;
        if (IsDefend) cStamina -= 5;
        else
        {
            damage = eAttack;
            if (damage < 0) damage = 0;
            cLife -= damage;
            if (cLife <= 0) IsDead = true;
        }
        // magic.DisplayDamage(damage);
        return (damage);
    }
    /// <summary>
    /// reduce to 0 all life of object
    /// </summary>
    public void Kill()
    {
        IsDead = true;
        cLife = 0;
    }
    /// <summary>
    /// reduce to 0 all life of object
    /// </summary>
    public void setItem(GameObject Item)
    {
    }
    /// <summary>
    /// putmoney at wallet
    /// </summary>
    public void putMoney(int gold)
    {
        if (gold >= 0) Wallet += gold;
    }
    /// <summary>
    /// take money from wallet
    /// </summary>
    public bool takeMoney(int gold)
    {
        if (Wallet - gold >= 0)
        {
            Wallet -= gold;
            return (true);
        }
        else return (false);
    }
    /// <summary>
    /// add potion
    /// </summary>
    public bool getpotion()
    {
        if (QPotions < maxPotions)
        {
            QPotions++;
            magic.Pot.text = QPotions + "/" + maxPotions;
            return (true);
        }
        else return (false);
    }
    /// <summary>
    /// use a potion
    /// </summary>
    public void usePotion()
    {
        if (QPotions > 0)
        {
            QPotions--;
            magic.Pot.text = QPotions + "/" + maxPotions;
            Heal(10);
        }
    }
    /// <summary>
    /// release chest
    /// </summary>
    public void ChestRelease()
    {
        if (OnChest)
        {
            OnChest = false;
            chestObj = null;
            if (!OnLedge && !OnClimb && !OnLever && !OnChest && !OnShop) magic.OnInteract(false);
        }
    }

    public void upgradeStats(string stat, int upgr)
    {
        if (stat == "LifeXC000") tLife = cLife = tLife + upgr;
        else if (stat == "StaminaXD010") tStamina = cStamina = tStamina + upgr;
        else if (stat == "AttackXE100") Attack2 = 2 * (Attack1 = Attack1 + upgr);
    }
    public void Heal(int heal)
    {
        if (heal > 0) cLife += heal;
    }

    public void RestoreAllStatus()
    {
        IsDead = false;
        cLife = OLife;
        cStamina = OLife;
    }


    //Colliders verifications
    void OnCollisionEnter(Collision Col)
    {
        IsLanded = true;
    }

    protected void StatusEnteringTrigger(Collider other)
    {
        if (other.gameObject.tag == "Ledge") OnLedge = true;
        if (other.gameObject.tag == "Climb") OnClimb = true;
        if (other.gameObject.tag == "Shop") OnShop = true;
        if (other.gameObject.tag == "Lever")
        {
            OnLever = true;
            leverObj = other.gameObject.GetComponent<leverbutton>();
        }
        if (other.gameObject.tag == "Chest")
        {
            OnChest = true;
            chestObj = other.gameObject.GetComponent<chestButton>();
        }
        if (OnClimb || OnLever || OnChest || OnShop) magic.OnInteract(true);
        if (other.GetComponent<IDeath>() != null) this.Kill();
    }

    protected void StatusExitingTrigger(Collider other)
    {
        if (!other.CompareTag("NPC"))
        {
            if (other.gameObject.tag == "Ledge") OnLedge = false;
            if (other.gameObject.tag == "Climb") OnClimb = false;
            if (other.gameObject.tag == "Shop") OnShop = false;
            if (other.gameObject.tag == "Lever") OnLever = false;
            if (other.gameObject.tag == "Chest") OnChest = false;
            if (!OnClimb && !OnLever && !OnChest && !OnShop) magic.OnInteract(false);
        }
    }
}
