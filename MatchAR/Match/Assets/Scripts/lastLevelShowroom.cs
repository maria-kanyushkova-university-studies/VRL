using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lastLevelShowroom : MonoBehaviour
{

    private Touch touch;

    private Quaternion rotationY;

    private float rotateSpeed = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(
                    0f,
                    -touch.deltaPosition.x * rotateSpeed,
                    0f
                );

                transform.rotation = rotationY * transform.rotation;
            }
        }
    }

    public void reset()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
