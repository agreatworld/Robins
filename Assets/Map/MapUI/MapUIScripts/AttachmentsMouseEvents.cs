using UnityEngine;
using UnityEngine.EventSystems;

public class AttachmentsMouseEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {

	/// <summary>
	/// 鼠标拖动面板UI
	/// </summary>
	private bool mouseDragPanel = false;

	/// <summary>
	/// 上一帧鼠标世界坐标用于计算偏移量
	/// </summary>
	private Vector2 mouseWorldPositionLastFrame;

	private void Start() {
		gameObject.SetActive(false);
	}

	private void Update() {
		HandleDragPanel();
	}

	private void HandleDragPanel() {
		if (mouseDragPanel) {
			Vector2 mouseWorldPositionThisFrame = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 offset = mouseWorldPositionThisFrame - mouseWorldPositionLastFrame;
			mouseWorldPositionLastFrame = mouseWorldPositionThisFrame;
			transform.position += (Vector3)offset;
		}
	}

	public void OnPointerEnter(PointerEventData eventData) {
		// 鼠标移入面板时屏蔽鼠标对地图的事件
		MapUIController.Instance.DisableAllEvents();
	}

	public void OnPointerExit(PointerEventData eventData) {
		// 鼠标移出后启用鼠标对地图的事件
		MapUIController.Instance.EnableAllEvents();
	}

	public void OnPointerDown(PointerEventData eventData) {
		mouseDragPanel = true;
		// 初始化鼠标上一帧的坐标来平滑过渡
		mouseWorldPositionLastFrame = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	public void OnPointerUp(PointerEventData eventData) {
		mouseDragPanel = false;
	}

	public void OnPointerClick(PointerEventData eventData) {
		transform.SetAsLastSibling();
	}

}
