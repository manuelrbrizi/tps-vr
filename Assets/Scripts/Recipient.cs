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
    public AudioSource fireSound;
    public ParticleSystem explosion;
    public ParticleSystem steam;
    public ParticleSystem fire;

    private void Start()
    {
        explosion.Stop();
        steam.Stop();
        fire.Stop();
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
    
    public void Steam()
    {
        StartCoroutine(SteamRecipient());
    }

    public void Fire()
    {
        StartCoroutine(StartFire());
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
        steam.startColor = new Color(1f, 0.6f, 0f, 0.5f);
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
    
    private IEnumerator SteamRecipient()
    {
        steam.startColor = new Color(0.72f, 0.88f, 0.93f, 1);
        heavyBubbling.Play();
        yield return new WaitForSeconds(2f);
        steamSound.Play();
        steam.Play();
        yield return new WaitForSeconds(8f);
        heavyBubbling.Stop();
        steam.Stop();
        steamSound.Stop();
    }
    
    private IEnumerator StartFire()
    {
        heavyBubbling.volume = 0.3f;
        heavyBubbling.Play();
        yield return new WaitForSeconds(2f);
        fire.Play();
        fireSound.Play();
        yield return new WaitForSeconds(7f);
        fire.Stop();
        yield return new WaitForSeconds(2f);
        fireSound.Stop();
        heavyBubbling.Stop();
        heavyBubbling.volume = 0.6f;
    }
}
