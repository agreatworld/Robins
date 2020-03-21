using UnityEngine;

public class MouseEvents : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	private Material originalMaterial;

	private Material rimLightMaterial;

	private void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		originalMaterial = new Material(Shader.Find("Sprites/Default"));
		rimLightMaterial = Resources.Load<Material>("Material/RimLight");

	}


	private void OnMouseOver() {
		if (MapUIController.Instance.ShouldReactMaterial()) {
			HighLightSubMap();
		}
	}

	private void OnMouseExit() {
		if (MapUIController.Instance.ShouldReactMaterial()) {
			ResetMaterial();
		}
	}

	public void ResetMaterial() {
		spriteRenderer.material = originalMaterial;
	}

	public void HighLightSubMap() {
		spriteRenderer.material = rimLightMaterial;
	}
}
