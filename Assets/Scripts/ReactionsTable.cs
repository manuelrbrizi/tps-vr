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
			["Water"] = "White Steam",
			["Sodium"] = "Fire"
		};

		// Sodium dictionaries
		var sodiumDict = new Dictionary<string, string>
		{
			["Water"] = "Boom",
			["Uranium"] = "Cold Uranium",
			["Calcium"] = "Fire"
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
		
		var sugarDict = new Dictionary<string, string>
		{
			["Colors"] = "Substance SC",
			["Flowers"] = "Substance SF"
		}; 
		
		var flowersDict = new Dictionary<string, string>
		{
			["Colors"] = "Substance FC",
			["Sugar"] = "Substance SF"
		}; 
		
		var colorsDict = new Dictionary<string, string>
		{
			["Sugar"] = "Substance SC",
			["Flowers"] = "Substance FC"
		}; 
		
		var scDict = new Dictionary<string, string>
		{
			["Flowers"] = "Magic"
		}; 
		
		var sfDict = new Dictionary<string, string>
		{
			["Colors"] = "Magic"
		};
		
		var fcDict = new Dictionary<string, string>
		{
			["Sugar"] = "Magic"
		};

		// Adding dictionaries to reaction table
		_reactionsTable["Water"] = waterDict;
		_reactionsTable["Uranium"] = uraniumDict;
		_reactionsTable["Sodium"] = sodiumDict;
		_reactionsTable["Nitric Acid"] = nitricAcidDict;
		_reactionsTable["Copper"] = copperDict;
		_reactionsTable["Calcium"] = calciumDict;
		_reactionsTable["Flowers"] = flowersDict;
		_reactionsTable["Colors"] = colorsDict;
		_reactionsTable["Sugar"] = sugarDict;
		_reactionsTable["Substance SF"] = sfDict;
		_reactionsTable["Substance SC"] = scDict;
		_reactionsTable["Substance FC"] = fcDict;

		return _reactionsTable;
    }

    public static Dictionary<string, string> GetReactionsFor(string substance){
	    return _reactionsTable[substance];
    }
}
