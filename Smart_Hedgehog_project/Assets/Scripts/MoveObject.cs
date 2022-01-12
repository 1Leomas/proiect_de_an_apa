using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject moveObject;
    public Transform targetPosition;
    public float Speed; 

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Vector3.Distance(moveObject.transform.position, targetPosition.position) < 0.4f)
            {
                Destroy(moveObject, 2);
                Application.LoadLevel("Lesson3_3");
            }
            else
            {
                moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position, targetPosition.position, Time.deltaTime * Speed);
                moveObject.transform.LookAt(targetPosition);
            }
        }
    }
}
