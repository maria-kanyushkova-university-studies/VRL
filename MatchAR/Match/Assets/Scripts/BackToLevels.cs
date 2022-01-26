using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToLevels : MonoBehaviour
{
    Button restart;

    public void pressBack()
    {
        SceneManager.LoadScene(1);
    }
}
