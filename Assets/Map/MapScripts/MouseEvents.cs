using SpriteGlow;
using UnityEngine;

public class MouseEvents : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	private Material defaultMaterial;

	private Material rimLightMaterial;
	
	private SpriteGlowEffect spriteGlow;

	private SubMapManager subMapManager;

	private void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultMaterial = new Material(Shader.Find("Sprites/Default"));
		rimLightMaterial = Resources.Load<Material>("Material/RimLight");
		spriteGlow = GetComponent<SpriteGlowEffect>();
		ResetMaterial();
		subMapManager = GetComponent<SubMapManager>();
	}


	private void HandleDragging() {
		if (DragCardManager.Instance.isDragging && Input.GetMouseButtonUp(0) && MapUIController.Instance.showingIndex.ToString() == transform.name) {
			subMapManager.Construct(DragCardManager.Instance.GetCard(), DragCardManager.Instance.GetCardRank());
		}
	}

	private void OnMouseOver() {
		HandleDragging();
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
		spriteRenderer.material = defaultMaterial;
		spriteGlow.enabled = false;
	}

	public void HighLightSubMap() {
		//spriteRenderer.material = rimLightMaterial;
		spriteGlow.enabled = true;
	}

	private void OnMouseUpAsButton() {
		if (GameGuide.Instance.isGameGuiding && PasserSettleGuide.Instance && PasserSettleGuide.Instance.getReadyForSettleDown) {
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

