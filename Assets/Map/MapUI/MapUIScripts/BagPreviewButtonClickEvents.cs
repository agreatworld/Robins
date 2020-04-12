using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagPreviewButtonClickEvents : MonoBehaviour, IPointerClickHandler, IPointerDownHandler {

	private Image image;

	private void Awake() {
		image = GetComponent<Image>();
	}
	public void OnPointerClick(PointerEventData eventData) {
		BagPreviewWindowTabs.Instance.ClickTab(image);

	}

	public void OnPointerDown(PointerEventData eventData) {

	}
}
