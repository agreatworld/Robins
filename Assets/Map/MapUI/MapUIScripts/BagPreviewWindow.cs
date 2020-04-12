using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPreviewWindow : MonoBehaviour {

	public static BagPreviewWindow Instance;

	private void Awake() {
		Instance = this;
	}

	public void ShowConstructPanel() {
		BagPreviewWindowTabs.Instance.ClickTab(0);
	}
}
