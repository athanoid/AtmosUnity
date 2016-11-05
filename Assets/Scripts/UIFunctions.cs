using UnityEngine;
using System.Collections;

public class UIFunctions : MonoBehaviour {

	public void exitApp(){

		Input.location.Stop();
		Application.Quit();
		Debug.Log("Exiting...");
	}

}
