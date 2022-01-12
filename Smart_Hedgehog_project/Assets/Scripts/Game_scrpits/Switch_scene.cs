using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch_scene : MonoBehaviour
{
    public string level = null;
    public void LoadingScene(string lvl)
    {
        SceneManager.LoadScene(lvl);

    }
}
