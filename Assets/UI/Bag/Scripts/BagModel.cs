using System.Collections;
using System.Collections.Generic;

namespace Bag {
	public class BagModel {

		#region internal data structure

		public class Card {
			public int id;
			public string name;
			public bool ifOwned;
			public int count;
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
		public Dictionary<Tab, List<Card>> allCardsDictionary = new Dictionary<Tab, List<Card>>();

		/// <summary>
		/// 保存已拥有卡牌的字典
		/// </summary>
		public Dictionary<Tab, List<Card>> ownedCardsDictionary = new Dictionary<Tab, List<Card>>();

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
		public static BagModel Instance = new BagModel();

		/// <summary>
		/// 私有构造器
		/// </summary>
		private BagModel() {
			Init();

		}

		/// <summary>
		/// 数据初始化
		/// </summary>
		private void Init() {

			// 为数据分配内存空间
			for (int i = 0; i < tabs.Count; ++i) {
				allCardsDictionary.Add(tabs[i], new List<Card>());
				ownedCardsDictionary.Add(tabs[i], new List<Card>());
			}

		}

		/// <summary>
		/// 刷新已拥有卡牌字典
		/// </summary>
		public void UpdateOwnedCardsDictionary() {
			for (int i = 0; i < tabs.Count; ++i) {
				
				// 获取选项卡对应的卡牌列表
				List<Card> allCards;
				allCardsDictionary.TryGetValue(tabs[i], out allCards);

				List<Card> ownedCards;
				ownedCardsDictionary.TryGetValue(tabs[i], out ownedCards);
				ownedCards.Clear();
				
				// 遍历卡牌列表，更新已拥有卡牌字典数据
				for (int j = 0; j < allCards.Count; ++j) {
					Card card = allCards[j];
					if (card.ifOwned) {
						ownedCards.Add(card);
					}
				}

			}
		}

	}
}


