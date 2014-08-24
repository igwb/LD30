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

		HUD.getHUD().getTextHandler("Energy").setText(energyValue + seperator + energyMaxValue);
		HUD.getHUD().getTextHandler("Water").setText(waterValue + seperator + waterMaxValue);
	}
}
