using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionsTable : MonoBehaviour
{

	public Dictionary<string, Dictionary<string, string>> reactionsTable;
	public Dictionary<string, Color[]> colorTable;


    // Start is called before the first frame update
    void Start()
    {
	//building dicts
	this.reactionsTable = new Dictionary<string, Dictionary<string, string>>();
	this.colorTable = new Dictionary<string, Color[]>();
	//testing puporses, setting up water
	Dictionary<string, string> waterDict = new Dictionary<string, string>();
	waterDict["NaCl"] = "aguita";
	waterDict["Na"] = "boom";
	colorTable.Add("aguita", new Color[]{ new Color(0.6f, 0.1f, 0.4f, 1), new Color(0.4f, 0.1f, 0.6f, 1)});
        this.reactionsTable["water"] = waterDict;
    }

    public Dictionary<string, string> getReactionsFor(string substance){
	    return this.reactionsTable[substance];
    }

    public Color[] getSubstanceColorOf(string substance){
	    //hardcoded, should go away
	    if(substance == "water"){
		return new Color[]{ new Color(50, 140, 180), new Color(136, 255, 247)};
		
	    }
	    return this.colorTable[substance];
    }

}
