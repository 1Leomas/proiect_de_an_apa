using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTail : MonoBehaviour
{
    //public GameObject snakeTarget; //obiectul spre cine tinde sarpele
    public Transform targetPosition;

    float hitDistance;


    private void Start()
    {
        //hitDistance = ((targetPosition.localScale.x) + (transform.localScale.x)) * 1.1f;
        hitDistance = (targetPosition.transform.localScale.x/2);

        //Debug.Log(hitDistance);
    }

    void Update()
    {

        if (Vector3.Distance(transform.position, targetPosition.transform.position) >= hitDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, Time.deltaTime * GameConfig.player_speed);

            //snakeTarget.transform.LookAt(targetPosition);

            //transform.RotateAround(targetPosition.position, new Vector3(-1,0,-1) , 9.0f * Time.deltaTime);

        }
    }

}

