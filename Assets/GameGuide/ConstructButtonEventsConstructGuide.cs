using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructButtonEventsConstructGuide : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		GameGuide.Instance.guideMask.SetActive(false);
		Destroy(transform.Find("ButtonCopied(Clone)").gameObject);
		Destroy(this);
	}


}
