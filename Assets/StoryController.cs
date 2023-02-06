using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    public SceneReference scene;

    public void ButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
