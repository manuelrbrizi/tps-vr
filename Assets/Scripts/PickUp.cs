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
    private bool _playingFillingSound;
    private bool _wasEmpty;
    
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
		_particles = transform.name.Contains("Empty")? null : GetComponentInChildren<ParticleSystem>();
        _grabbed = false;
        _rotated = false;
        _justRotated = true;
        _playingFillingSound = false;
        _wasEmpty = false;
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
	        if (_particles != null)
	        {
		        recipient.StartPouring();
	        }
	        
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
		RaycastHit hit;
		
		if(Physics.Raycast(sanitizedPosition, raycastDir * 1f, out hit, 2f)){
			if(hit.collider.isTrigger && hit.collider.gameObject.CompareTag("Substance")){
				Substance sub = GetComponent<Substance>();
				//react with container's substance
				hit.collider.gameObject.SendMessage("ReactWith", sub);
				//empty bottle
				//sub.ChangeSubstanceAmount(-99f);
			}
			else if (hit.transform.gameObject.tag.Contains("Sink"))
			{
				Substance sub = GetComponent<Substance>();
				sub.transform.Find("F_Liquid_05").gameObject.SetActive(false);
				sub.SubstanceName = "Empty";
				sub.ChangeSubstanceAmount(-99f);
				StartCoroutine(NullifyParticles());
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
	        if (_particles != null && !_wasEmpty)
	        {
		        _particles.Play(true);
	        }
        } 
        else if(!_rotated) {
	        transform.rotation = Quaternion.identity;
	        if (_playingFillingSound)
	        {
		        transform.Find("Filling Sound").GetComponent<AudioSource>().Stop();
		        _playingFillingSound = false;
	        }
	        if (_wasEmpty)
	        {
		        _wasEmpty = false;
	        }
	        if (_particles != null)
	        {
		        _particles.Stop(true); 
	        }
	        
	        _justRotated = true;
        }

        transform.position = destination.position;
    }

    public void Fill()
    {
	    _particles = GetComponentInChildren<ParticleSystem>();
	    _playingFillingSound = true;
	    _wasEmpty = true;
	    transform.Find("Filling Sound").GetComponent<AudioSource>().Play();
    }

    private IEnumerator NullifyParticles()
    {
	    yield return new WaitForSeconds(3f);
	    _particles = null;
    }
}
