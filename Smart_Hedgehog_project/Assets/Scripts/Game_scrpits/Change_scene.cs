using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_scene : MonoBehaviour
{
    public string level = null;

    private void OnMouseDown()
    {
        SceneManager.LoadScene(level);

        //Loading_Scene.LoadingScene(lvl);

    }
}
