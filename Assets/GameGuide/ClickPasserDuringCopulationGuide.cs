using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickPasserDuringCopulationGuide : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		GameGuide.Instance.guideMask.SetActive(false);
		AvesSettleManager.Instance.GetComponent<Canvas>().sortingOrder = 0;
		DialogueController.Instance.HideDialogue();
		Destroy(this);
	}
}
