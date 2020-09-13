using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLose : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            RestartLevel.Restart();
        }
    }
}
