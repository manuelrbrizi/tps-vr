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
	
	// Helpful refs
	private GameObject _copperCoin;

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
		else if(SubstanceName != "Copper")
		{
			particleRenderer = GetComponentInChildren<ParticleSystemRenderer>();
			if (SubstanceName.Equals("Empty")) return;
			ChangeSubstanceColor();
		}
	}

	void LoadColors()
	{
		_colorTable = new Dictionary<string, Color[]>{};
		_colorTable.Add("Salt Water", new []{new Color(0.72f, 0.88f, 0.93f, 1), new Color(0.72f, 0.88f, 0.93f, 1)});
		_colorTable.Add("Sodium solution", new []{Color.magenta, Color.magenta});
		_colorTable.Add("Hydrogen peroxide", new []{Color.cyan, Color.cyan});
		_colorTable.Add("Water", new []{new Color(0.72f, 0.88f, 0.93f, 1), new Color(0.72f, 0.88f, 0.93f, 1)});
		_colorTable.Add("Nitric Acid", new []{new Color(0.84f, 0.5f, 0.25f, 1), new Color(0.84f, 0.5f, 0.25f, 1)});
		_colorTable.Add("Steam", new []{new Color(0f, 0.6845f, 1f, 1), new Color(0f, 0.6845f, 1f, 1)});
		_colorTable.Add("Calcium solution", new []{new Color(0.14f, 0.28f, 0.35f, 1), new Color(0.14f, 0.28f, 0.35f, 1)});
		_colorTable.Add("Sugar", new []{new Color(1f, 1f, 1f, 1), new Color(1f, 1f, 1f, 1)});
		_colorTable.Add("Flowers", new []{new Color(0.55f, 0.2f, 0.8f, 1), new Color(0.55f, 0.2f, 0.8f, 1)});
		_colorTable.Add("Colors", new []{new Color(0.8f, 1f, 0.5f, 1), new Color(0.8f, 1f, 0.5f, 1)});
		_colorTable.Add("Substance SF", new []{new Color(1f, 0f, 0.35f, 1), new Color(1f, 0f, 0.35f, 1)});
		_colorTable.Add("Substance SC", new []{new Color(1f, 0f, 0.35f, 1), new Color(1f, 0f, 0.35f, 1)});
		_colorTable.Add("Substance FC", new []{new Color(1f, 0f, 0.35f, 1), new Color(1f, 0f, 0.35f, 1)});
		_colorTable.Add("Obsolete Substance", new []{new Color(0.5f, 0.35f, 0.25f, 1), new Color(0.5f, 0.35f, 0.25f, 1)});
	}

	public void ReactWith(Substance other){
		// Check if receipe is empty
		if (SubstanceName == "Void")
		{
			// If empty with void, return
			if (other.SubstanceName.Equals("Empty"))
			{
				return;
			}
			
			SubstanceName = other.SubstanceName;
			Debug.Log("First substance: " + SubstanceName);
			
			if (other.SubstanceName.Equals("Copper"))
			{
				_copperCoin = other.gameObject;
				return;
			}
			
			ChangeSubstanceAmount(other.SubstanceAmount);
			ChangeSubstanceColor();
			return;
		}

		if (other.SubstanceName.Equals("Empty"))
		{
			other.SubstanceName = SubstanceName;
			other.transform.Find("F_Liquid_05").gameObject.SetActive(true);
			other.flaskRenderer = other.GetComponentsInChildren<MeshRenderer>()[1];
			other.GetComponent<PickUp>().Fill();
			other.ChangeSubstanceColor();
			other.ChangeSubstanceAmount(15f);
			ResetRecipient();
			return;
		}
		
		Debug.Log(SubstanceName + " + " + other.SubstanceName);
		
		// If its the same substance, add amount and return
		ChangeSubstanceAmount(other.SubstanceAmount);
		if (other.SubstanceName == SubstanceName) return;

		// If it's a copper coin, save gameObject
		if (other.SubstanceName.Contains("Copper"))
		{
			_copperCoin = other.gameObject;
		}
		
		// Get possible reactions for this substance
		Dictionary<string, string> possibleReactions = ReactionsTable.GetReactionsFor(SubstanceName);

		// Get new substance from combination
		var newSubstance = possibleReactions.ContainsKey(other.SubstanceName)? possibleReactions[other.SubstanceName] : "Obsolete Substance";
		
		Debug.Log("New substance is: " + newSubstance);
		
		// If nothing happens, return
		if(newSubstance == SubstanceName) return;
		
		// Save new substance
		SubstanceName = newSubstance;

		switch (newSubstance)
		{
			// If reaction name is "boom", an explosion occurs
			case "Boom":
				recipient.Explode();
				StartCoroutine(RestartAfter(1.1f));
				return;

			// Steam everything!
			case "Steam":
				recipient.Steam(_copperCoin);
				ChangeSubstanceColor();
				StartCoroutine(RestartAfter(10.3f));
				return;
			
			case "White Steam":
				recipient.Steam();
				StartCoroutine(RestartAfter(10.3f));
				return;
			
			case "Fire":
				recipient.Fire();
				StartCoroutine(RestartAfter(12f));
				return;
			
			case "Substance FC":
			case "Substance SC":
			case "Substance SF":
				recipient.StartBubbling();
				ChangeSubstanceColor();
				return;
			
			case "Magic":
				recipient.DoMagic();
				StartCoroutine(RestartAfter(14f));
				return;
			
			case "Fast Steam":
				recipient.FastSteam();
				StartCoroutine(RestartAfter(10.3f));
				return;
			
			case "Fog":
				recipient.Fog();
				StartCoroutine(RestartAfter(16.3f));
				return;
			
			// If reaction generates nothing, just return 
			case null:
				return;
		}
		
		ChangeSubstanceColor();
		ChangeSubstanceAmount(other.SubstanceAmount);
	}

	public void ChangeSubstanceAmount(float amt) {
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
		var main = GetComponentInChildren<ParticleSystem>().main;
		main.startColor = newColor[1];

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
