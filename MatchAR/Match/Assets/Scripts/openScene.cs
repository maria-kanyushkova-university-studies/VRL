using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class openScene : MonoBehaviour
{

    private GameObject[] gameObjects;

    private GameObject blur;

    private GameObject text1;
    private GameObject text2;
    private GameObject text3;

    private bool start = false;

    private bool start1 = false;
    private bool start2 = false;
    private bool start3 = false;
    private bool start4 = false;
    private bool start5 = false;

    private bool blurOn = false;

    private float timer = 0f;

    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("startObjects");
        blur = GameObject.FindGameObjectWithTag("blur");
        text1 = GameObject.FindGameObjectWithTag("1");
        text2 = GameObject.FindGameObjectWithTag("2");
        text3 = GameObject.FindGameObjectWithTag("3");
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        StartCoroutine(startLevel());
    }
    IEnumerator startLevel()
    {
        start = true;
        yield return new WaitUntil(() => timer >= 3);
        start = false;
        timer = 0f;
        onPress();
    }
    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            timer += Time.deltaTime;
        }
        if(start1)
        {
            var col = text1.GetComponent<Text>().color;
            col.a = 1 * timer;
            text1.GetComponent<Text>().color = col;
        }
        if (start4)
        {
            var col = text1.GetComponent<Text>().color;
            col.a = 1 - 1 * timer;
            text1.GetComponent<Text>().color = col;
        }
        if(start2)
        {
                var col = text2.GetComponent<Text>().color;
                col.a = 1 * timer;
                text2.GetComponent<Text>().color = col;
            
        }
        if(start5)
        {
            var col = text2.GetComponent<Text>().color;
            col.a = 1 - 1 * timer;
            text2.GetComponent<Text>().color = col;
        }
        if(start3)
        {
            
                var col = text3.GetComponent<Text>().color;
                col.a = 1 * timer;
                text3.GetComponent<Text>().color = col;
           
        }
        if(blurOn)
        {
            var col = blur.GetComponent<Image>().color;
            col.a = 1 * timer;
            blur.GetComponent<Image>().color = col;
        }
        
    }

    public void onPress()
    {
        print("here");
        foreach(GameObject g in gameObjects)
        {
            g.SetActive(false);
        }

        var col = text1.GetComponent<Text>().color;
        col.a = 0;
        text1.GetComponent<Text>().color = col;
        text2.GetComponent<Text>().color = col;
        text3.GetComponent<Text>().color = col;
        text1.SetActive(true);
        text2.SetActive(true);
        text3.SetActive(true);

        StartCoroutine(sceneAnim());
    }

    IEnumerator sceneAnim()
    {
        start = true;
        start1 = true;
        yield return new WaitUntil(() => timer >= 1);
        timer = 0f;
        start1 = false;
        yield return new WaitUntil(() => timer >= 3);
        timer = 0f;
        start4 = true;
        yield return new WaitUntil(() => timer >= 1);
        start4 = false;
        start2 = true;
        timer = 0f;
        yield return new WaitUntil(() => timer >= 1);
        start2 = false;
        yield return new WaitUntil(() => timer >= 3);
        timer = 0f;
        start5 = true;
        yield return new WaitUntil(() => timer >= 1);
        start5 = false;
        start3 = true;
        timer = 0f;
        yield return new WaitUntil(() => timer >= 1);
        start3 = false;
        yield return new WaitUntil(() => timer >= 3);
        timer = 0f;
        blurOn = true;
        yield return new WaitUntil(() => timer >= 1.5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
