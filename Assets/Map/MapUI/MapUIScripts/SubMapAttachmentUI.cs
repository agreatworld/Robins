using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubMapAttachmentUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

	public static SubMapAttachmentUI Instance;

	/// <summary>
	/// 鼠标拖动面板UI
	/// </summary>
	private bool mouseDragPanel = false;

	/// <summary>
	/// 上一帧鼠标世界坐标用于计算偏移量
	/// </summary>
	private Vector2 mouseWorldPositionLastFrame;

	private void Awake() {
		Instance = this;
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

	public void UpdateInfo() {
		Debug.Log("刷新信息面板");
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
}
