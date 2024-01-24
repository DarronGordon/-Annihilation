using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerControler : MonoBehaviour
{
    public static SceneManagerControler Instance;


    [SerializeField]CanvasGroup faderCanvasGroup;
    [SerializeField]UnityEngine.UI.Image fadeOutImg;
    [SerializeField]float fadeDuration;
    [SerializeField]bool isFading;

    private void Awake() 
    {

        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }   

    void Start()
    {
        fadeOutImg.color = new Color(0f, 0f, 0f, 1f);

        faderCanvasGroup.alpha = 1f;

        StartCoroutine(Fade(0f));
    }

   
    

#region [[  SCENE-LOADER]]

    public void LoadIntroScene()
    {
         StartCoroutine(Fade(1f));
        SceneManager.LoadScene("Intro");
    }
    public void TutScene()
    {
        StartCoroutine(Fade(1f));
        SceneManager.LoadScene("Tut");
    }
    public void LoadLevel1Scene()
    {
        StartCoroutine(Fade(1f));
        SceneManager.LoadScene("Level#1");
    }
    public void LoadLevelScene(string sceneName)
    {
        StartCoroutine(Fade(1f));
        SceneManager.LoadScene(sceneName);
    }
    public void MainMenuScene()
    {
        StartCoroutine(Fade(1f));
        SceneManager.LoadScene("MainMenu");
    }
    public void ResetScene()
    {
        StartCoroutine(Fade(1f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

    #region [[FADE-IN FADE-OUT]]


    public void FadeOut()
    {
        StartCoroutine(Fade(1f));
    }
    IEnumerator Fade(float finalAlpha)
    {
        isFading = true;

        faderCanvasGroup.blocksRaycasts = true;

        //float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        while(!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeDuration * Time.deltaTime);

            yield return null;
        }

        isFading = false;

        faderCanvasGroup.blocksRaycasts = false;
    }

    #endregion
}
