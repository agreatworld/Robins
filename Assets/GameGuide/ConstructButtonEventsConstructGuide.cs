using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructButtonEventsConstructGuide : MonoBehaviour {


	private void OnMouseUpAsButton() {
		GameGuide.Instance.guideMask.SetActive(false);

		Destroy(gameObject);
	}

}
