using UnityEngine;
using UnityEngine.EventSystems;

public class BirdAvatarButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler {

	/// <summary>
	/// 需要在面板赋值
	/// </summary>
	public GameObject avesInfoPanel;



	public void OnPointerClick(PointerEventData eventData) {
		avesInfoPanel.SetActive(true);
	}

	public void OnPointerDown(PointerEventData eventData) {

	}
}
