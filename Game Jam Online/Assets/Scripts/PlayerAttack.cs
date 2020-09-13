using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject guitar;

    [SerializeField] float transitionVel = 1;
    Animator animGuitar;
    PlayerMovement player;
    BoxCollider collGuitar;

    bool isAttacking = false;

    private void Start()
    {
        animGuitar = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
        collGuitar = guitar.GetComponentInChildren<BoxCollider>();
    }

    private void Update()
    {
        CallGuitar();
    }

    void CallGuitar() {
        //Cuando presiona Z, activa la guitarra, indica que esta atacando y el jugador no se puede mover (solo rotar)
        if (Input.GetKeyDown(KeyCode.Z)) {
            if (!guitar.activeSelf)
            {
                guitar.SetActive(true);
            }
            isAttacking = true;
            player.canMove = false;
            StopAllCoroutines();
            StartCoroutine(Attack());
        }
        //Se hace el llamado a la animacion de ataque
        animGuitar.SetBool("Attack", isAttacking);
    }

    //Funcion que se llama en la animacion
    //Indica que ahora el jugador se puede mover y se desactiva la guitarra
    public void Move() {
        if (isAttacking) return;
        player.canMove = true;
        guitar.SetActive(false);
    }

    public void ActivateCollider() {
        collGuitar.enabled = true;
    }

    public void DeactivateCollider()
    {
        collGuitar.enabled = false;
    }

    //Despues de 2 segundos el jugador ya no esta atacando
    IEnumerator Attack() {
        yield return new WaitForSeconds(1f);
        isAttacking = false;

    }
}
