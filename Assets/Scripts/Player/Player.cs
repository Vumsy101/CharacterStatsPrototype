using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    public FixedJoystick joystick;

    public GameObject sword;

    public float speed;
    public float rotationSpeed = 5f;
    public float xInput;
    public float yInput;

    public bool attack;
    public bool canMove = true;
    public bool canAttack = true;

    void Start()
    {
        joystick = FindObjectOfType<FixedJoystick>();

        PlayerStats playerstats = GetComponent<PlayerStats>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckInput();
        ApplyRotation();


        if (canMove == false)
        {
            Invoke("MovementLock", 0.2f);
        }
    }
    private void FixedUpdate()
    {
        ApplyMovement();
    }

    void CheckInput()
    {
        xInput = joystick.Horizontal;
        yInput = joystick.Vertical;
    }

    void ApplyMovement()
    {
        if (xInput != 0 && (canMove == true) || yInput != 0 && (canMove == true))
        {

            Vector3 move = new Vector3(xInput * speed, yInput * speed, transform.position.z);

            transform.position += Vector3.ClampMagnitude(move, speed) * Time.deltaTime;
        }
    }

    void ApplyRotation()
    {
        if (xInput == 1 && (canMove == true))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, -90);
        }
        else if (xInput == -1 && (canMove == true))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, 90);
        }
        if (yInput == 1 && (canMove == true))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, 0);
        }
        else if (yInput == -1 && (canMove == true))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, -180);
        }
        if ((xInput == 1 && yInput == 1) && (canMove == true))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, -45);
        }
        else if ((xInput == -1 && yInput == -1) && (canMove == true))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, 135);
        }
        if ((xInput == 1 && yInput == -1) && (canMove == true))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, -135);
        }
        if ((xInput == -1 && yInput == 1) && (canMove == true))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, -45);
        }
    }

    public void SwordAttack()
    {
        if ((canAttack == true))

        {
            GameObject clonedObject = Instantiate(sword, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z), transform);
            canMove = false;
            canAttack = false;
            Destroy(clonedObject, 0.2f);
            if (canAttack == false)
            {
                Invoke("AttackLock", attackspeed);
                Invoke("MovementLock", attackspeed);
            }
        }
    }
    void MovementLock()
    {
        canMove = true;
    }
    void AttackLock()
    {
        canAttack = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
