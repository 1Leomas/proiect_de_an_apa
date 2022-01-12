using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick_move : MonoBehaviour
{
    public GameObject moveObject;
    public Joystick joystick;
    public Transform wallUp;
    public Transform wallDown;
    public Transform wallLeft;
    public Transform wallRight;
    public AudioSource Walking;

    public GameObject ariciObject;
    public GameObject ariciGoldObject;

    private Animator animationWalkingController;
    private Animator animationWalkingController2;

    float xMin;
    float xMax;
    float zMin;
    float zMax;

    float xMovement;
    float zMovement;

    float touchValue;

    private void Start()
    {
        xMin = wallLeft.position.x;
        xMax = wallRight.position.x;
        zMin = wallDown.position.z + 2f;
        zMax = wallUp.position.z;

        //Debug.Log("x: " + xMin + " - " + xMax);
        //Debug.Log("z: " + zMin + " - " + zMax);

        xMovement = joystick.Horizontal;
        zMovement = joystick.Vertical;

        touchValue = 2.1f;

        animationWalkingController = ariciObject.GetComponent<Animator>();
        animationWalkingController2 = ariciGoldObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (AI_controller.AI_mode == false) 
        {
            joystick.gameObject.SetActive(true);

            xMovement = joystick.Horizontal;
            zMovement = joystick.Vertical;
            //Debug.Log(xMovement + " " + zMovement);

            myMovementMethod2();
        }
        else
        {
            joystick.gameObject.SetActive(false);
        }
        
    }

    void myMovementMethod()
    {
        /* Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        moveObject.transform.Translate(direction * Time.deltaTime * speed);*/

        if (moveObject.transform.position.x + touchValue >= xMin)
        {
            if (xMovement < 0)
            {
                moveObj(new Vector3(xMovement, 0, 0));
            }
        }
        else if (moveObject.transform.position.x - touchValue <= xMax)
        {
            if (xMovement > 0)
            {
                moveObj(new Vector3(xMovement, 0, 0));
            }
        }
        else
        {
            moveObj(new Vector3(xMovement, 0, 0));
        }

        if (moveObject.transform.position.z + touchValue >= zMin)
        {
            if (zMovement < 0)
            {
                moveObj(new Vector3(0, 0, zMovement));
            }
        }
        else if (moveObject.transform.position.z - touchValue <= zMax)
        {
            if (zMovement > 0)
            {
                moveObj(new Vector3(0, 0, zMovement));
            }
        }
        else
        {
            moveObj(new Vector3(0, 0, zMovement));
        }
    }

    void myMovementMethod2()
    {

        Vector3 vec = new Vector3(0, 0, 0);
        
        // Daca suntem in limete ne putem misca
        if (moveObject.transform.position.x - moveObject.transform.localScale.x > xMin && moveObject.transform.position.x + moveObject.transform.localScale.x < xMax)
        {
                vec += new Vector3(xMovement, 0, 0);
        }
        // Daca am atins limetele ne putem misca in directia opusa
        else
        {
            //ne miscam in dreapta
            if (moveObject.transform.position.x - moveObject.transform.localScale.x < xMin && xMovement > 0)
            {
                vec += new Vector3(xMovement, 0, 0);
            }
            //ne miscam in stanga
            else if (moveObject.transform.position.x + moveObject.transform.localScale.x > xMax && xMovement < 0)
            {
                vec += new Vector3(xMovement, 0, 0);
            }
        }

        if (moveObject.transform.position.z - moveObject.transform.localScale.z/2f > zMin && moveObject.transform.position.z + moveObject.transform.localScale.z/2f < zMax)
        {
                vec += new Vector3(0, 0, zMovement);
        }
        else
        {
            if(moveObject.transform.position.z - moveObject.transform.localScale.z/2f < zMin && zMovement > 0)
            {
                vec += new Vector3(0, 0, zMovement);
            }
            else if(moveObject.transform.position.z + moveObject.transform.localScale.z/2f > zMax && zMovement < 0)
            {
                vec += new Vector3(0, 0, zMovement);
            }
        }


        moveObj(vec);
    }

    void myMovementMethod3()
    {

        Vector3 vec = new Vector3(0, 0, 0);

        if (moveObject.transform.position.x - touchValue <= xMin)
        {
            if (xMovement > 0)
            {
                vec += new Vector3(xMovement, 0, 0);
            }
        }
        else if (moveObject.transform.position.x + touchValue >= xMin)
        {
            if (xMovement < 0)
            {
                vec += new Vector3(xMovement, 0, 0);
            }
        }
        else
        {
            vec += new Vector3(xMovement, 0, 0);
        }

        if (moveObject.transform.position.z - touchValue <= zMin)
        {
            if (zMovement > 0)
            {
                vec += new Vector3(0, 0, zMovement);
            }
        }
        else if (moveObject.transform.position.z + touchValue >= zMin)
        {
            if (zMovement < 0)
            {
                vec += new Vector3(0, 0, zMovement);
            }
        }
        else
        {
            vec += new Vector3(0, 0, zMovement);
        }

        moveObj(vec);
    }


    private void moveObj(Vector3 newPosition)
    {
        if (newPosition != Vector3.zero)
        {
            Vector3 rotate_direction = Vector3.RotateTowards(moveObject.transform.forward,newPosition, GameConfig.player_speed/2.0f, 0.0f);
            moveObject.transform.rotation = Quaternion.LookRotation(rotate_direction);

            if(ariciObject.active == true)
                animationWalkingController.SetBool("walking", true);
            else if (ariciGoldObject.active == true)
                animationWalkingController2.SetBool("walking", true);

        }
        else
        {
            if (ariciObject.active == true)
                animationWalkingController.SetBool("walking", false);
            else if(ariciGoldObject.active == true)
                animationWalkingController2.SetBool("walking", false);
            Walking.Play();

        }

        Vector3 moveDir = newPosition.normalized;

        //Debug.Log(GameConfig.snake_speed);

        moveObject.transform.position += moveDir * GameConfig.player_speed * Time.deltaTime;

        //moveObject.transform.position += GameConfig.snake_speed * moveObject.transform.forward;

    }
}
