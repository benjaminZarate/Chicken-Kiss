using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float vel;
    [SerializeField] float rotationVel;

    CharacterController _controller;
    Animator _anim;
    public bool canMove = true;
    bool isMoving = false;

    Vector3 moveDown;
    float verticalVel;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        verticalVel -= 1;
        moveDown = new Vector3(0, verticalVel * 0.2f * Time.deltaTime, 0);
        _controller.Move(moveDown);

    }

    void Movement() {
        //Inputs
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        //Direccion hacia la que va el jugador
        Vector3 direction = new Vector3(-inputZ,0,inputX);

        //Animacion de correr
        _anim.SetBool("Run", isMoving);

        //Si el jugador no se mueve, se pasa a la animacion Idle y detiene su rotacion
        if (inputX == 0 && inputZ == 0) {
            isMoving = false;
            return; 
        }
        //Indica que se esta moviendo para hacer la transicion entre idle y running
        isMoving = true;

        //Rota hacia la direccion a la que se esta moviendo
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationVel * Time.deltaTime);

        //Si el j7ugador ataca, no se puede mover
        if (!canMove) return;

        //Movimiento
        _controller.Move(direction * vel * Time.deltaTime);

    }
}
