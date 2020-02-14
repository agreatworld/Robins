using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bag {
	public class BagModel : MonoBehaviour {

		#region internal data structure

		public class Card {
			public string name;
			public bool ifOwned;
			public int count;

			public Card(string name) {
				this.name = name;
				ifOwned = false;
				count = 0;
			}
		}

		public enum Tab {
			Seed,
			BirdFeed,
			Fertilizer
		};
		#endregion

		#region field

		/// <summary>
		/// 保存所有卡牌的字典
		/// </summary>
		public Dictionary<Tab, List<Card>> cardsModel = new Dictionary<Tab, List<Card>>();

		/// <summary>
		/// 选项卡列表
		/// </summary>
		public List<Tab> tabs = new List<Tab> {
			Tab.Seed,
			Tab.BirdFeed,
			Tab.Fertilizer
		};

		#endregion

		/// <summary>
		/// 单例
		/// </summary>
		public static BagModel Instance;


		private void Awake() {
			Instance = this;
			Init();
		}

		/// <summary>
		/// 数据初始化
		/// </summary>
		private void Init() {

			// 为数据分配内存空间
			for (int i = 0; i < tabs.Count; ++i) {
				cardsModel.Add(tabs[i], new List<Card>());
			}
		}

	}
}


