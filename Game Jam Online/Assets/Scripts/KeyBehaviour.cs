using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    BoxCollider collide;
    [SerializeField]OpenDoor door;
    [SerializeField] AudioSource key;

    private void Awake()
    {
        collide = GetComponent<BoxCollider>();
    }

    IEnumerator CanTake() {
        yield return new WaitForSeconds(1f);
        collide.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            door.canOpen = true;
            key.Play();
            StartCoroutine(DestroyThis());
        }
    }
    IEnumerator DestroyThis() {
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);
    }
}
