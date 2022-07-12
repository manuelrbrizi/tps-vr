using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RigidBodyMovement : MonoBehaviour
{
    // Variables and Camera
    public float moveSpeed = 0.25f;
    public float rotationRate = 5f;
    public Camera _camera;
    public Text objectText;
    public StartLab labStarter;
    
    // Auxiliars
    private bool playingFootstep;
    private AudioSource footstep;
    private Rigidbody _rigidBody;
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
	    Grab();
    }

    private void OnCaminar(InputValue value)
    {
        if (labStarter.IsStarting()) return;
        _mov = value.Get<Vector2>();
	footStep(_mov);
    }

    private void OnRotar(InputValue value){
        if(holdingSomething && !holdingObject.transform.name.Contains("Copper")) holdingObject.SendMessage("RotateObject"); //quickfix, en realidad habría que cambiar el actionmap
    }

 
     private void FixedUpdate()
     {
         if (labStarter.IsStarting()) return;
         Vector3 moveVector = _camera.transform.TransformDirection(new Vector3(_mov.x, 0, _mov.y)) * moveSpeed;
         _rigidBody.velocity = new Vector3(moveVector.x, _rigidBody.velocity.y, moveVector.z);
         var pos = transform.position;
         transform.position = new Vector3(pos.x, 1.68f, pos.z);
         SetObjectText();
     }

     private void SetObjectText()
     {
         RaycastHit hit;
        
         if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 2f))
         {
             if (hit.transform.tag.Contains("WorkingTool"))
             {
                 objectText.text = hit.transform.GetComponent<Substance>().SubstanceName;
             }
             else
             {
                 objectText.text = "";
             }
         }
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
         
         if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 3f))
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
             else if (holdingSomething && holdingObject.transform.name.Contains("Copper") && hit.transform.name.Contains("recipient"))
             {
                 holdingObject.SendMessage("UngrabObject", hit.point);
                 var selfSubstance = holdingObject.GetComponent<Substance>();
                 var recipientSubstance = selfSubstance.recipient.GetComponentInChildren<Substance>();
                 recipientSubstance.SendMessage("ReactWith", selfSubstance);
                 holdingObject = null;
                 holdingSomething = false;
             }
             else if(!holdingSomething && hit.transform.tag.Contains("Tape"))
             {
                 hit.transform.gameObject.GetComponent<Tape>().Action();
             }
             else if(!holdingSomething && hit.transform.tag.Contains("Burner"))
             {
                 hit.transform.gameObject.GetComponent<Burner>().Action();
             }
             else if (!holdingSomething && hit.transform.tag.Contains("Cabinet"))
             {
                 var anim = hit.transform.GetComponentInParent<Animator>();
                 hit.transform.GetComponentInParent<Animator>().SetBool("Close", anim.GetBool("Open"));
                 hit.transform.GetComponentInParent<Animator>().SetBool("Open", !anim.GetBool("Open"));
                 //hit.transform.GetComponent<Animator>().Play("Open");
             }
         }
     }
}
