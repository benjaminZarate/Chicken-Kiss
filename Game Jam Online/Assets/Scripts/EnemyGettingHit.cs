using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGettingHit : MonoBehaviour
{
    EnemyBehaviour enemy;
    [SerializeField] AudioSource guitar;
    AudioSource oof;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();
        guitar = FindObjectOfType<CharacterController>().GetComponent<AudioSource>();
        oof = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guitar"))
        {
            enemy.GettingHit();
            guitar.Play();
            oof.Play();
        }
    }
}
