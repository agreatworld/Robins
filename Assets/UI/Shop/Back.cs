using UnityEngine;
using UnityEngine.EventSystems;

namespace Shop {
	public class Back : MonoBehaviour, IPointerClickHandler {

		private GameObject shop;

		private void Awake() {
			shop = transform.parent.gameObject;
		}

		public void OnPointerClick(PointerEventData eventData) {
			AudioManager.Instance.audioSource.clip = AudioManager.Instance.sceneBGM;
			shop.SetActive(false);
		}

	}

}

