using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class generator : MonoBehaviour
{

    void Start()
    {
        print(Time.realtimeSinceStartup);
        if(PlayerPrefs.HasKey("lastLevel"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("lastLevel"));
        }
    }

}
