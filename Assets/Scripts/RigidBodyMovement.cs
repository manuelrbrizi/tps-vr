using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RigidBodyMovement : MonoBehaviour
{
    // Variables and Camera
    public float moveSpeed = 0.25f;
    public float rotationRate = 5f;
    public Camera camera;
    
    // Auxiliars
    private bool playingFootstep;
    private AudioSource footstep;
    private Rigidbody _rigidBody;
    private Vector3 moveInput;
    private bool holdingSomething;
    private GameObject holdingObject;
    private Vector2 _mov;
    private Vector2 _look;
    [SerializeField] private PlayerInput playerInput;

    private void Start()
     {
         playingFootstep = false;
         holdingSomething = false;
         holdingObject = null;
         _rigidBody = GetComponent<Rigidbody>();
         footstep = GetComponent<AudioSource>();
     }

    private void OnAgarrar(InputValue value){
        Debug.Log("Grabbing");
	    Grab();
    }

    private void OnCaminar(InputValue value){
        Debug.Log("Caminando");
	    _mov = value.Get<Vector2>();
	    moveInput = new Vector3(_mov.x, 0, _mov.y);
	    footStep(_mov);
    }

    private void OnMirar(InputValue value){
        Debug.Log("mirando");
	    _look = value.Get<Vector2>();
    }

    private void OnRotar(InputValue value){
        Debug.Log("Rotando");
        if(holdingSomething) holdingObject.SendMessage("RotateObject"); //quickfix, en realidad habría que cambiar el actionmap
    }

 
     private void FixedUpdate()
     {
         var lastAngle = camera.transform.localRotation;
         Vector3 moveVector = transform.TransformDirection(new Vector3(_mov.x, 0, _mov.y)) * moveSpeed;
         _rigidBody.velocity = new Vector3(moveVector.x, _rigidBody.velocity.y, moveVector.z);
         var pos = transform.position;
         transform.position = new Vector3(pos.x, 1.68f, pos.z);
         var xRot = camera.transform.localRotation.x;
         camera.transform.Rotate(new Vector3(- _look.y * rotationRate, 0f, 0f));   
         if ((xRot < -0.5 && _look.y < 0) || (xRot > 0.65 && _look.y > 0))  camera.transform.localRotation = lastAngle;
         transform.Rotate(0f,  _look.x * rotationRate, 0f);
     }


    private void footStep(Vector2 vec) {
         if (!playingFootstep && (_mov.x != 0 || _mov.y != 0))
         {
             playingFootstep = true;
             float volume = getVolume(Mathf.Abs(_mov.x), Mathf.Abs(_mov.y));
             footstep.volume = volume;
             footstep.Play();
             StartCoroutine(volume <= 0.8f ? FootstepCooldown(0.9f) : FootstepCooldown(0.5f));
         }
    }

     private float getVolume(float a, float b)
     {
         if(a < 0.5f && b < 0.5f)
         {
             return 0.8f;
         }

         return Mathf.Min(a+b, 1);
     }
     
     private IEnumerator FootstepCooldown(float time)
     {
         yield return new WaitForSeconds(time);
         playingFootstep = false;
     }

     private void Grab()
     {
         RaycastHit hit;
        
         if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 3f))
         {
             if (!holdingSomething && hit.transform.tag.Contains("WorkingTool"))
             {
                 holdingObject = hit.transform.gameObject;
                 holdingSomething = true;
                 hit.transform.gameObject.SendMessage("GrabObject");
             }
             else if (holdingSomething && hit.transform.tag.Contains("Desk"))
             {
                 holdingObject.SendMessage("UngrabObject", hit.point);
                 holdingObject = null;
                 holdingSomething = false;
             }
         }
     }
}
