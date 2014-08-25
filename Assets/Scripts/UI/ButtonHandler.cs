using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	public string Name = "ButtonHandler";

	public delegate void onButtonPress();
	public event onButtonPress ButtonPressEvent;

	void Start() {
		ButtonPressEvent += delegate() {};
	}

	public void FireEvent() {
		ButtonPressEvent.Invoke();
	}
}
