using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class start2 : MonoBehaviour
{

    private GameObject blur;

    public GameObject text1;
    public GameObject text2;

    private bool start = false;

    private bool start1 = false;
    private bool start3 = false;
    private bool start4 = false;

    private bool blurOn = false;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        blur = GameObject.FindGameObjectWithTag("blur");
        //text1 = GameObject.FindGameObjectWithTag("1");
        //text2 = GameObject.FindGameObjectWithTag("2");
        text1.SetActive(false);
        text2.SetActive(false);
        blurOn = true;
        onPress();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timer += Time.deltaTime;
        }
        if (start1)
        {
            var col = text1.GetComponent<Text>().color;
            col.a = timer;
            text1.GetComponent<Text>().color = col;
        }
        if(start4)
        {
            var col = text1.GetComponent<Text>().color;
            col.a = 1 - 1 * timer;
            text1.GetComponent<Text>().color = col;
        }
        if (start3)
        {
            
                var col = text2.GetComponent<Text>().color;
                col.a = 1 * timer;
                text2.GetComponent<Text>().color = col;
            
        }
        if (blurOn)
        {
            var col = blur.GetComponent<Image>().color;
            col.a = 1 - 1 * timer;
            blur.GetComponent<Image>().color = col;
        }

    }

    void onPress()
    {
        var col = text1.GetComponent<Text>().color;
        col.a = 0;
        text1.GetComponent<Text>().color = col;
        text2.GetComponent<Text>().color = col;
        text1.SetActive(true);
        text2.SetActive(true);

        StartCoroutine(sceneAnim());
    }

    IEnumerator sceneAnim()
    {
        start = true;
        yield return new WaitUntil(() => timer >= 1);
        blurOn = false;
        timer = 0;
        start1 = true;
        yield return new WaitUntil(() => timer >= 1);
        timer = 0;
        start1 = false;
        yield return new WaitUntil(() => timer >= 3);
        timer = 0;
        start4 = true;
        yield return new WaitUntil(() => timer >= 1);
        start4 = false;
        start3 = true;
        timer = 0;
        yield return new WaitUntil(() => timer >= 1);
        start3 = false;
        yield return new WaitUntil(() => timer >= 3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
