using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioSource grabbingSound;
    public AudioSource ungrabbingSound;
    public Transform destination;
    public float rotationAngle = -60;
    public Recipient recipient;
    
    private Rigidbody _rigidBody;
    private BoxCollider _boxCollider;
    private ParticleSystem _particles;
    private bool _grabbed;
    private bool _rotated;
    private bool _justRotated;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
		_particles = GetComponentInChildren<ParticleSystem>();
        _grabbed = false;
        _rotated = false;
        _justRotated = true;
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
		if(_rotated){
			_rotated = !_rotated;
			transform.rotation = Quaternion.identity;
			_particles.Stop(true);
		}
        _grabbed = false;
        _boxCollider.enabled = true;
        transform.position = new Vector3(point.x, 1.1f, point.z);
        transform.parent = null;
        _rigidBody.velocity = Vector3.zero;
        ungrabbingSound.Play();
    }

    public void RotateObject()
    {
        _rotated = !_rotated;
        if (_rotated)
        {
	        recipient.StartPouring();
	        InteractWithSubstance();
        }
        else
        {
	        recipient.StopPouring();
        }
    }

	private void InteractWithSubstance()
	{
		Vector3 raycastDir = new Vector3(0,-1f,0f);
		var sanitizedPosition = transform.position;
		sanitizedPosition.y = 1.4f;
		Debug.DrawRay(sanitizedPosition,  raycastDir * 1f, Color.red, 30);
		RaycastHit hit;
		
		if(Physics.Raycast(sanitizedPosition, raycastDir * 1f, out hit, 2f)){
			if(hit.collider.isTrigger && hit.collider.gameObject.CompareTag("Substance")){
				Substance sub = GetComponent<Substance>();
				//react with container's substance
				hit.collider.gameObject.SendMessage("ReactWith", sub);
				//empty bottle
				//sub.ChangeSubstanceAmount(-99f);
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        if (!_grabbed) return;
        
        if (_rotated && _justRotated) {
			transform.Rotate(new Vector3(rotationAngle,rotationAngle + 45,rotationAngle), Space.World);
            _justRotated = false;
			_particles.Play(true);
        } 
        else if(!_rotated) {
            transform.rotation = Quaternion.identity;
            _justRotated = true;
			_particles.Stop(true);
        }
        
        transform.position = destination.position;
    }
}
