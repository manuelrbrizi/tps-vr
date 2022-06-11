using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    public ParticleSystem water;
    public AudioSource waterPouring;
    
    private bool _pouring;
    
    // Start is called before the first frame update
    void Start()
    {
        _pouring = false;
        water.Stop();
    }

    public void Action()
    {
        if (_pouring)
        {
            _pouring = false;
            waterPouring.Stop();
            water.Stop();
        }
        else
        {
            _pouring = true;
            waterPouring.Play();
            water.Play();
        }
    }
}
