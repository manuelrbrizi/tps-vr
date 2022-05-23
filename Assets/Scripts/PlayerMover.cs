using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float rotationSpeed = 2.5f;
    private bool playingFootstep;
    private AudioSource footstep;

    // Start is called before the first frame update
    void Start()
    {
        footstep = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if (!playingFootstep && (horizontalInput != 0 || verticalInput != 0))
        {
            playingFootstep = true;
            footstep.Play();
            StartCoroutine(FootstepCooldown());
        }
        
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime, Space.Self);
        transform.Translate(new Vector3(0f, 0f, verticalInput) * moveSpeed * Time.deltaTime, Space.Self);
        var pos = transform.position;
        transform.position = new Vector3(pos.x, 1.68f, pos.z);
        transform.Rotate(0f, Input.GetAxis("Pitch") * rotationSpeed, 0f, Space.World);
        transform.Rotate(Input.GetAxis("Roll") * rotationSpeed,  0f, 0f, Space.Self);
    }
    
    private IEnumerator FootstepCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        playingFootstep = false;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("WE HIT AN OBSTACLE");
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("COLISION");
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
