using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;

    public SceneReference MainGameScene;

    public void StartGame()
    {
        loadingScreen.SetActive(true);

        SceneManager.LoadSceneAsync(MainGameScene);
    }
}
