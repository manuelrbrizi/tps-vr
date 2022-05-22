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
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var rotation = transform.rotation;
        var dir = new Vector3(horizontalInput, 0, verticalInput);

        /*if (rotation.y < -90 || rotation.y > 90)
        {
            verticalInput *= -1;
        }*/
        
        Debug.Log(horizontalInput + " " + verticalInput);

        if (!playingFootstep && (horizontalInput != 0 || verticalInput != 0))
        {
            playingFootstep = true;
            footstep.Play();
            StartCoroutine(FootstepCooldown());
        }
        
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, Space.Self);
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
}
