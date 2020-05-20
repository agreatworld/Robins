
using UnityEngine;
using UnityEngine.EventSystems;

public class TranslateAvesButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler {


	public void OnPointerClick(PointerEventData eventData) {
		AvesTransitionManager.Instance.SetSource(SubMapAttachmentUI.Instance.currentSubMapManager);
		MapUIController.Instance.HighLightAllSubMaps();
	}

	public void OnPointerDown(PointerEventData eventData) {

	}
}
