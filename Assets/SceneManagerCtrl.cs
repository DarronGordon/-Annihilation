using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManagerCtrl : MonoBehaviour
{
    public static SceneManagerCtrl Instance;
    [SerializeField]int sceneCheckPoints;

    [SerializeField]Vector2 currentSpawnPointPos;

    [SerializeField]CanvasGroup faderCanvasGroup;
    [SerializeField]UnityEngine.UI.Image fadeOutImg;
    [SerializeField]float fadeDuration;
    [SerializeField]bool isFading;
    [SerializeField]GameObject gamePanelGUI;
    [SerializeField]GameObject player;

    [SerializeField]List<CheckPointCtrl> checkPoints = new List<CheckPointCtrl>() ;

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

    private void OnEnable() {
        EventHandlerManager.onPlayerCheckPointActivateEvent +=SetCurrentCheckPoint;
    }
    private void OnDisable() {
        EventHandlerManager.onPlayerCheckPointActivateEvent -=SetCurrentCheckPoint;
    }
    void Start()
    {
        fadeOutImg.color = new Color(0f, 0f, 0f, 1f);

        faderCanvasGroup.alpha = 1f;

        CheckCheckPointProgress();
        SetCurrentSpawnPoint();

        StartCoroutine(Fade(0f));
        //SCENE LOADED EVENT
    }

    private void CheckCheckPointProgress()
    {
        CheckPointCtrl checkpointC = null;
        int checkPointsActiveAmount = 0;

        foreach(CheckPointCtrl cpc in checkPoints)
        {
            if(cpc.isCheckPointActive)
            {
                checkPointsActiveAmount ++;
                checkpointC = cpc;
            }
        }

        if(checkPointsActiveAmount <= 0)
        {
            checkpointC = checkPoints[0];
        }

        SetCurrentCheckPoint(checkpointC.id, checkpointC.gameObject.transform.position);
    }
    #region [[  CHECKPOINT-SYSTEM ]]

    public void SetCurrentSpawnPoint()
    {
        player.transform.position = currentSpawnPointPos;
    }

    public void SetCurrentCheckPoint(int checkpointID, Vector2 pos)
    {
        currentSpawnPointPos = pos;
    }

    #endregion

#region [[  SCENE-LOADER]]

    public void InroScene()
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

    public void LoadLastCheckPointAndFadeOut()
    {
        StartCoroutine(FadeAndLoadCheckPoint());
        
    }
    public void FadeIn()
    {
        StartCoroutine(Fade(0f));
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
    IEnumerator FadeAndLoadCheckPoint()
    {

        yield return StartCoroutine(Fade(1f));

        LoadLastCheckPoint();
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerMovementCtrl>().ResetPlayer();
        yield return StartCoroutine(Fade(0f));
    }
    public void LoadLastCheckPoint()
    {
        SetCurrentSpawnPoint();
    }



    #endregion
}
