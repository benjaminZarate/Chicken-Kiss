using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    Transform target = null;

    Vector3 direction = Vector3.zero;

    [SerializeField] float vel = 10;

    void Start()
    {
        target = FindObjectOfType<CharacterController>().transform;

        direction = target.position;
        transform.rotation = Quaternion.LookRotation(direction);
        Invoke("DestroyThis",2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, vel * Time.deltaTime);
    }

    void DestroyThis() {
        Destroy(this.gameObject);
    }
}
