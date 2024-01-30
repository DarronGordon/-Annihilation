using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneLoader : MonoBehaviour
{
public void LoadIntroScene()
{
    SceneManager.LoadScene("Intro");
}
}
