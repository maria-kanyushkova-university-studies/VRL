using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public Button start, Exit;
    bool isNotFirstTime;
    GameObject[] gameObjects;

    void Start()
    {
        isNotFirstTime = PlayerPrefs.HasKey("lastLevel");
        if (isNotFirstTime)
        {
            gameObjects = GameObject.FindGameObjectsWithTag("firstTime");
            foreach(GameObject g in gameObjects)
            {
                g.SetActive(false);
            }
        }
        else
        {
            gameObjects = GameObject.FindGameObjectsWithTag("notFirstTime");
            foreach (GameObject g in gameObjects)
            {
                g.SetActive(false);
            }
        }
    }

    public void onTapStart()
    {
        SceneManager.LoadScene(2);
    }

    public void onTapExit()
    {
        Application.Quit();
    }

    public void onContinue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastLevel"));
    }

    public void onChooseLevel()
    {
        SceneManager.LoadScene(1);
    }
    
}
