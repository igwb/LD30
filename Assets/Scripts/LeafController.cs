using UnityEngine;
using System.Collections;

public class LeafController : MonoBehaviour {

	public float powerProduction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		BottomPanel bottomPanel = HUD.getHUD().bottomPanel;
		
		bottomPanel.energyValue = Mathf.Min(bottomPanel.energyValue + powerProduction * Time.deltaTime, bottomPanel.energyMaxValue);
	}
}
