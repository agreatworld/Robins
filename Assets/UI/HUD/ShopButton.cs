using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {

	private Button button;

	public GameObject shop;

	private void Awake() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => {
			GameGuide.Instance.isGameGuiding = false;
			shop.SetActive(true);
		});
	}
}
