using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLose : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            RestartLevel.Restart();
        }
    }
}
