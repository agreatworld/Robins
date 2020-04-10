using UnityEngine;

public class BranchEventsForGuide : MonoBehaviour {
	private void OnMouseUpAsButton() {
		GameGuide.Instance.guideMask.SetActive(false);
		GetComponent<SpriteRenderer>().sortingOrder = 3;
		DialogueController.Instance.ShowDialogue();
		DialogueManager.Instance.UpdateDialogueStatus();
		Destroy(this);
	}

	

}
