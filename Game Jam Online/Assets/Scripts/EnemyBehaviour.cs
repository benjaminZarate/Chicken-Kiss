using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float vel;
    [SerializeField] float rotationVel;

    [SerializeField] int hp = 4;

    Transform target;
    bool canMove = true;

    public enum enemyState { 
        IDLE, RUNNING, ATTACKING, DYING
    }

    public enemyState state;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<CharacterController>().transform;
    }

    public void Running()
    {
        anim.SetBool("Run", true);

        Vector3 direction = target.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(direction);

        lookRot.x = 0;
        lookRot.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Mathf.Clamp01(rotationVel * Time.maximumDeltaTime));
        transform.position = Vector3.MoveTowards(transform.position,target.position, vel * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= 8) {
            state = enemyState.ATTACKING;
        }

    }

    void Attacking() {
        anim.SetBool("Attack", true);
        if (hp <= 0)
        {
            anim.Play("Die Forwards");
            state = enemyState.DYING;
            return;
        }
    }

    void ChangeIdle() {
        state = enemyState.IDLE;
    }

    void Idle() {
        anim.SetBool("Attack", false);
    }

    private void Update()
    {
        switch (state) {
            case enemyState.IDLE:
                Idle();
                break;
            case enemyState.RUNNING:
                Running();
                break;
            case enemyState.ATTACKING:
                Attacking();
                break;
            case enemyState.DYING:
                anim.SetBool("Die", true);
                break;
        }
    }

    public void GettingHit()
    {
        hp--;
        if (hp <= 0) {
            anim.SetBool("Die", true);
            state = enemyState.DYING;
            return;
        }
        Vector3 toTarget = (target.position - transform.position).normalized;
        if (Vector3.Dot(toTarget, transform.forward) > 0)
        {
            anim.SetTrigger("HitFront");
        }
        else {
            anim.SetTrigger("HitBack");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (state == enemyState.ATTACKING || state == enemyState.DYING) return;
            state = enemyState.RUNNING;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (state == enemyState.DYING) return;
        state = enemyState.IDLE;
    }
}
