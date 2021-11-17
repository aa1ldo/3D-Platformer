using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ScoreScript.scoreValue += 1;
        Destroy(gameObject);
    }
}
