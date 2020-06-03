using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGoodsDispatcher : MonoBehaviour {

	public static ShopGoodsDispatcher Instance {
		private set; get;
	}

	/// <summary>
	/// 货物
	/// </summary>
	public GameObject[] goods;

	/// <summary>
	/// 商店货物栏
	/// </summary>
	public GameObject[] goodFrames;

	private void Awake() {
		Instance = this;
	}


	public void UpdateShop() {
		for (int i = 0; i < 3; ++i) {
			Destroy(goodFrames[i].transform.GetChild(0).gameObject);
			int index = Random.Range(0, goods.Length);
			GameObject good = Instantiate(goods[index], goodFrames[i].transform);
		}
	}
}
