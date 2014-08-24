using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextHandler : MonoBehaviour {

	public string Name = "TextHandler";

	private Text textObject;

	void Start() {

		textObject = GetComponent<Text>();
	}

	public void setText(string text) {

		textObject.text = text;
	}

	public void setColor(Color color) {

		textObject.color = color;
	}
}
