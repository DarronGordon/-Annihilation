using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
