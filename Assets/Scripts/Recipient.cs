using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipient : MonoBehaviour
{
    public AudioSource bubbling;
    public AudioSource pouring;
    public AudioSource heavyBubbling;
    public AudioSource explosionSound;
    public AudioSource steamSound;
    public ParticleSystem explosion;
    public ParticleSystem steam;

    private void Start()
    {
        explosion.Stop();
        steam.Stop();
    }

    public void StartBubbling()
    {
        bubbling.Play();
    }
    
    public void StopBubbling()
    {
        bubbling.Stop();
    }
    
    public void StartPouring()
    {
        pouring.Play();
    }

    public void StopPouring()
    {
        pouring.Stop();
    }

    public void Explode()
    {
        StartCoroutine(Explosion());
    }

    public void Steam(GameObject coin)
    {
        StartCoroutine(SteamRecipient(coin));
    }

    private IEnumerator Explosion()
    {
        heavyBubbling.Play();
        yield return new WaitForSeconds(2f);
        heavyBubbling.Stop();
        explosionSound.Play();
        explosion.Play();
        yield return new WaitForSeconds(1.1f);
        explosion.Stop();
    }
    
    private IEnumerator SteamRecipient(GameObject coin)
    {
        heavyBubbling.Play();
        yield return new WaitForSeconds(2f);
        steamSound.Play();
        steam.Play();
        yield return new WaitForSeconds(8f);
        heavyBubbling.Stop();
        steam.Stop();
        steamSound.Stop();
        Destroy(coin);
    }
}
