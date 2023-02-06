using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    public SceneReference scene;

    private void OnCollisionEnter(Collision other)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
