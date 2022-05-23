using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioSource grabbingSound;
    public AudioSource ungrabbingSound;
    
    public Transform destination;
    private Rigidbody _rigidBody;
    private BoxCollider _boxCollider;
    private bool _grabbed;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _grabbed = false;
    }

    public void GrabObject()
    {
        if (_grabbed) return;
        _grabbed = true;
        _boxCollider.enabled = false;
        transform.position = destination.position;
        transform.parent = GameObject.FindWithTag("Destination").transform;
        _rigidBody.velocity = Vector3.zero;
        grabbingSound.Play();
    }

    public void UngrabObject(Vector3 point)
    {
        if (!_grabbed) return;
        _grabbed = false;
        _boxCollider.enabled = true;
        transform.position = new Vector3(point.x, 1.1f, point.z);
        transform.parent = null;
        _rigidBody.velocity = Vector3.zero;
        ungrabbingSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_grabbed) return;
        transform.rotation = Quaternion.identity;
        transform.position = destination.position;
    }
}
