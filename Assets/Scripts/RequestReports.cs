using UnityEngine;
using System.Collections;
using SimpleJSON;

public class RequestReports : MonoBehaviour {

	
	
	void Start()
	{
		StartCoroutine(SendRequest());
	}
	
	
	IEnumerator SendRequest()
	{
		WWW request = new WWW("http://derecho.inf.usi.ch/WeatherUSI/getPlacesWithReports.php");
		
		yield return request;
		
		if (request.error == null || request.error == "")
		{
			var N = JSON.Parse(request.text);
			int placesLength = N["places"].Count;
			for (int i=0; i<=placesLength; i++)
			{
				if(N["places"][i]["place_name"].Value.ToString() != "unknown")
					Debug.Log(N["places"][i]["place_name"].Value.ToString());
			}
			
		}
		else
		{
			Debug.Log("WWW error: " + request.error);
		}
		
	}
	

}
