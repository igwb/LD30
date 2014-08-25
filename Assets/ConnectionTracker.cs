using UnityEngine;
using System.Collections;

public class ConnectionTracker : MonoBehaviour {

	public int totalPlanets;
	public int totalConnections;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(PLACER.getPLACER().GetComponentsInChildren<Transform>(false).Length == 1) {
			HUD.getHUD().getTextHandler("Tool Tip").setText("Connected " + totalConnections + " of " + totalPlanets + " worlds.");
		}
		
		if(totalPlanets == totalConnections) {
			Application.LoadLevel("Win");
		}
	}
}
