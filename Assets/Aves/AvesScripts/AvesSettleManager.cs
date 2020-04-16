using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvesSettleManager : MonoBehaviour {

	public static AvesSettleManager Instance;

	private struct AvesUnit {
		public Vector2 localPosition;
		public GameObject aves;
	}

	private static int unitCount = 13;

	private AvesUnit[] avesUnits = new AvesUnit[unitCount];

	private Transform holder;

	private GameObject avesPrefab;

	[HideInInspector]
	public bool isPreSettling;
	[HideInInspector]
	public GameObject preSettleAvatar;
	[HideInInspector]
	public GameObject avesSettled;

	private void Awake() {
		Instance = this;
		holder = transform.Find("AvesSettleManager");
		avesPrefab = new GameObject();
		avesPrefab.AddComponent<RectTransform>();
		avesPrefab.AddComponent<CanvasRenderer>();
		avesPrefab.AddComponent<Image>();
		avesPrefab.transform.SetParent(transform);
		RectTransform rt = avesPrefab.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(95, 95);
		rt.localScale = Vector3.one;
		rt.gameObject.SetActive(false);
		rt.name = "AvesPrefab";
		avesPrefab.AddComponent<AvesMouseEvents>();
		avesPrefab.GetComponent<AvesMouseEvents>().enabled = true;
		Init();
	}

	private void Init() {
		for (int i = 0; i < unitCount; ++i) {
			avesUnits[i].localPosition = new Vector2(-900f + i * 150, 0);
		}
	}

	public void AddAves(string avesName) {
		// 若队列已满不加入新鸟
		if (avesUnits[unitCount - 1].aves != null) {
			return;
		}
		for (int i = 0; i < unitCount; ++i) {
			if (avesUnits[i].aves == null) {
				// 遍历一遍找到第一个空位分配给新鸟
				GameObject aves = Instantiate(avesPrefab, Vector2.zero, Quaternion.identity, holder) as GameObject;
				aves.GetComponent<RectTransform>().localPosition = avesUnits[i].localPosition;
				aves.name = avesName;
				aves.GetComponent<Image>().sprite = Resources.Load<Sprite>("Aves/AvesAvatar/" + avesName);
				aves.SetActive(true);
				avesUnits[i].aves = aves;
				break;
			}
		}
	}

	public void RemoveAvesPreSettleAvatar() {
		Destroy(preSettleAvatar);
		RefreshAvesUnits();
	}

	private void RefreshAvesUnits() {
		for (int i = 0; i < unitCount; ++i) {
			if (avesUnits[i].aves == null) {
				for (int j = i + 1; j < unitCount; ++j) {
					if (avesUnits[j].aves != null) {
						avesUnits[i].aves = avesUnits[j].aves;
						avesUnits[i].aves.transform.localPosition = avesUnits[i].localPosition;
						avesUnits[j].aves = null;
						break;
					}
				}
			}
		}
	}

	public void HighLightAllSubMaps() {
		var infoArray = MapUIController.Instance.GetInfoArray();
		foreach (var info in infoArray) {
			info.mouseEvents.HighLightSubMap();
		}
	}

	public void ResetAllSubMaps() {
		var infoArray = MapUIController.Instance.GetInfoArray();
		foreach (var info in infoArray) {
			info.mouseEvents.ResetMaterial();
		}
	}

	public void SetAvesSettled(GameObject preSettleAvatar, GameObject aves) {
		this.preSettleAvatar = preSettleAvatar;
		avesSettled = aves;
	}

	public void AvesSettleDown() {
		RemoveAvesPreSettleAvatar();
		ResetAllSubMaps();
		isPreSettling = false;
	}
}
