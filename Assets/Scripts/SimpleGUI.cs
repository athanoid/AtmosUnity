using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleGUI : MonoBehaviour {

		public float atmosLatitude;
		public float atmosLongitude;

		Text text;

		MyCoordinates atmos;

		void Start () {

		MyCoordinates atmos = new MyCoordinates();
		atmosLatitude = atmos.Latitude;
		atmosLongitude = atmos.Longitude;

		text = GetComponent <Text> ();


		}
	// Use this for initialization
	void Update () {

	text.text = "Latitude: "+ atmosLatitude + "\nLongitude: "+ atmosLongitude;

	}
	

}
