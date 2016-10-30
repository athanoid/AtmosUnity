using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class RequestReports : MonoBehaviour {

	List<string> placesReports = new List<string>();
	List<string> placesPredictions = new List<string>();

	string APIkey = "AIzaSyB8h936q7Jl2H0GpnLfiETzcdRK3b5FPqQ";
	public static string myLoc;

	void Start()
	{
		fetchReports();
	}
	
	
	void fetchReports(){
		StartCoroutine(getPlacesWithReports());
		StartCoroutine(getPlacesWithPredictions());
		StartCoroutine(getGeolocation());
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


	/*
	* Reverse geocoding request and response (address lookup) 
	*/	
	IEnumerator getGeolocation(){ 

		//get coordinates
		float atmosLatitude = Input.location.lastData.latitude;
		float atmosLongitude = Input.location.lastData.longitude;

//		WWW request = new WWW("https://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452&key=AIzaSyB8h936q7Jl2H0GpnLfiETzcdRK3b5FPqQ"); //test location
		WWW request = new WWW("https://maps.googleapis.com/maps/api/geocode/json?latlng="+atmosLatitude.ToString()+","+atmosLongitude.ToString()+"&key="+APIkey);
		
		yield return request;
		
		if (request.error == null || request.error == "")
		{
			var N = JSON.Parse(request.text);
			int results = N["results"].Count;
			int addressComponents = N["results"][0]["address_components"].Count;

			Debug.Log(N["results"][addressComponents-1]["address_components"][0]["long_name"]);
			myLoc = N["results"][addressComponents-1]["address_components"][0]["long_name"];
			//Debug.Log("addressLength: "+addressLength+" "+"address_components: "+ N["results"][addressLength]["address_components"].Value.ToString());
		}
		else{
			Debug.Log("WWW error: " + request.error);
		}

      }

}
