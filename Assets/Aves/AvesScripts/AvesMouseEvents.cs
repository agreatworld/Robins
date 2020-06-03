using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class AvesMouseEvents : MonoBehaviour, IPointerClickHandler {

	[HideInInspector]
	public GameObject avesShowAtMap;

	private void Awake() {
		avesShowAtMap = new GameObject();
		avesShowAtMap.name = transform.name;
		avesShowAtMap.AddComponent<Aves>();
		avesShowAtMap.SetActive(false);



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
		AudioSource.PlayClipAtPoint(AudioManager.Instance.clickAvesAvatar, Vector3.zero);
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
