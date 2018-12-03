using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_DialogueTrigger : MonoBehaviour
{
    public bool isActive = false;
    public GameObject dialogue;
    private Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isActive == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                dialogue.SetActive(true);
                player.canMove = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isActive == true)
        {
            dialogue.SetActive(false);
            player.canMove = true;
        }
    }
}
