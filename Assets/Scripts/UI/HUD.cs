using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class HUD : MonoBehaviour {
	
	public BottomPanel bottomPanel;
	public LeftPanel leftPanel;
	public TextHandler[] textHandlers;
	public List<ButtonHandler> buttonHandlers = new List<ButtonHandler>();

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

	public ButtonHandler getButtonHandler(string name) {
		
		foreach (var item in buttonHandlers) {
			
			if (item.Name == name) {
				return item;
			}
			
		}
		
		return null;
	}
}