using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Substance : MonoBehaviour {

	public string name;
	public float amount;
	public GameObject tableObj;

	private ReactionsTable table;
	private MeshRenderer flaskRenderer;
	private ParticleSystemRenderer particleRenderer;

	private const float rangeSteps = 5;
	//amuont must be between 0.44 and 0.56 because of shaderd's implementation
	private const float minFillAmount = 0.44f;
	private const float maxFillAmount = 0.56f;
	private const float fillRange = maxFillAmount - minFillAmount;
	private const float fillSteps = fillRange / rangeSteps;

	void Start(){
		this.table = tableObj.GetComponent<ReactionsTable>();
		//changeSubstanceColor(table.getSubstanceColorOf(this.name)); //TODO review if it needs to be setup on start, the other object might need to be on Awake()
		MeshRenderer[] allRenderers = this.GetComponentsInChildren<MeshRenderer>();
		this.particleRenderer = this.GetComponentInChildren<ParticleSystemRenderer>();
		if(allRenderers.Length > 1){
			this.flaskRenderer = allRenderers[1];
		}else{
			this.flaskRenderer = allRenderers[0];
		}
	}

	public void ReactWith(Substance other){
		ReactWith(other.name, other.amount);
	}

	public void ReactWith(string other, float addedAmount){
		Debug.Log(this.name + " has reacted with " + other);
		//if its the same substance, add amount
		if(other == this.name){
			changeSubstanceAmount(addedAmount);
			return;
		}
		//get possible reactions for this substance
		Dictionary<string, string> possibleReactions = table.getReactionsFor(this.name);
		//get new substance that is reaction with this and other
		string newSubstance = possibleReactions[other];
		Debug.Log("new substance is " + newSubstance);
		//check the result of the reaction
		//nothing happened, return
		if(newSubstance == this.name) return;
		//if reaction name is "boom", an explosion occurs
		if(newSubstance == "boom"){
			explode();
		//reaction generated new substance
		//change this substance's name and color
		}else if(newSubstance != null){
			this.name = newSubstance;
			changeSubstanceColor(table.getSubstanceColorOf(this.name));
			changeSubstanceAmount(addedAmount);
		}
	}

	public void changeSubstanceAmount(float amount) {
		if(amount > 0){
			//adding amount
			this.amount += amount;
			//liquid overflow, for now, it is not possible,
			//maybe in the future we can add a split particle
			if(this.amount > 10) this.amount = 1;
		}else{
			//removing amount
			this.amount += amount;
			if(this.amount < 0) this.amount = 0;
		}
		this.flaskRenderer.material.SetFloat("_FillAmount", minFillAmount + fillSteps * this.amount);
	}

	private void changeSubstanceColor(Color[] newColor){
		//changing properties
		this.flaskRenderer.material.SetColor("_Tint", newColor[0]);
		this.flaskRenderer.material.SetColor("_TopColor", newColor[1]);
		//also change particle's color
		Material m = Instantiate(this.particleRenderer.material);
		m.SetColor("_Color", newColor[0]);
		Destroy(particleRenderer.material);
		this.particleRenderer.trailMaterial = m;
	}


	private void explode(){
		Debug.Log("Explosion");
	}

}
