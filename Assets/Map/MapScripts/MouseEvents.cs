using SpriteGlow;
using TMPro;
using UnityEngine;

public class MouseEvents : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	private Material originalMaterial;

	private Material rimLightMaterial;
	
	private SpriteGlowEffect spriteGlow;

	private void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		originalMaterial = new Material(Shader.Find("Sprites/Default"));
		rimLightMaterial = Resources.Load<Material>("Material/RimLight");
		spriteGlow = GetComponent<SpriteGlowEffect>();
	}

	private void OnMouseOver() {
		if (!MapUIController.Instance.mouseEventsEnabled)
			return;
		if (MapUIController.Instance.ShouldReactMaterial()) {
			HighLightSubMap();
		}
	}

	private void OnMouseExit() {
		if (!MapUIController.Instance.mouseEventsEnabled)
			return;
		if (MapUIController.Instance.ShouldReactMaterial()) {
			ResetMaterial();
		}
	}

	public void ResetMaterial() {
		//spriteRenderer.material = originalMaterial;
		spriteGlow.enabled = false;
	}

	public void HighLightSubMap() {
		//spriteRenderer.material = rimLightMaterial;
		spriteGlow.enabled = true;
	}
}

