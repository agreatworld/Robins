using UnityEngine;
using UnityEngine.EventSystems;

public class MapSeedButton : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log("播种");
	}
}
