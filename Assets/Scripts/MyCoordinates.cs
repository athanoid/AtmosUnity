using UnityEngine;
using System.Collections;

public class MyCoordinates : MonoBehaviour {

	private static float newlatitude, newlongitude;

        public float Latitude //Creating coordinates property
        {
            get { //get method for returning value
                return newlatitude;
            }
            set { // set method for storing value in newlatitude field.         
                newlatitude = value;
            }
        }
         public float Longitude 
        {
            get {
                return newlongitude;
            }
            set{
                newlongitude = value;
            }
        }

}
