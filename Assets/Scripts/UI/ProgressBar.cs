using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	private float fill = 0.5f;

	public void setProgress(float value, float max) {

		fill = Mathf.Min(1, Mathf.Max(0.1f, value) / max);

		if (fill < 0.001f)
			fill = 0.001f;
	}

	void Update () {
		GetComponent<Image>().fillAmount = fill;
	}
}
