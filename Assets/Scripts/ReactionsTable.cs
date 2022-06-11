using System.Collections.Generic;
using UnityEngine;

public class ReactionsTable : MonoBehaviour
{
	private static Dictionary<string, Dictionary<string, string>> _reactionsTable = Initialize();

	/*
	  Every reaction is stored in the dictionary, using the substance name as key and another
	  dictionary using other substance name as key and result substance as value
	*/
	static Dictionary<string, Dictionary<string, string>> Initialize()
    {
		// Instantiate dictionaries
		_reactionsTable = new Dictionary<string, Dictionary<string, string>>();

		// Water dictionaries
		var waterDict = new Dictionary<string, string>
		{
			["Sodium"] = "Boom",
			["Uranium"] = "Cold Uranium",
			["Calcium"] = "White Steam"
		};
		
		var calciumDict = new Dictionary<string, string>
		{
			["Water"] = "White Steam"
		};

		// Sodium dictionaries
		var sodiumDict = new Dictionary<string, string>
		{
			["Water"] = "Boom",
			["Uranium"] = "Cold Uranium"
		};

		// Uranium dictionaries
		var uraniumDict = new Dictionary<string, string>
		{
			["Sodium"] = "Boom",
			["Water"] = "Cold Uranium"
		};
		
		var nitricAcidDict = new Dictionary<string, string>
		{
			["Copper"] = "Steam"
		};
		
		var copperDict = new Dictionary<string, string>
		{
			["Nitric Acid"] = "Steam"
		};

		// Adding dictionaries to reaction table
		_reactionsTable["Water"] = waterDict;
		_reactionsTable["Uranium"] = uraniumDict;
		_reactionsTable["Sodium"] = sodiumDict;
		_reactionsTable["Nitric Acid"] = nitricAcidDict;
		_reactionsTable["Copper"] = copperDict;
		_reactionsTable["Calcium"] = calciumDict;

		return _reactionsTable;
    }

    public static Dictionary<string, string> GetReactionsFor(string substance){
	    return _reactionsTable[substance];
    }
}
