using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ArrowBahaviour : MonoBehaviour {

    Rigidbody rig;
    float vel;
    Vector3 velocity;
    StatBehaviour owner;
    public LineRenderer Traject;

	// Use this for initialization
	void Start () {
        vel = 1;
        rig = GetComponent<Rigidbody>();
        rig.isKinematic = true;
        Traject = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(this.transform.position, owner.transform.position) > 20) Destroy(this.gameObject);
	}

    public void SetOwner(StatBehaviour bs)
    {
        owner = bs;
    }

    public void charge()
    {
        if (vel <= 5) vel += Time.deltaTime;
        velocity = transform.forward * vel * 5;
        MakeaLine();
    }

    public void fire()
    {
        rig.velocity = velocity;
        rig.isKinematic = false;
        Destroy(Traject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject GO = other.gameObject;
        string tag = other.gameObject.tag;
        if (other.gameObject.tag != owner.gameObject.tag)
        {
            if (tag == "Holder")
            {
                GO.GetComponent<Destructible>().inflictDamage(100);
                Destroy(this.gameObject);
            }
            else if (tag == "Enemy")
            {
                owner.GetComponent<StatBehaviour>().ApplyDamage2(GO.GetComponent<StatBehaviour>());
                Destroy(this.gameObject);
            }
        }
    }

    void MakeaLine()
    {
        Vector3 pos = this.transform.position;
        Vector3 velo = velocity;

        for (int i = 0; i < Traject.positionCount; i++)
        {
            Traject.SetPosition(i, pos);
            velo += Physics.gravity*Time.fixedDeltaTime;
            pos += velo * Time.fixedDeltaTime; 
        }

    }
}
