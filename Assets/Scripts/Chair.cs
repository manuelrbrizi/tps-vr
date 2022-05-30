using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public AudioSource audioSource;
    public float magnitude = 15f;
    
    private Rigidbody rb;
    private bool audioPlaying;

    private void Start()
    {
        audioPlaying = false;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag.Contains("Player"))
        {
            if (!rb.velocity.Equals(Vector3.zero))
            {
                if (audioPlaying) return;
                var tf = c.gameObject.transform.position;
                Vector3 direction = tf - transform.position;
                direction.y = 0f;
                direction.x = 0f;
                direction.Normalize();
                Debug.Log(direction);
                rb.AddForceAtPosition(direction * magnitude, tf, ForceMode.Impulse);
                audioSource.Play();
                audioPlaying = true;
                StartCoroutine(PlayAudio());
            }
        }
    }

    private IEnumerator PlayAudio()
    {
        yield return new WaitForSeconds(0.7f);
        audioPlaying = false;
    }
}
