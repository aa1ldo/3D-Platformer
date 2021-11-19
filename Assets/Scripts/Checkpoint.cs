using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    CharacterController player;
    public Vector3 respawnPoint;

    private void Start()
    {
        player = GameObject.Find("Capsule").GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            respawnPoint = player.myRigidbody.transform.position;
            Debug.Log(respawnPoint);
        }
    }
}
