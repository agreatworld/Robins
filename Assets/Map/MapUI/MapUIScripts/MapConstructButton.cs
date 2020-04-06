using UnityEngine;
using UnityEngine.EventSystems;

public class MapConstructButton : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log("Click Construct Button");
	}
}
