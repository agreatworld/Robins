using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMapEventsForConstructGuide : MonoBehaviour {

	private void OnMouseUpAsButton() {
		GameGuide.Instance.guideMask.SetActive(false);
		GetComponent<SpriteRenderer>().sortingOrder = 1;
		MapUIController.Instance.ClickSubMap();
		ClickConstructButtonGuide();
	}

	private void ClickConstructButtonGuide() {
		GameObject constructButton = transform.Find("Canvas").Find("MapButtonsHolder").Find("ConstructButton").gameObject;
		GameObject buttonCopied = Instantiate(Resources.Load<GameObject>("GameGuide/ButtonCopied"), constructButton.transform.position, Quaternion.identity) as GameObject;
		buttonCopied.GetComponent<SpriteRenderer>().sortingOrder = 101;
		GameGuide.Instance.guideMask.SetActive(true);
		buttonCopied.AddComponent<ConstructButtonEventsConstructGuide>();
	}

}
