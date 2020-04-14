using UnityEngine;

public class MapUIController : MonoBehaviour {

	public struct SubMapInfo {
		public GameObject canvas;
		public MapUIAnimation animation;
		public MouseEvents mouseEvents;
		public int index;
	}

	public static MapUIController Instance;

	private SubMapInfo[] infoArray;

	[HideInInspector]
	public int showingIndex = -1;

	private int subMapCount;

	[HideInInspector]
	public bool mouseEventsEnabled = true;

	private void Awake() {
		Instance = this;
		Transform subMaps = transform.Find("SubMaps");
		infoArray = new SubMapInfo[subMaps.childCount];
		subMapCount = subMaps.childCount;
		for (int i = 0; i < infoArray.Length; ++i) {
			Transform subMap = subMaps.GetChild(i);
			infoArray[i].mouseEvents = subMap.GetComponent<MouseEvents>();
			infoArray[i].index = i;
			infoArray[i].canvas = subMap.Find("Canvas").gameObject;
			infoArray[i].animation = infoArray[i].canvas.transform.Find("MapButtonsHolder").GetComponent<MapUIAnimation>();
			infoArray[i].canvas.SetActive(false);
		}
		gameObject.SetActive(false);
	}

	private void Update() {
		if (!mouseEventsEnabled)
			return;
		if (Input.GetMouseButtonUp(0)) {
			ClickSubMap();
		}

	}

	public void ClickSubMap() {
		RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hitInfo) {
			string hitName = hitInfo.collider.name;
			int hitIndex = -1;
			if (int.TryParse(hitName, out hitIndex)) {
				if (showingIndex == -1) {
					// -1表示当前没有显示的按钮
					int index = -1;
					if (int.TryParse(hitName, out index)) {
						if (index > -1 && index < subMapCount) {
							ShowAll(index);
							return;
						}
					}
				}
				if (hitIndex != showingIndex) {
					HideAll(showingIndex);
				} else {
					ShowButtons();
				}
			}
		} else if (showingIndex != -1) {
			if (infoArray[showingIndex].animation.mouseInsideRootButton) {
				HideButtons(showingIndex);
			} else {
				HideAll(showingIndex);
			}
		}
	}

	public void ShowButtons() {
		if (infoArray[showingIndex].animation.mouseInsideRootButton) {
			return;
		}
		infoArray[showingIndex].animation.rootShowing = true;
		infoArray[showingIndex].animation.buttonsShowing = true;
		infoArray[showingIndex].animation.ShowButtons();
	}

	public void ShowAll(int index) {
		showingIndex = index;
		infoArray[index].canvas.SetActive(true);
		infoArray[index].animation.rootShowing = true;
		infoArray[index].animation.buttonsShowing = true;
		infoArray[index].animation.ShowAll();
		ShowSubMapInfoUI();
	}

	public void ShowSubMapInfoUI() {
		// 显示子地图信息面板
		SubMapAttachmentUI.Instance.gameObject.SetActive(true);
		SubMapAttachmentUI.Instance.UpdateInfo();
	}

	public void HideButtons(int index) {
		if (index == -1)
			return;
		infoArray[index].animation.rootShowing = true;
		infoArray[index].animation.buttonsShowing = false;
		infoArray[index].animation.HideButtons();
	}

	public void HideAll(int index) {
		if (index == -1) {
			return;
		}
		infoArray[index].animation.rootShowing = false;
		infoArray[index].animation.buttonsShowing = false;
		infoArray[index].animation.HideAll();
		Invoke("FalseCanvas", 0.5f);
		infoArray[index].mouseEvents.ResetMaterial();
		HideSubMapInfoUI();
		HideBagPreviewWindow();
	}

	public void HideBagPreviewWindow() {
		BagPreviewWindow.Instance.gameObject.SetActive(false);
	}

	public void HideSubMapInfoUI() {
		// 隐藏子地图信息面板
		SubMapAttachmentUI.Instance.gameObject.SetActive(false);
	}

	/// <summary>
	/// 为了Invoke延迟调用抽取出一个单独的方法
	/// </summary>
	private void FalseCanvas() {
		if (showingIndex == -1)
			return;
		infoArray[showingIndex].canvas.SetActive(false);
		showingIndex = -1;
	}


	public bool ShouldReactMaterial() {
		if (showingIndex == -1) {
			return true;
		}
		if (infoArray[showingIndex].animation.rootShowing) {
			return false;
		} else {
			return true;
		}
	}

	public void DisableShowingSubMapEvent() {
		if (showingIndex == -1)
			return;
		infoArray[showingIndex].animation.mouseInsideRootButton = true;
	}

	public void EnableShowingSubMapEvent() {
		if (showingIndex == -1)
			return;
		infoArray[showingIndex].animation.mouseInsideRootButton = false;
	}

	public void DisableAllEvents() {
		mouseEventsEnabled = false;
	}

	public void EnableAllEvents() {
		mouseEventsEnabled = true;
	}

	public SubMapInfo[] GetInfoArray() {
		return infoArray;
	}

}
