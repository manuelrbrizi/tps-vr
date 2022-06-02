using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioSource grabbingSound;
    public AudioSource ungrabbingSound;
    public Transform destination;
    public Transform mainCamera;
	
	public float _rotationAngle = -60;
    
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
			this._particles.Stop(true);
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
		interactWithSubstance();
    }

	private void interactWithSubstance(){
		Vector3 raycastDir = new Vector3(0,-0.5f,-0.1f);
		Debug.DrawRay(transform.position,  raycastDir * 1f, Color.red, 2);
		RaycastHit hit;
		if(Physics.Raycast(transform.position, raycastDir * 1f, out hit, 1f)){
			if(hit.collider.isTrigger && hit.collider.gameObject.tag == "Substance"){
				//get this object's component
				Substance sub = GetComponent<Substance>();
				//if no substance on reactive, ignore
				if(sub.amount == 0){
					Debug.Log("No amount to react");
					return;
				}
				//react with container's substance
				hit.collider.gameObject.SendMessage("ReactWith", sub);
				//empty bottle
				sub.changeSubstanceAmount(-99f);
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        if (!_grabbed) return;
        if (_rotated && _justRotated) {
			transform.Rotate(new Vector3(_rotationAngle,_rotationAngle + 45,_rotationAngle), Space.World);
            _justRotated = false;
	    this._particles.Play(true);
        } else if(!_rotated)
        {
            transform.rotation = Quaternion.identity;
            _justRotated = true;
	    this._particles.Stop(true);
        }
        transform.position = destination.position;
    }
}
