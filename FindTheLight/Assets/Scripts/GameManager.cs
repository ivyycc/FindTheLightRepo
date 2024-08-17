using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour, IPointerEnterHandler 
{

    public int scene;
    public GameObject Transition;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Cursor Entering " + name + " GameObject");
    }
    
    public void Hover()
    {
        Debug.Log("Cursor Entering " + name + " GameObject");
        AudioManager.instance.PlayOneShot(FMODEvents.instance.hover, this.transform.position);
    }
    
    public void SettingsMenu()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.select, this.transform.position);
    }

    public void QuitGame()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.quit, this.transform.position);
        Application.Quit();
    }

    public void StartGame()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.play, this.transform.position);
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.select, this.transform.position);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.select, this.transform.position);
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene(7, LoadSceneMode.Single);
    }


    public void LoadNextScene()
    {
        scene = SceneManager.GetActiveScene().buildIndex; 
        StartCoroutine(LoadYourAsyncScene(scene+1));
    }

    public void RestartLevel()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current, LoadSceneMode.Single);
    }

    public int getCurrentLevel()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        return current;
    }

    IEnumerator LoadYourAsyncScene(int scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Don't let the Scene activate until you allow it to
        asyncLoad.allowSceneActivation = false;
        Transition.SetActive(true);
        // Wait for 5 seconds before activating the scene
        yield return new WaitForSeconds(4);

        // Now allow the scene to activate
        asyncLoad.allowSceneActivation = true;
        Transition.SetActive(false);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
