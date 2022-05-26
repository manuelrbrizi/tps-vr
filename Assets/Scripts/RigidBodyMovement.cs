using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
     {
         playingFootstep = false;
         holdingSomething = false;
         holdingObject = null;
         _rigidBody = GetComponent<Rigidbody>();
         footstep = GetComponent<AudioSource>();
     }
 
     private void FixedUpdate()
     {
         var horizontalInput = Input.GetAxis("Horizontal");
         var verticalInput = Input.GetAxis("Vertical");
         var pitch = Input.GetAxis("Pitch");
         var roll = Input.GetAxis("Roll");
         var lastAngle = camera.transform.localRotation;

         if (Input.GetKeyDown(KeyCode.Joystick1Button1))
         {
             Grab();
         }

         if (!playingFootstep && (horizontalInput != 0 || verticalInput != 0))
         {
             playingFootstep = true;
             float volume = getVolume(Mathf.Abs(horizontalInput), Mathf.Abs(verticalInput));
             footstep.volume = volume;
             Debug.Log(volume);
             footstep.Play();
             StartCoroutine(volume <= 0.8f ? FootstepCooldown(0.9f) : FootstepCooldown(0.5f));
         }
         
         moveInput = new Vector3(horizontalInput, 0, verticalInput);
         Vector3 moveVector = transform.TransformDirection(moveInput) * moveSpeed;
         _rigidBody.velocity = new Vector3(moveVector.x, _rigidBody.velocity.y, moveVector.z);
         var pos = transform.position;
         transform.position = new Vector3(pos.x, 1.68f, pos.z);
         var xRot = camera.transform.localRotation.x;
         camera.transform.Rotate(new Vector3(roll * rotationRate, 0f, 0f));   
         if ((xRot < -0.5 && roll < 0) || (xRot > 0.65 && roll > 0))
         {
             camera.transform.localRotation = lastAngle;
         }
         
         transform.Rotate(0f,  pitch * rotationRate, 0f);
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
