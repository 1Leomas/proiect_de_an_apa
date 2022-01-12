using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallUpStatusPower : MonoBehaviour
{
    public List<Animator> myAnimationController;

    public static bool animation_wall_turn_off;

    private void Start()
    {
        animation_wall_turn_off = false;
    }

    void Update()
    {
        if (MoveHead.superPower)
        {
            myAnimationController.ForEach(anim => anim.SetBool("startAnimation", true));         
        }
        if(animation_wall_turn_off == true)
        {
            myAnimationController.ForEach(anim => anim.SetBool("startAnimation", false));
            animation_wall_turn_off = false;
        }
    }
}
