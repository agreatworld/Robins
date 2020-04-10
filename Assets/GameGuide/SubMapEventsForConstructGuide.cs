using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMapEventsForConstructGuide : MonoBehaviour {

	private bool clicked = false;

	private void OnMouseUpAsButton() {
		if (clicked)
			return;
		clicked = true;
		GameGuide.Instance.guideMask.SetActive(false);
		GetComponent<SpriteRenderer>().sortingOrder = 1;
		MapUIController.Instance.ClickSubMap();
		ClickConstructButtonGuide();
	}

	private void ClickConstructButtonGuide() {
		GameObject constructButton = transform.Find("Canvas").Find("MapButtonsHolder").Find("ConstructButton").gameObject;
		// 实例化一个副本只为UI显示，无任何作用
		GameObject buttonCopied = Instantiate(Resources.Load<GameObject>("GameGuide/ButtonCopied"), constructButton.transform.position, Quaternion.identity, constructButton.transform) as GameObject;
		GameGuide.Instance.guideMask.SetActive(true);
		constructButton.AddComponent<ConstructButtonEventsConstructGuide>();
	}

}
