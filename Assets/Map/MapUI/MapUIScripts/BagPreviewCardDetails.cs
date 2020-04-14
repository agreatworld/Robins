using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPreviewCardDetails : MonoBehaviour {

	public static BagPreviewCardDetails Instance;

	private Image image;

	private GameObject panel;

	private void Awake() {
		Instance = this;
		panel = transform.Find("CardDetails").gameObject;
		image = panel.transform.Find("Image").GetComponent<Image>();
		Hide();
	}

	public void ChangeSprite(Sprite sprite) {
		image.sprite = sprite;
	}

	public void Hide() {
		panel.SetActive(false);
	}

	public void Show() {
		MapUIController.Instance.DisableAllEvents();
		panel.SetActive(true);
	}

}
