using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{

    public Button Lvl1, Lvl2, Lvl3, Lvl4;

    public Color colorDisabled;

    public Color colorEnabled;

    GameObject[] gameObjects;

    int lastLevel;

    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("levelButton");

        if (PlayerPrefs.HasKey("lastLevel"))
        {
            lastLevel = PlayerPrefs.GetInt("lastLevel");
        }
        else
        {
            lastLevel = 2;
            PlayerPrefs.SetInt("lastLevel", lastLevel);
            PlayerPrefs.Save();
        }

        foreach(GameObject g in gameObjects)
        {
            int levelButton;
            int.TryParse(g.name, out levelButton);
            if (levelButton + 1 <= lastLevel)
            {
                g.GetComponent<Button>().image.color = colorEnabled;
            }
            else
            {
                g.GetComponent<Button>().image.color = colorDisabled;
            }
        }
    }

    public void levelToLoad(int level)
    {
        if(level <= lastLevel)
        {
            SceneManager.LoadScene(level);
        }
    }
}
