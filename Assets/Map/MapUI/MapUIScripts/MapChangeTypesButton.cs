using UnityEngine;
using UnityEngine.EventSystems;

public class MapChangeTypesButton : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log("Click Change Types Button");
	}
}
