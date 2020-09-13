using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{

    CapsuleCollider coll;

    [SerializeField]ParticleSystem explosion;
    [SerializeField] AudioSource sfx;
    void Start()
    {
        StartCoroutine(Explode());
        coll = GetComponent<CapsuleCollider>();
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(2);
        coll.enabled = true;
        explosion.Play();
        sfx.Play();
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);

    }
}
