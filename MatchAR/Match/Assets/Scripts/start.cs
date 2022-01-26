using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour
{

    private bool fade = false;

    public float Duration = 0.4f;

    public CanvasGroup canvGroup;

    void Update()
    {
        if(fade)
        {
            canvGroup.enabled = false;
        }
    }

    public void fader()
    {

        canvGroup = GetComponent<CanvasGroup>();

        StartCoroutine(doFade(canvGroup, canvGroup.alpha, 0));
        
    }

    public IEnumerator doFade(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0f;

        while(counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            
        }

        fade = true;

        yield return null;
    }
}
