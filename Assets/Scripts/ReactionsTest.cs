using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionsTest : MonoBehaviour
{

	public GameObject flask;
	public string react;

    void OnTriggerEnter(Collider other){
	    flask.GetComponent<Substance>().reactWith(this.react);
    }


}
