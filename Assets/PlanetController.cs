using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {


	private bool connected;
	
	// Use this for initialization
	void Start () {
		GameObject.Find("ConnectionTracker").GetComponent<ConnectionTracker>().totalPlanets ++;
	}
	
	public void Connect() {
		
		if(!connected) {
			GameObject.Find("ConnectionTracker").GetComponent<ConnectionTracker>().totalConnections ++;
		}
		
		connected = true;
	}
}
