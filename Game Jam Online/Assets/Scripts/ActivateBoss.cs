using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    [SerializeField]BossBehaviour boss;
    [SerializeField] AudioSource mainTheme;
    [SerializeField] AudioClip bossTheme;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            boss.enabled = true;
            mainTheme.clip = bossTheme;
            mainTheme.Play();
            Destroy(this.gameObject);
        }
    }
}
