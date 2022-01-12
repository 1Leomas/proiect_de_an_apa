using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_controller : MonoBehaviour
{
    public static bool AI_mode;

    private void Start()
    {
        AI_mode = false;
    }

    public static void AI_state()
    {
        AI_mode = !AI_mode;
    }

    public static void AI_OfF()
    {
        AI_mode = false;
    }
}
