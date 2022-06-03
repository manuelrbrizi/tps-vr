using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Substance : MonoBehaviour {
	
	// Parameters
	public string SubstanceName;
	public float SubstanceAmount;
	public Recipient recipient;
	
	private MeshRenderer flaskRenderer;
	private ParticleSystemRenderer particleRenderer;
	
	// Color dictionary
	private Dictionary<string, Color[]> _colorTable;

	private const float rangeSteps = 5;
	//amuont must be between 0.44 and 0.56 because of shaderd's implementation
	private const float minFillAmount = 0.44f;
	private const float maxFillAmount = 0.56f;
	private const float fillRange = maxFillAmount - minFillAmount;
	private const float fillSteps = fillRange / rangeSteps;

	void Start()
	{
		LoadColors();
		var allRenderers = GetComponentsInChildren<MeshRenderer>();
		flaskRenderer = allRenderers.Length > 1 ? allRenderers[1] : allRenderers[0];
		
		if (SubstanceName == "Void")
		{
			particleRenderer = GetComponent<ParticleSystemRenderer>();
		}
		else
		{
			particleRenderer = GetComponentInChildren<ParticleSystemRenderer>();
			ChangeSubstanceColor();
		}
	}

	void LoadColors()
	{
		_colorTable = new Dictionary<string, Color[]>{};
		_colorTable.Add("Salt Water", new []{new Color(0.72f, 0.88f, 0.93f, 1), new Color(0.72f, 0.88f, 0.93f, 1)});
		_colorTable.Add("Sodium", new []{Color.magenta, Color.magenta});
		_colorTable.Add("Water", new []{new Color(0.72f, 0.88f, 0.93f, 1), new Color(0.72f, 0.88f, 0.93f, 1)});
		_colorTable.Add("Uranium", new []{new Color(0.03f, 1f, 0f, 1), new Color(0.03f, 1f, 0f, 1)});
		_colorTable.Add("Cold Uranium", new []{new Color(0.2f, 0.59f, 0.19f, 1), new Color(0.2f, 0.59f, 0.19f, 1)});
	}

	public void ReactWith(Substance other){
		// Check if receipe is empty
		if (SubstanceName == "Void")
		{
			SubstanceName = other.SubstanceName;
			ChangeSubstanceAmount(other.SubstanceAmount);
			ChangeSubstanceColor();
			Debug.Log("First substance: " + SubstanceName);
			return;
		}
		
		Debug.Log(SubstanceName + " + " + other.SubstanceName);
		
		// If its the same substance, add amount
		if(other.SubstanceName == SubstanceName){
			ChangeSubstanceAmount(other.SubstanceAmount);
			return;
		}
		
		// Get possible reactions for this substance
		Dictionary<string, string> possibleReactions = ReactionsTable.GetReactionsFor(SubstanceName);
		
		// Get new substance from combination
		var newSubstance = possibleReactions[other.SubstanceName];
		Debug.Log("New substance is: " + newSubstance);
		
		// If nothing happens, return
		if(newSubstance == SubstanceName) return;

		switch (newSubstance)
		{
			// If reaction name is "boom", an explosion occurs
			case "Boom":
				recipient.Explode();
				StartCoroutine(RestartAfter(1.1f));
				//ResetRecipient();
				return;

			// If reaction generated new substance, change this substance's name and color and do stuff
			case "Cold Uranium":
				recipient.StartBubbling();
				StartCoroutine(RestartAfter(10f));
				break;
			
			// If reaction generates nothing, just return 
			case null:
				return;
		}

		SubstanceName = newSubstance;
		ChangeSubstanceColor();
		ChangeSubstanceAmount(other.SubstanceAmount);
	}

	private void ChangeSubstanceAmount(float amt) {
		if(amt > 0){
			SubstanceAmount += amt;
			//liquid overflow, for now, it is not possible,
			//maybe in the future we can add a split particle
			if(SubstanceAmount > 10) SubstanceAmount = 1;
		}
		else{
			//removing amount
			SubstanceAmount += amt;
			if(SubstanceAmount < 0) SubstanceAmount = 0;
		}
		
		flaskRenderer.material.SetFloat("_FillAmount", minFillAmount + fillSteps * SubstanceAmount);
	}

	private void ChangeSubstanceColor(){
		// Get Color
		var newColor = _colorTable[SubstanceName];
		
		// Changing Material properties
		flaskRenderer.material.SetColor("_Tint", newColor[0]);
		flaskRenderer.material.SetColor("_TopColor", newColor[1]);
		
		// Also change particle's color
		if (particleRenderer == null) return;
		Material m = Instantiate(particleRenderer.material);
		m.SetColor("_Color", newColor[0]);
		Destroy(particleRenderer.material);
		particleRenderer.trailMaterial = m;
	}

	private void ResetRecipient()
	{
		SubstanceName = "Void";
		ChangeSubstanceAmount(-99f);
	}

	private IEnumerator RestartAfter(float t)
	{
		yield return new WaitForSeconds(t);
		recipient.StopBubbling();
		ResetRecipient();
	}
}
