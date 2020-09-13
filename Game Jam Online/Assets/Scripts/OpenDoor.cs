using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [HideInInspector]public bool canOpen = false;
    [SerializeField] ParticleSystem portal;
    [SerializeField] int hitPoints;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] AudioSource guitar;
    [SerializeField] AudioSource wood;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guitar")) {
            if (canOpen) {
                portal.Stop();
                hitPoints--;
                hitParticle.Play();
                guitar.Play();
                wood.Play();
                Debug.Log("Hitting");
                if (hitPoints <= 0) StartCoroutine(DestroyThis());
            }
        }
    }
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(this.gameObject);
    }
}
