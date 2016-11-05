using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleGUI : MonoBehaviour {

		Text text;

		void Start () {

		text = GetComponent <Text> ();

		}


	void Update () {

//		text.text = "Latitude: "+ Input.location.lastData.latitude + "\nLongitude: "+ Input.location.lastData.longitude + "\nLoc: " + RequestReports.myLoc;
		text.text = "Latitude: "+ RequestLocation.lat.ToString() + "\nLongitude: "+ RequestLocation.lon.ToString() + "\nLoc: " + RequestLocation.myLoc;
	}
	


}
