using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingHit : MonoBehaviour
{
    PlayerMovement _player;
    Animator _anim;

    [SerializeField] ParticleSystem hitParticle;

    [SerializeField] List<GameObject> hpHud;
    private void Start()
    {
        _player = GetComponent<PlayerMovement>();
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Danger")) {
            if (PlayerLifeManager.hp <= 0) {
                _anim.SetTrigger("Die");
                return;
            }
            _player.canMove = false;
            _anim.SetTrigger("Hit");
            hitParticle.Play();
            PlayerLifeManager.hp--;
            hpHud[PlayerLifeManager.hp].SetActive(false);
        }
    }



}
