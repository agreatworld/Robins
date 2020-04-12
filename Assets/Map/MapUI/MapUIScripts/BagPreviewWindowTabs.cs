using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPreviewWindowTabs : MonoBehaviour {

	public static BagPreviewWindowTabs Instance;

	private Image[] tabs;

	private int showingIndex;

	private Vector3 showingVector = new Vector3(0, 0, 30);

	private GameObject[] scrollViewContents = new GameObject[3];

	private void Awake() {
		Instance = this;
		tabs = GetComponentsInChildren<Image>();
		GameObject viewport = transform.parent.Find("ScrollView").Find("Viewport").gameObject;
		scrollViewContents[0] = viewport.transform.GetChild(0).gameObject;
		scrollViewContents[1] = viewport.transform.GetChild(1).gameObject;
		scrollViewContents[2] = viewport.transform.GetChild(2).gameObject;

		showingIndex = 0;
		ClickTab(showingIndex);
	}

	public void ClickTab(int index) {
		HideTab(showingIndex);
		showingIndex = index;
		tabs[index].transform.Rotate(showingVector);
		scrollViewContents[index].SetActive(true);
	}
	private void HideTab(int index) {
		tabs[index].transform.rotation = Quaternion.identity;
		scrollViewContents[index].SetActive(false);
	}

}
