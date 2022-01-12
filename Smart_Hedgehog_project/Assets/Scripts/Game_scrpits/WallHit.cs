using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHit : MonoBehaviour
{

    public GameObject moveObject;
    public GameObject targetObject;
    

    void Update()
    {
        if (targetObject != null && Vector3.Distance(moveObject.transform.position, targetObject.transform.position) < 1)
        {
            

        }
    }
}
