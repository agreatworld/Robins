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
			if (!AvesSettleManager.Instance.isPreSettling) {
				ResetMaterial();
			}
		}
	}


	public void ResetMaterial() {
		spriteRenderer.material = defaultMaterial;
		spriteGlow.enabled = false;
	}

	public void HighLightSubMap() {
		//spriteRenderer.material = rimLightMaterial;
		if (!spriteGlow.enabled) {
			spriteGlow.enabled = true;
		}
	}

	private void OnMouseUpAsButton() {
		if (GameGuide.Instance.isGameGuiding && GameGuide.Instance.firstPasser) {
			HandleGameGuide();
		}
		HandleAvesSettleDown();
	}

	private void HandleAvesSettleDown() {
		if (AvesSettleManager.Instance.isPreSettling) {
			if (subMapManager.AddNewAves(AvesSettleManager.Instance.avesSettled)) {
				// 鸟类定居成功
				AvesSettleManager.Instance.AvesSettleDown();
			}
		}
	}

	private void HandleGameGuide() {
		if (AvesSettleManager.Instance.isPreSettling) {
			GameGuide.Instance.firstPasser = false;
			var manager = GetComponent<SubMapManager>();
			manager.AddBranchEventsForGuide();
			gameObject.AddComponent<CollectBranchesGuide>();
			DialogueManager.Instance.UpdateDialogueStatus();
			DialogueManager.Instance.PlayNext();
		}
	}
}

