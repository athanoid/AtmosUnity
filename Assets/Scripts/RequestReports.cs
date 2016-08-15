using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class RequestReports : MonoBehaviour {

	List<string> placesReports = new List<string>();
	List<string> placesPredictions = new List<string>();

	void Start()
	{
		fetchReports();
	}
	
	
	void fetchReports(){
		StartCoroutine(getPlacesWithReports());
		StartCoroutine(getPlacesWithPredictions());
	}

	/*
	* Get all available places with reports from Atmos 
	*/
	IEnumerator getPlacesWithReports()
	{
		WWW request = new WWW("http://derecho.inf.usi.ch/WeatherUSI/getPlacesWithReports.php");
		
		yield return request;
		
		if (request.error == null || request.error == "")
		{
			var N = JSON.Parse(request.text);
			int placesLength = N["places"].Count;
			for (int i=0; i<=placesLength; i++){
				if(N["places"][i]["place_name"].Value.ToString() != "unknown")
				//	Debug.Log(N["places"][i]["place_name"].Value.ToString());
					placesReports.Add(N["places"][i]["place_name"].Value.ToString());
			}	
				//Debug.Log("Places: "+placesReports.Count); 
		}
		else{
			Debug.Log("WWW error: " + request.error);
		}
	}
	

	/*
	* Get all available predictions with reports from Atmos 
	*/
	IEnumerator getPlacesWithPredictions()
	{
		WWW request = new WWW("http://derecho.inf.usi.ch/WeatherUSI/getPlacesWithPredictions.php");
		
		yield return request;
		
		if (request.error == null || request.error == "")
		{
			var N = JSON.Parse(request.text);
			int placesLength = N["places"].Count;
			for (int i=0; i<=placesLength; i++){
				if(N["places"][i]["place_name"].Value.ToString() != "unknown")
				//	Debug.Log(N["places"][i]["place_name"].Value.ToString());
					placesPredictions.Add(N["places"][i]["place_name"].Value.ToString());
			}	
				Debug.Log("Predictions: "+placesPredictions.Count);
		}
		else{
			Debug.Log("WWW error: " + request.error);
		}
	}




}
