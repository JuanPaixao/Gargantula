using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(StatBehaviour))]
public class NoobController : MonoBehaviour
{

    Rigidbody rig;
    Animator anim;
    StatBehaviour stats;

    public commonShop Shop;
    public float speed, Jump;
    public Transform bowholder;
    public GameObject model, arrow;


    // Use this for initialization
    void Start()
    {
        stats = GetComponent<StatBehaviour>();
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float MX = rig.velocity.x, MY = rig.velocity.y;

        //moviments
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
            anim.SetFloat("Walk", speed);
            MX = -speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            anim.SetFloat("Walk", speed);
            MX = speed;
        }
        if (Input.GetKey(KeyCode.UpArrow) && stats.isLanded)
        {
            anim.SetTrigger("Jump");
            rig.AddForce(Jump * transform.up, ForceMode.Impulse);
            stats.LandedToogle();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) stats.usePotion();

        //interactions
        if (Input.GetKey(KeyCode.Q) && stats.onClimb)
        {
            stats.climbLadder();
            if (Input.GetKey(KeyCode.UpArrow) && stats.stamina > 0)
            {
                rig.isKinematic = false;
                MY = speed;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && stats.stamina > 0)
            {
                rig.isKinematic = false;
                MY = -speed;
            }
            else if (stats.stamina <= 0) rig.isKinematic = false;
            else rig.isKinematic = true;
        }
        if (stats.onLedge && Input.GetKeyDown(KeyCode.Q)) stats.grabLedge();
        if (stats.onLedge && Input.GetKeyUp(KeyCode.Q)) stats.releaseLedge();
        if (Input.GetKeyDown(KeyCode.Q) && stats.onLever && stats.leverObj) stats.leverObj.LeverToggle();
        if (Input.GetKeyDown(KeyCode.Q) && stats.onChest && stats.chestObj) stats.chestObj.OpenChest(stats);
        if (Input.GetKeyDown(KeyCode.Q) && stats.onShop) Shop.OpenShop();

        //attacks
        if (Input.GetKeyDown(KeyCode.F)) stats.setDefend(true);
        if (Input.GetKeyUp(KeyCode.F)) stats.setDefend(false);
        if (Input.GetKeyDown(KeyCode.E)) stats.ApplyDamage1(this.stats);
        if (Input.GetKeyDown(KeyCode.W)) stats.ApplyDamage2(this.stats);
        if (Input.GetKeyDown(KeyCode.V) && arrow == null)
        {
            arrow = Instantiate(model, bowholder.transform.position, bowholder.transform.rotation);
            arrow.GetComponent<ArrowBahaviour>().SetOwner(stats);
            arrow.transform.parent = bowholder.transform;
        }
        if (Input.GetKey(KeyCode.V) && arrow.GetComponent<ArrowBahaviour>() != null) arrow.GetComponent<ArrowBahaviour>().charge();
        if (Input.GetKeyUp(KeyCode.V) && arrow.GetComponent<ArrowBahaviour>() != null)
        {
            arrow.GetComponent<ArrowBahaviour>().fire();
            arrow = null;
        }

        rig.velocity = new Vector3(MX, MY, 0);
        rig.velocity.Normalize();
    }



}
