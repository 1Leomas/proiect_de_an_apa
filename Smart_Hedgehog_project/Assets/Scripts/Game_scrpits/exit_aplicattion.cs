using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit_aplicattion : MonoBehaviour
{

    public void exit_game()
    {
        Application.Quit();
    }

    private void OnMouseDown()
    {
        Application.Quit();
    }
}
