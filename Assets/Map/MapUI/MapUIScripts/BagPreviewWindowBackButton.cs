using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagPreviewWindowBackButton : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler {
	public void OnPointerClick(PointerEventData eventData) {
		BagPreviewWindow.Instance.Hide();
	}

	public void OnPointerDown(PointerEventData eventData) {
	}

	public void OnPointerUp(PointerEventData eventData) {
	}
}
