using UnityEngine.EventSystems;
using UnityEngine;

namespace Bag {
	public class TabClickEvents : MonoBehaviour, IPointerClickHandler {

		/// <summary>
		/// 选项卡
		/// </summary>
		private BagModel.Tab tab;


		// Start is called before the first frame update
		void Awake() {
			switch (transform.name) {
				case "ImageSeedTab":
					tab = BagModel.Tab.Seed;
					break;
				case "ImageBirdFeedTab":
					tab = BagModel.Tab.BirdFeed;
					break;
				case "ImageFertilizerTab":
					tab = BagModel.Tab.Fertilizer;
					break;
			}
		}

		public void OnPointerClick(PointerEventData eventData) {
			BagPresenter.Instance.selectedTab = tab;
			BagPresenter.Instance.UpdateSelectedTab();
		}
	}
}

