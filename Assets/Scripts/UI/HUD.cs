using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public BottomPanel bottomPanel;
	private static HUD hud;
	
	void Start() {
		hud = this;
	}
	
	void Update() {
		
		
	}
	
	public static HUD getHUD() {
		return hud;
	}
}