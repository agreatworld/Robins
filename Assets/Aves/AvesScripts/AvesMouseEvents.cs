using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class AvesMouseEvents : MonoBehaviour, IPointerClickHandler {

	[HideInInspector]
	public GameObject avesShowAtMap;

	private void Awake() {
		var resource = Resources.Load<Sprite>("Aves/AvesShowAtMap/" + transform.name);
		if (resource) {
			avesShowAtMap = new GameObject();
			avesShowAtMap.transform.localScale = Vector2.one * 0.2f;
			avesShowAtMap.AddComponent<SpriteRenderer>();
			SpriteRenderer sr = avesShowAtMap.GetComponent<SpriteRenderer>();
			sr.sprite = resource;
			sr.sortingOrder = 4;
			avesShowAtMap.name = transform.name;
			avesShowAtMap.AddComponent<Aves>();
			avesShowAtMap.SetActive(false);
			
		} else {
			Debug.LogError("加载资源出错，请检查路径");
		}

	}

	private void Update() {
		if (AvesSettleManager.Instance.isPreSettling) {
			// 右键取消
			if (Input.GetMouseButtonDown(1)) {
				ResetAves();
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (!AvesSettleManager.Instance.isPreSettling) {
			PreSettle();
		}
	}

	private void PreSettle() {
		AvesSettleManager.Instance.isPreSettling = true;
		AvesSettleManager.Instance.HighLightAllSubMaps();
		AvesSettleManager.Instance.SetAvesSettled(gameObject, avesShowAtMap);
		//transform.DOShakeScale(0.6f, 0.25f, 2, 20).SetLoops(-1, LoopType.Yoyo);
	}

	private void ResetAves() {
		AvesSettleManager.Instance.isPreSettling = false;
		AvesSettleManager.Instance.ResetAllSubMaps();
		AvesSettleManager.Instance.SetAvesSettled(null, null);
	}

}
