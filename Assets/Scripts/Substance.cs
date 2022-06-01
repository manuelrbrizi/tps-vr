using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Substance : MonoBehaviour {

	public string name;
	public GameObject tableObj;

	private ReactionsTable table;

	void Start(){
		table = tableObj.GetComponent<ReactionsTable>();
		//changeSubstanceColor(table.getSubstanceColorOf(this.name));
	}

	public void reactWith(string other){
		Debug.Log(this.name + " has reacted with " + other);
		//get possible reactions for this substance
		Dictionary<string, string> possibleReactions = table.getReactionsFor(this.name);
		//get new substance that is reaction with this and other
		string newSubstance = possibleReactions[other];
		Debug.Log("new substance is " + newSubstance);
		//check the result of the reaction
		//nothing happened, return
		if(newSubstance == this.name) return;
		//if reaction name is "boom", an explision occurs
		if(newSubstance == "boom"){
			explode();
		//reaction generated new substance
		//change this substance's name and color
		}else if(newSubstance != null){
			this.name = newSubstance;
			changeSubstanceColor(table.getSubstanceColorOf(this.name));
		}
	}

	private void changeSubstanceColor(Color[] newColor){
		//grabbing children renderer ignoring parent
		MeshRenderer[] allRenderers = this.GetComponentsInChildren<MeshRenderer>();
		MeshRenderer renderer = null;
		foreach(MeshRenderer m in allRenderers){
			if(m.gameObject.GetInstanceID() != this.GetInstanceID()){
				renderer = m;
			}
		}
		//changing properties
		renderer.material.SetColor("_Tint", newColor[0]);
		renderer.material.SetColor("_TopColor", newColor[1]);
		renderer.material.SetFloat("_FillAmount", 0.44f);
	}


	private void explode(){
		Debug.Log("Explosion");
	}

}
