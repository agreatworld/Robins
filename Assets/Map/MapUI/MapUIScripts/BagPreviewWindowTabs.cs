using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPreviewWindowTabs : MonoBehaviour {

	public static BagPreviewWindowTabs Instance;

	private Image[] tabs;

	private Image showingTab;

	private Vector3 showingVector = new Vector3(0, 0, 30);

	private void Awake() {
		Instance = this;
		tabs = GetComponentsInChildren<Image>();
		showingTab = tabs[0];
		ClickTab(tabs[0]);
	}

	public void ClickTab(Image tab) {
		HideTab(showingTab);
		showingTab = tab;
		tab.transform.Rotate(showingVector);
	}
	private void HideTab(Image tab) {
		tab.transform.rotation = Quaternion.identity;
	}

}
