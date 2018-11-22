using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTreeBehaviour : MonoBehaviour {

    StatBehaviour player;

    private void Update()
    {
        if (player) player.Heal(2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") player = other.gameObject.GetComponent<StatBehaviour>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") player = null;
    }
}
