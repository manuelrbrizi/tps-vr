using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour
{
    public AudioSource burnerSound;
    public ParticleSystem fire;

    private bool _burning;
    // Start is called before the first frame update
    void Start()
    {
        _burning = false;
        fire.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void Action()
    {
        if (_burning)
        {
            _burning = false;
            burnerSound.Stop();
            fire.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        else
        {
            _burning = true;
            burnerSound.Play();
            fire.Play();
        }
    }
}
