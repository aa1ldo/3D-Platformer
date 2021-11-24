using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger1 : MonoBehaviour
{
    CharacterController player;
    [SerializeField] private Transform respawnPoint1;

    private void Start()
    {
        player = GameObject.Find("Capsule").GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.transform.position = respawnPoint1.transform.position;
        }
    }
}
