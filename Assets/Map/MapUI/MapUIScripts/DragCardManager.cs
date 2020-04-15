using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCardManager : MonoBehaviour {
	public static DragCardManager Instance;

	private SpriteRenderer cover;

	private Vector3 mousePosLastFrame;


	public bool isDragging {
		private set; get;
	}

	private void Awake() {
		Instance = this;
		var sprite = new GameObject();
		sprite.AddComponent<SpriteRenderer>();
		cover = sprite.GetComponent<SpriteRenderer>();
		cover.sortingOrder = 20;
	}

	private void Update() {
		HandleDragging();
	}

	private void HandleDragging() {
		if (isDragging) {
			Vector3 mousePosThisFrame = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 offset = mousePosThisFrame - mousePosLastFrame;
			cover.transform.position += (Vector3)offset;
			mousePosLastFrame = mousePosThisFrame;
		}
	}

	public void StartDragging(Sprite sprite) {
		isDragging = true;
		cover.sprite = sprite;
		mousePosLastFrame = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		cover.gameObject.SetActive(true);
		cover.transform.position = (Vector2)mousePosLastFrame;
	}
	public void StopDragging() {
		isDragging = false;
		cover.gameObject.SetActive(false);
		MapUIController.Instance.DisableAllEvents();
	}
	public GameObject GetCard() {
		var card = Instantiate(cover.gameObject) as GameObject;
		return card;
	}

	public int GetCardRank() {
		Debug.Log("卡牌等级加载，暂默认为1");
		return 1;
	}
}
