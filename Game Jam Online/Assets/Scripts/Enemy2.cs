using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float rotationVel;
    [SerializeField] float vel;
    [SerializeField] float minDistance;
    Transform currentWaypoint;
    Transform target;

    [SerializeField] Transform projectileSpawn;
    [SerializeField] GameObject projectile;
    [SerializeField] AudioSource guitar;

    int index = 0;
    [SerializeField]int hp;
    Animator _anim;
    AudioSource oof;

    public enum enemyState { PATROL, ATTACK, DYING }

    enemyState state;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        target = FindObjectOfType<CharacterController>().transform;
        currentWaypoint = waypoints[index];
        guitar = FindObjectOfType<CharacterController>().GetComponent<AudioSource>();
        oof = GetComponent<AudioSource>();
    }

    private void Update()
    {
        switch (state) {
            case enemyState.PATROL:
                GoToWaypoint();
                break;
            case enemyState.ATTACK:
                Attack();
                break;
        }
    }

    void GoToWaypoint() {
        Vector3 direction = currentWaypoint.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(direction);

        lookRot.x = 0;
        lookRot.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Mathf.Clamp01(rotationVel * Time.maximumDeltaTime));
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, vel * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= minDistance) {
            state = enemyState.ATTACK;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint")) {
            ChangeWaypoint();
        }

        if (other.CompareTag("Guitar")) {
            GettingHit();
        }
    }

    void ChangeWaypoint() {
        index++;
        if (index >= waypoints.Length)
        {
            index = 0;
        }
        currentWaypoint = waypoints[index];
    }

    void Attack() {
        if (hp <= 0)
        {
            _anim.Play("Die Forwards");
            state = enemyState.DYING;
            return;
        }
        _anim.SetBool("Attack", true);
        Vector3 direction = target.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(direction);

        lookRot.x = 0;
        lookRot.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Mathf.Clamp01(rotationVel * Time.maximumDeltaTime));
    }

    void SpawnObject() {
        Instantiate(projectile, projectileSpawn.position,Quaternion.identity);
    }

    public void GettingHit()
    {
        hp--;
        guitar.Play();
        oof.Play();
        if (hp <= 0)
        {
            _anim.SetBool("Die", true);
            state = enemyState.DYING;
            return;
        }
        Vector3 toTarget = (target.position - transform.position).normalized;
        if (Vector3.Dot(toTarget, transform.forward) > 0)
        {
            _anim.SetTrigger("HitFront");
        }
        else
        {
            _anim.SetTrigger("HitBack");
        }
    }


}
