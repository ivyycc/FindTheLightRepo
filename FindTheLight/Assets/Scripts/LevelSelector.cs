using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public int level;
    public TMP_Text levelText;

    private void Start()
    {
        levelText.text = level.ToString();
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
}
