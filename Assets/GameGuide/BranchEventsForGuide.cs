using UnityEngine;

public class BranchEventsForGuide : MonoBehaviour {
	private void OnMouseUpAsButton() {
		GameGuide.Instance.guideMask.SetActive(false);
		DialogueController.Instance.ShowDialogue();
		DialogueManager.Instance.UpdateDialogueStatus();
		Destroy(this);
	}
}
