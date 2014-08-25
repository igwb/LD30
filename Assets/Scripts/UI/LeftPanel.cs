using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LeftPanel : MonoBehaviour {

	public GameObject ButtonPrefab;
	public List<GameObject> Buttons = new List<GameObject>();

	public GameObject addButton(string name, string text) {

		var button = (GameObject) GameObject.Instantiate (ButtonPrefab);

		button.transform.parent = transform;

		var bh = button.GetComponent<ButtonHandler> ();
		bh.Name = name;
		button.GetComponentInChildren<Text>().text = text;

		Vector3 pos = new Vector3(75f, -5f + 10f * Buttons.Count + 30f * Buttons.Count, 0f);

		button.transform.position = Vector3.zero;
		button.transform.localPosition = pos;		
		button.transform.localScale = Vector3.one;

		HUD.getHUD ().buttonHandlers.Add (bh);
		Buttons.Add (button);
		return button;
	}
}
