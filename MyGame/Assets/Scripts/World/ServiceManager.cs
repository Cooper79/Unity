using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiceManager : MonoBehaviour
{

    #region SingleTone
    public static ServiceManager Instanse;
    private void Awake()
    {
        if (Instanse == null)
        {
            Instanse = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    private void Start()
    {
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayerPrefs.SetInt(GamePrefs.LastPlayedLVL.ToString(), SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt(GamePrefs.LVLPlayed.ToString() + SceneManager.GetActiveScene().buildIndex, 1);
        }
    }

    public void Restart()
    {
        ChangeLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndLevel()
    {
        ChangeLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeLevel(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}

public enum Scenes
{
    MainMenu,
    First,
    Second,
    Third
}

public enum GamePrefs
{
    LastPlayedLVL,
    LVLPlayed,
}