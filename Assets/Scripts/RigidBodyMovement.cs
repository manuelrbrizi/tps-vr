using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    public float moveSpeed = 0.25f;
    public float rotationRate = 5f;
    public Camera camera;
    private bool playingFootstep;
    private AudioSource footstep;
    private Rigidbody _Rigidbody;
    private Vector3 moveInput;
    private float minAngle = 0f;
    private float maxAngle = 360f;

    private void Start()
     {
         playingFootstep = false;
         _Rigidbody = GetComponent<Rigidbody>();
         footstep = GetComponent<AudioSource>();
     }

     /*private void Update()
     {
         /* To add values to the moveInput property you write "new Vector3 (x, y, z)" and fill those
         values with the inputs that you would like to use. In my case I used for the X axis
         Input.GetAxis("Horizontal") to get the input values from the default
         keys A, D, Left Arrow and Right Arrow and for the Z axis Input.GetAxis("Vertical") to
         get the default input values from the keys W, S, Up Arrow and Down Arrow.
         #1#
 
         // Try to always get your player inputs in the Update method.
         
     }*/
 
     private void FixedUpdate()
     {
         // Rigidbody actions are handled by Unity's physics engine, so you should always mess with
         // rigidbody stuff inside FixedUpdate, this will guarantee consistent physics behaviour.
         
         // After this you just simply use your rigidbody position with a += moveInput (like you did) and multiply
         // that Vector3 by a property I called moveSpeed so that you can control how fast your object should move.
         var horizontalInput = Input.GetAxis("Horizontal");
         var verticalInput = Input.GetAxis("Vertical");
         var pitch = Input.GetAxis("Pitch");
         var roll = Input.GetAxis("Roll");
         var lastAngle = camera.transform.localRotation;

         if (!playingFootstep && (horizontalInput != 0 || verticalInput != 0))
         {
             playingFootstep = true;
             footstep.Play();
             StartCoroutine(FootstepCooldown());
         }
         
         moveInput = new Vector3(horizontalInput, 0, verticalInput);
         //_Rigidbody.position += moveInput * Time.deltaTime;
         Vector3 moveVector = transform.TransformDirection(moveInput) * moveSpeed;
         _Rigidbody.velocity = new Vector3(moveVector.x, _Rigidbody.velocity.y, moveVector.z);
         //transform.Rotate(0f, Input.GetAxis("Pitch") * rotationRate, 0f, Space.World);
         //transform.Rotate(Input.GetAxis("Roll") * rotationRate,  0f, 0f, Space.Self);
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
     
     private IEnumerator FootstepCooldown()
     {
         yield return new WaitForSeconds(0.5f);
         playingFootstep = false;
     }
}
