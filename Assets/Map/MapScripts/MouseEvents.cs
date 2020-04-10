using SpriteGlow;
using TMPro;
using UnityEditor.SceneManagement;
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
		ResetMaterial();
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
		spriteRenderer.material = originalMaterial;
		spriteGlow.enabled = false;
	}

	public void HighLightSubMap() {
		Debug.Log(MapUIController.Instance.mouseEventsEnabled);
		//spriteRenderer.material = rimLightMaterial;
		spriteGlow.enabled = true;
	}

	private void OnMouseUpAsButton() {
		if (GameGuide.Instance.isGameGuiding && PasserSettleGuide.Instance.getReadyForSettleDown) {
			PasserSettleGuide.Instance.ResetAllSubMaps();
			var manager = GetComponent<SubMapManager>();
			manager.AddNewAves(PasserSettleGuide.Instance.gameObject);
			manager.AddBranchEventsForGuide();
			gameObject.AddComponent<CollectBranchesGuide>();
			PasserSettleGuide.Instance.getReadyForSettleDown = false;
			DialogueManager.Instance.UpdateDialogueStatus();
			DialogueManager.Instance.PlayNext();
		}
	}
}

