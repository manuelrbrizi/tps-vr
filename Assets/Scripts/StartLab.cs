using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLab : MonoBehaviour
{
    public AudioSource professorSpeech;
    public AudioSource doorClose;
    private bool isStarting;
    
    void Start()
    {
        isStarting = true;
        StartCoroutine(Speak());
    }

    private IEnumerator Speak()
    {
        yield return new WaitForSeconds(1.5f);
        professorSpeech.Play();
        yield return new WaitForSeconds(20f);
        doorClose.Play();
        yield return new WaitForSeconds(2f);
        isStarting = false;
    }

    public bool IsStarting()
    {
        return isStarting;
    }
}
