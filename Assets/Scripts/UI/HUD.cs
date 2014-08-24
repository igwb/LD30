using UnityEngine;
using System.Collections;

using System.Linq;

public class HUD : MonoBehaviour {
	
	public BottomPanel bottomPanel;
	public TextHandler[] textHandlers;

	private static HUD hud;
	
	void Start() {
		hud = this;
	}
	
	public static HUD getHUD() {
		return hud;
	}

	public TextHandler getTextHandler(string name) {

		foreach (var item in textHandlers) {
		
			if (item.Name == name) {
				return item;
			}

		}

		return null;
	}
}