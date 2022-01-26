
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SwipeRotate : MonoBehaviour
{
    public GameObject[] objectsToGenerate;

    public Camera ARCamera;
    
    private Touch touch;

    private Vector2 touchPosition;

    private Quaternion rotationY;

    private float rotateSpeed = 0.1f;

    private Rigidbody rig1;

    private Rigidbody rig2;

    private bool finish = false;

    public Button Restart;

    private GameObject[] pauseObjects;

    private GameObject[] objectsToDestroy = new GameObject[2];

    private GameObject[] startObjects;

    private GameObject[] ui;

    private GameObject button;

    private GameObject tree;

    private bool canPlay;

    float timer = 0.0f;

    private bool startTimer = false;

    public AudioClip choose;

    public AudioClip correct;

    public AudioClip incorrect;

    AudioSource audio;



    void Start()
    {
        Button btn = Restart.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        pauseObjects = GameObject.FindGameObjectsWithTag("levelCompletion");
        button = GameObject.FindGameObjectWithTag("levelCompletion1");
        startObjects = GameObject.FindGameObjectsWithTag("startObjects");
        ui = GameObject.FindGameObjectsWithTag("notstart");
        audio = GetComponent<AudioSource>();
        hidePaused();
        if(Time.realtimeSinceStartup > 4f && SceneManager.GetActiveScene().buildIndex > 0)
        {
            pressStart();
        }
    }

    void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        button.SetActive(false);

        foreach (GameObject g in ui)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in startObjects)
        {
            g.SetActive(false);
        }

        canPlay = false;

    }

    public void pressStart()
    {
        print("here");
        foreach (GameObject g in ui)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in startObjects)
        {
            g.SetActive(false);
        }

        //canPlay = true;
    }


    void showPaused()
    {
        var last = PlayerPrefs.GetInt("lastLevel");
        if(last < SceneManager.GetActiveScene().buildIndex + 2)
        {
            if(SceneManager.GetActiveScene().buildIndex + 2 >= SceneManager.sceneCountInBuildSettings)
            {
                PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex);
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex + 2);
                PlayerPrefs.Save();
            }
            
        }
        button.SetActive(true);
        StartCoroutine(grower());
    }

    IEnumerator grower()
    {
        startTimer = true;
        yield return new WaitUntil(() => timer >= 4.5);
        startTimer = false;
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
            ARCamera.Reset();
        }
    }

    public void TaskOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (startTimer)
        {
            tree = GameObject.FindGameObjectWithTag("finishTree");
            timer += Time.deltaTime;
            float movementSpeed = 2f;
            print(tree.transform.position.y);
            tree.transform.position = tree.transform.position + new Vector3(0, movementSpeed * Time.deltaTime, 0);
        }
        var myObjects = GameObject.FindGameObjectsWithTag("gameObject");
        if(myObjects.Length != 0)
        {
            canPlay = true;
        }
        finish = true;
        foreach(var i in myObjects){
            if(i.GetComponent<MeshRenderer>().enabled == true)
            {
                finish = false;
            }
        }
        if (finish && canPlay)
        {
            showPaused();
        }
        if (Input.touchCount > 0 && canPlay)
        {
            touch = Input.GetTouch(0);

            if (!PlayerPrefs.HasKey("firstTime"))
            {
                PlayerPrefs.SetInt("firstTime", 1);
                PlayerPrefs.Save();
            }

            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(
                    0f,
                    -touch.deltaPosition.x * rotateSpeed,
                    0f
                );

                transform.rotation = rotationY * transform.rotation;
            }
            var ray = ARCamera.ScreenPointToRay(touch.position);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (rig1 == null)
                {
                    rig1 = hitInfo.collider.GetComponent<Rigidbody>();
                    objectsToDestroy[0] = hitInfo.collider.gameObject;

                    if (rig1 != null)
                    {
                        var col = rig1.GetComponent<MeshRenderer>().material.color;
                        col.a = 0.6f;
                        rig1.GetComponent<MeshRenderer>().material.color = col;
                        audio.PlayOneShot(choose);
                    }
                }
                else
                {
                    if (rig2 == null)
                    {
                        rig2 = hitInfo.collider.GetComponent<Rigidbody>();
                        objectsToDestroy[1] = hitInfo.collider.gameObject;
                    }
                }
                if (rig2 != null) {
                    if (rig2.GetComponent<MeshRenderer>().material.color.a != 0.6f)
                    {
                        if (rig1.GetComponent<MeshRenderer>().name == rig2.GetComponent<MeshRenderer>().name)
                        {
                            audio.PlayOneShot(correct);
                            rig1.GetComponent<MeshCollider>().enabled = false;
                            rig1.GetComponent<MeshRenderer>().enabled = false;
                            rig2.GetComponent<MeshRenderer>().enabled = false;
                            rig2.GetComponent<MeshCollider>().enabled = false;
                            var col = rig1.GetComponent<MeshRenderer>().material.color;
                            col.a = 1f;
                            rig1.GetComponent<MeshRenderer>().material.color = col;
                            rig1 = null;
                            rig2 = null;
                            Destroy(objectsToDestroy[0]);
                            Destroy(objectsToDestroy[1]);
                            

                        }
                        else
                        {
                            audio.PlayOneShot(incorrect);
                            var col = rig1.GetComponent<MeshRenderer>().material.color;
                            col.a = 1f;
                            rig1.GetComponent<MeshRenderer>().material.color = col;
                            rig1 = null;
                            rig2 = null;
                            
                        }
                    }
                    else
                    {
                        rig2 = null;
                    }
                   
                }
            }
        }
        else
        {
            Vector3 vec = new Vector3(1000, 10000, 1000);
            var ray = ARCamera.ScreenPointToRay(vec);
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);
        }
    }
}
