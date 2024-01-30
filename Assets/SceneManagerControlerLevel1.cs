using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerControlerLevel1 : MonoBehaviour
{
    public static SceneManagerControlerLevel1 Instance;

[SerializeField]GameObject sliderImg;
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

        StartCoroutine(FadeInStart(0f));
    }

   
    

#region [[  SCENE-LOADER]]

    public void LoadLevelScene(string sceneName)
    {
        StartCoroutine(FadeToLevel(1f,sceneName));
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

        IEnumerator FadeToLevel(float finalAlpha, string sceneName)
    {
        isFading = true;

        faderCanvasGroup.blocksRaycasts = true;

        while(!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeDuration * Time.deltaTime);

            yield return null;
        }

        isFading = false;

        faderCanvasGroup.blocksRaycasts = false;

         SceneManager.LoadScene(sceneName);
    }

        IEnumerator FadeInStart(float finalAlpha)
    {
        isFading = true;

        faderCanvasGroup.blocksRaycasts = true;

        yield return new WaitForSeconds(1f);

        while(!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeDuration * Time.deltaTime);

            yield return null;
        }

        isFading = false;

        faderCanvasGroup.blocksRaycasts = false;

    }
    IEnumerator Fade(float finalAlpha)
    {
        isFading = true;

        faderCanvasGroup.blocksRaycasts = true;

        while(!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeDuration * Time.deltaTime);

            yield return null;
        }

        isFading = false;

        faderCanvasGroup.blocksRaycasts = false;
    }

    #endregion

    public void FlickerIcon()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        sliderImg.SetActive(false);
        yield return new WaitForSeconds(.5f);
        sliderImg.SetActive(true);
        yield return new WaitForSeconds(.5f);
        sliderImg.SetActive(false);
        yield return new WaitForSeconds(.5f);
        sliderImg.SetActive(true);
        yield return new WaitForSeconds(.5f);
        sliderImg.SetActive(false);
        yield return new WaitForSeconds(.5f);
        sliderImg.SetActive(true);
        yield return new WaitForSeconds(.5f);
        sliderImg.SetActive(false);
        yield return new WaitForSeconds(.5f);
        sliderImg.SetActive(true);
        yield return new WaitForSeconds(.5f);
        sliderImg.SetActive(false);
        yield return new WaitForSeconds(.5f);
        sliderImg.SetActive(true);
    }
}
