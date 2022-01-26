using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pressButtonStart : MonoBehaviour
{

    GameObject[] gameObjects;

    public Button button; 
    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("startObjects");
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(onButtonPress);
    }

    void Update()
    {

        
    }

    public void onButtonPress()
    {
        foreach(GameObject g in gameObjects)
        {
            g.SetActive(false);
        }
    }
}
