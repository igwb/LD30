using UnityEngine;
using System.Collections;

public class BottomPanel : MonoBehaviour {
	
	public float energyMaxValue = 1000.0f, energyValue = 1000.0f;
	public float waterMaxValue = 1000.0f, waterValue = 1000.0f;
	public string seperator = "/";

	public ProgressBar energyBar, waterBar;
	public Radar radar;

	void Update() {

		energyBar.setProgress(energyValue, energyMaxValue);
		waterBar.setProgress(waterValue, waterMaxValue);
		var count = 0;
		foreach(var text in GetComponentsInChildren<UnityEngine.UI.Text>()) {

			if(count == 2)
				text.text = energyValue + seperator + energyMaxValue;
			else if(count == 3)
				text.text = waterValue.ToString().Split('.')[0].PadLeft(waterMaxValue.ToString().Split('.')[0].Length, '0') + seperator + waterMaxValue;

			count++;
		}
	}
}
