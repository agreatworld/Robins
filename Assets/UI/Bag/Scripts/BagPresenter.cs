using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bag {
	public class BagPresenter : MonoBehaviour {

		public static BagPresenter Instance;

		#region field

		/// <summary>
		/// 当前被选中的选项卡
		/// </summary>
		public BagModel.Tab selectedTab;

		/// <summary>
		/// 是否只展示已拥有的卡牌
		/// </summary>
		public bool onlyDisplayOwnedCards;

		/// <summary>
		/// 所有选项卡对应的 Content
		/// </summary>
		public Dictionary<string, GameObject> scrollViewContents = new Dictionary<string, GameObject>();

		/// <summary>
		/// 所有选项卡的 Image
		/// </summary>
		public Dictionary<string, GameObject> tabsView = new Dictionary<string, GameObject>();

		#endregion

		private void Awake() {
			Instance = this;
			selectedTab = BagModel.Instance.tabs[0]; // 默认选中第一张

			// 字段初始化
			Transform viewPort = transform.Find("ScrollView").Find("Viewport").transform;
			Transform panelTabs = transform.Find("PanelTabs");
			foreach (var tab in BagModel.Instance.tabs) {
				string tabName = tab.ToString();
				GameObject content = viewPort.Find("Content" + tabName).gameObject;
				scrollViewContents.Add(tabName, content);
				GameObject tabView = panelTabs.Find("Image" + tabName + "Tab").gameObject;
				tabsView.Add(tabName, tabView);
			}

			// 默认显示第一张选项卡，其余隐藏
			UpdateSelectedTab();


		}

		private void Start() {
			// Model 和 View 数据同步
			foreach(var tab in BagModel.Instance.tabs) {
				List<GameObject> cardsView;
				BagView.Instance.cardsView.TryGetValue(tab, out cardsView);
				List<BagModel.Card> cards;
				BagModel.Instance.cardsModel.TryGetValue(tab, out cards);
				foreach(var cardView in cardsView) {
					BagModel.Card card = new BagModel.Card(cardView.name);
					cards.Add(card);
				}
			}
		}

		/// <summary>
		/// 刷新选项卡状态
		/// </summary>
		public void UpdateSelectedTab() {
			string selectedTabName = selectedTab.ToString();
			foreach(var tab in BagModel.Instance.tabs) {
				// 获取数据
				string tabName = tab.ToString();
				GameObject content;
				scrollViewContents.TryGetValue(tabName, out content);
				GameObject tabView;
				tabsView.TryGetValue(tabName, out tabView);

				// View 层作出相应改变
				if (tabName.Equals(selectedTabName)) {
					content.SetActive(true);
					UpdateSelctedTabView(tabView, true);
				} else {
					content.SetActive(false);
					UpdateSelctedTabView(tabView, false);
				}
			}
		}

		/// <summary>
		/// 在 View 层刷新选项卡的状态
		/// </summary>
		private void UpdateSelctedTabView(GameObject tab, bool isSelected) {
			RectTransform rectTransform = tab.GetComponent<RectTransform>();
			if (isSelected) {
				rectTransform.transform.localScale = Vector3.one * 0.8f;
			} else {
				rectTransform.transform.localScale = Vector3.one;
			}
		}


	}

}

