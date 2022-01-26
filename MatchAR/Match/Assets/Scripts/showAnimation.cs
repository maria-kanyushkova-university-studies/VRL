using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class showAnimation : MonoBehaviour
{

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(showAnima());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    IEnumerator showAnima()
    {
        yield return new WaitUntil(() => timer >= 4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
