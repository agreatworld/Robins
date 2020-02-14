using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bag {
	public class BagView : MonoBehaviour {

		public static BagView Instance;

		/// <summary>
		/// 卡牌字典
		/// </summary>
		public Dictionary<BagModel.Tab, List<GameObject>> cardsView = new Dictionary<BagModel.Tab, List<GameObject>>();

		/// <summary>
		/// View 的初始化依赖于 Presenter 和 Model，在 Project Setting 设置了三者运行的先后顺序
		/// </summary>
		private void Awake() {
			Instance = this;

			// 字段初始化
			foreach(var tab in BagModel.Instance.tabs) {
				GameObject content;
				BagPresenter.Instance.scrollViewContents.TryGetValue(tab.ToString(), out content);
				List<GameObject> list = new List<GameObject>();
				for (int i = 0; i < content.transform.childCount; ++i) {
					list.Add(content.transform.GetChild(i).gameObject);
				}
				cardsView.Add(tab, list);
			}

		}


	}

}

