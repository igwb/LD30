using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class Radar : MonoBehaviour {

	public GameObject RadarEntity, test;
	public float divisionDistance = 100f, maxDistance = 100f;

	private List<RadarInfo> entities = new List<RadarInfo>();
	private List<GameObject> drawn = new List<GameObject>();

	void Start() {

		addEntity (Camera.main.transform, Color.blue, null);
		addEntity (test.transform, Color.red, null);
	}

	void Update() {

		foreach (var item in drawn) {

			GameObject.Destroy(item);
		}

		drawn.Clear ();

		var drawList = from entity in entities
			where Vector3.Distance (entity.Position.position, Camera.main.transform.position) / divisionDistance < maxDistance
				select entity;

		foreach (var item in drawList) {

			var entity = (GameObject) GameObject.Instantiate(RadarEntity);

			entity.transform.parent = transform;
			Vector3 pos = ((item.Position.position - Camera.main.transform.position) / divisionDistance) + transform.position;
			pos.z = 100f;
			entity.transform.position = pos;
			entity.transform.localScale = Vector3.one;

			if(item.Sprite != null)
				entity.GetComponent<UnityEngine.UI.Image>().sprite = item.Sprite;

			entity.GetComponent<UnityEngine.UI.Image>().color = item.Color;

			drawn.Add(entity);
		}
	}

	public void addEntity(Transform transform, Color color, Sprite sprite) {

		entities.Add(new RadarInfo(transform, color, sprite));
	}

	public class RadarInfo {

		public Transform Position {
			get;
			set;
		}

		public Color Color {
			get;
			set;
		}

		public Sprite Sprite {
			get;
			set;
		}

		public RadarInfo(Transform pos, Color c, Sprite s) {

			Position = pos;
			Color = c;
			Sprite = s;
		}
	}
}
