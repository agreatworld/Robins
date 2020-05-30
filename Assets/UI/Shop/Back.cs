using UnityEngine;
using UnityEngine.EventSystems;

namespace Shop {
	public class Back : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler {

		private GameObject shop;

		private void Awake() {
			shop = transform.parent.gameObject;
		}

		public void OnPointerClick(PointerEventData eventData) {
			shop.SetActive(false);
		}

		public void OnPointerEnter(PointerEventData eventData) {
			Debug.Log(1);
		}
	}

}

