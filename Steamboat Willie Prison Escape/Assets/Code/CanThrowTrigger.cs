using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanThrowTrigger : MonoBehaviour
{
    public Transform otherPlayer; // Reference to the other player

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the tag matches your player's tag
        {
            otherPlayer = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            otherPlayer = null;
        }
    }

}
