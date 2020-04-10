using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMapEventsForConstructGuide : MonoBehaviour {

	private void OnMouseUpAsButton() {
		GameGuide.Instance.guideMask.SetActive(false);
		GetComponent<SpriteRenderer>().sortingOrder = 1;
		MapUIController.Instance.ClickSubMap();
		DialogueController.Instance.ShowDialogue();
		DialogueManager.Instance.UpdateDialogueStatus();
		DialogueManager.Instance.PlayNext();
		Destroy(this);
	}

}
