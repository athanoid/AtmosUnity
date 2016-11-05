using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class RequestLocation : MonoBehaviour
{
	public static float lat;
	public static float lon;

	string APIkey = "AIzaSyB8h936q7Jl2H0GpnLfiETzcdRK3b5FPqQ";
	public static string myLoc;

	void Start()
	{
		// Turn on location services, if available
		Input.location.Start();
		fetchLocation();
		InvokeRepeating("fetchGeo", 2.0f, 1.0f); // Starting in 2 seconds, a getGeolocation request will be sent to Google every 1 second
	}
	
	void fetchLocation()
	{
		StartCoroutine(UpdateLocation());
	}

	void fetchGeo(){
		StartCoroutine(getGeolocation());
	}
	
	IEnumerator UpdateLocation()
	{
		// Do nothing if location services are not available
		if (Input.location.isEnabledByUser)
		{
			lat = Input.location.lastData.latitude;
			lon = Input.location.lastData.longitude;

			yield return null;
		}
		else{
			lat = -1f;
			lon = -1f;
		}
	}


	/*
	* Reverse geocoding request and response from Google (address lookup) 
	*/	
	IEnumerator getGeolocation(){ 
		
		//get coordinates
		float atmosLatitude = RequestLocation.lat;
		float atmosLongitude = RequestLocation.lon;
		
		//		WWW request = new WWW("https://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452&key=AIzaSyB8h936q7Jl2H0GpnLfiETzcdRK3b5FPqQ"); //test location
		WWW request = new WWW("https://maps.googleapis.com/maps/api/geocode/json?latlng="+atmosLatitude.ToString()+","+atmosLongitude.ToString()+"&key="+APIkey);
		
		yield return request;
		
		if (request.error == null || request.error == "")
		{
			var N = JSON.Parse(request.text);
			int results = N["results"].Count;
			string status = N["status"].Value;
			
			//			if(status == "ZERO_RESULTS"){
			//				print (status + " NULL!!!");
			//			} else{
			//				print (status + " DATA!!!");
			
			int addressComponents = N["results"][0]["address_components"].Count;

			if(status == "OK"){
				myLoc = (N["results"][2]["address_components"][1]["long_name"] + ", " + N["results"][2]["address_components"][2]["long_name"] + ", " + N["results"][2]["address_components"][5]["short_name"]);
			}
			else{
				myLoc = ("NO DATA");
			}
			Debug.Log(myLoc);
			//Debug.Log("addressLength: "+addressLength+" "+"address_components: "+ N["results"][addressLength]["address_components"].Value.ToString());
			
		}
		else{
			Debug.Log("WWW error: " + request.error);
		}
		
	}

	
	void OnApplicationQuit(){
		// Stop service on quit
		Input.location.Stop();
		Debug.Log("Exiting...");
	}


}
