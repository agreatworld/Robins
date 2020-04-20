﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class AvesMouseEvents : MonoBehaviour, IPointerClickHandler {

	[HideInInspector]
	public GameObject avesShowAtMap;

	private void Awake() {
		avesShowAtMap = Resources.Load<GameObject>("Aves/AvesShowAtMap/" + transform.name);
		if (avesShowAtMap) {
			avesShowAtMap = Instantiate(avesShowAtMap) as GameObject;
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
