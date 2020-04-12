using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructButtonEventsConstructGuide : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		BagPreviewWindow.Instance.gameObject.SetActive(true);
		GameGuide.Instance.guideMask.SetActive(false);
		DialogueController.Instance.ShowDialogue();
		DialogueManager.Instance.UpdateDialogueStatus();
		DialogueManager.Instance.PlayNext();
		Destroy(transform.Find("ButtonCopied(Clone)").gameObject);
		Destroy(this);
	}


}
