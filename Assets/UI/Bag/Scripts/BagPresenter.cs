using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bag {
	public class BagPresenter {

		public static BagPresenter Instance = new BagPresenter();

		#region field

		/// <summary>
		/// 当前被选中的选项卡，默认选中第一张
		/// </summary>
		public BagModel.Tab selectedTab = BagModel.Instance.tabs[0];

		/// <summary>
		/// 是否只展示已拥有的卡牌
		/// </summary>
		public bool onlyDisplayOwnedCards;

		#endregion

		private BagPresenter() {

		}

		
	}

}

