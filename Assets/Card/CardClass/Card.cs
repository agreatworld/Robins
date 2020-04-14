using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour, IPointerClickHandler, IPointerDownHandler {
	protected int cardCount = 1;
	protected Text countText;
	protected Sprite cardDetails;
	/// <summary>
	/// 卡牌的立绘 full-body standing drawing of a character
	/// </summary>
	protected Sprite cardFBSD;
	
	/// <summary>
	/// 预拖拽
	/// 拖拽距离超过阈值认定为拖拽卡牌操作
	/// </summary>
	private bool preDragging;

	private float preDraggingThreshold = 0.4f;

	private Vector3 startDraggingPos;

	protected virtual void Awake() {
		countText = transform.Find("CountText").GetComponent<Text>();
		cardDetails = Resources.Load<Sprite>("MapUI/Card/" + transform.name);
		cardFBSD = Resources.Load<Sprite>("MapUI/CardFBSD/" + transform.name);
	}

	protected virtual void Update() {
		CheckCardCount();
		HandleCardDragging();
		HandleMouseUp();
	}

	public void OnPointerClick(PointerEventData eventData) {
		BagPreviewCardDetails.Instance.ChangeSprite(cardDetails);
		BagPreviewCardDetails.Instance.Show();
	}

	public void OnPointerDown(PointerEventData eventData) {
		preDragging = true;
		startDraggingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
	}



	private void HandleCardDragging() {
		if (preDragging) {
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Vector2.Distance(mousePosition, startDraggingPos) > preDraggingThreshold) {
				DragCardManager.Instance.StartDragging(cardFBSD);
				preDragging = false;
			}
		}
	}

	private void HandleMouseUp() {
		if (DragCardManager.Instance.isDragging) {
			if (Input.GetMouseButtonUp(0)) {
				DragCardManager.Instance.StopDragging();
			}
		}
	}

	private void CheckCardCount() {
		if (cardCount == 0) {
			gameObject.SetActive(false);
			return;
		}
		if (cardCount == 1) {
			countText.gameObject.SetActive(false);
			return;
		}
		countText.text = cardCount.ToString();
	}

}
