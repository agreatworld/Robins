using UnityEngine;

public class MouseEvents : MonoBehaviour {

	private Material material;

	private Material originalMaterial;

	private Material rimLightMaterial;

	private void Awake() {
		material = GetComponent<SpriteRenderer>().material;
		originalMaterial = new Material(Shader.Find("Sprites/Default"));
		rimLightMaterial = Resources.Load<Material>("Material/OutLight");
		//rimLightMaterial = Resources.Load<Material>("Material/RimLight");
	}

	private void OnMouseEnter() {
		GetComponent<SpriteRenderer>().material = rimLightMaterial;
	}

	private void OnMouseExit() {
		GetComponent<SpriteRenderer>().material = originalMaterial;
	}

}
