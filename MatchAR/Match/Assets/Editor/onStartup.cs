using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class onStartup : MonoBehaviour
{
    static onStartup()
    {
        if(PlayerPrefs.HasKey("lastLevel"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("lastLevel"));
        }
    }
}
