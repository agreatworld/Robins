using UnityEngine;
using UnityEngine.EventSystems;

public class MapButtonsHolderEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public void OnPointerEnter(PointerEventData eventData) {
		MapUIController.Instance.DisableShowingSubMapEvent();
	}

	public void OnPointerExit(PointerEventData eventData) {
		MapUIController.Instance.EnableShowingSubMapEvent();
	}


}
