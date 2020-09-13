using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] int hp = 10;
    Animator _anim;

    [SerializeField] Transform target;
    [SerializeField] float rotationVel;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject door;
    [SerializeField] AudioSource guitar;

    public enum bossState { IDLE,PHASE1, PHASE2, DYING}
    bossState state;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        state = bossState.PHASE1;
        sword.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case bossState.IDLE:
                break;
            case bossState.PHASE1:
                MeleeAttack();
                break;
            case bossState.PHASE2:
                Phase2();
                break;
            case bossState.DYING:
                break;
        }
    }

    void MeleeAttack() {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(direction);

        lookRot.x = 0;
        lookRot.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Mathf.Clamp01(rotationVel * Time.maximumDeltaTime));
        _anim.SetBool("Melee",true);
        if (hp <= 5)
        {
            state = bossState.PHASE2;
            sword.SetActive(false);
        }
    }

    void RangeAttack() {
        Instantiate(projectile, new Vector3(target.position.x, 0, target.position.z),Quaternion.identity);
    }

    void Dying() {
        _anim.SetTrigger("Die");
    }

    void Phase2() {
        _anim.SetBool("Magic", true);
        if (hp <= 0)
        {
            state = bossState.DYING;
            _anim.SetTrigger("Die");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guitar")) {
            hp--;
            guitar.Play();
        }
    }

    public void DeactivateCollider() { 
        sword.GetComponent<BoxCollider>().enabled = false;
    }

    public void ActivateCollider() {
        sword.GetComponent<BoxCollider>().enabled = true;
    }

    public void DeactivateDoor() {
        door.SetActive(false);
    }
}
