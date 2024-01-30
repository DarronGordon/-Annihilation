using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroScenCrtl : MonoBehaviour
{
    [SerializeField]GameObject txt1;
    [SerializeField]GameObject txt2;
    IEnumerator Start()
    {
        txt1.SetActive(true);
        txt2.SetActive(false);
        yield return new WaitForSeconds(10f);
        txt1.SetActive(false);
        txt2.SetActive(true);
        yield return new WaitForSeconds(10f);

        SceneManager.LoadScene("MainMenu");
    }


}
