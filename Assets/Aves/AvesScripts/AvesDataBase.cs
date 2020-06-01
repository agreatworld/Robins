using System.Collections.Generic;
using UnityEngine;

public class AvesDataBase : MonoBehaviour {

	public static AvesDataBase Instance {
		private set; get;
	}

	public enum Rarity {
		R, SR, SSR
	};

	private struct AvesData {
		/// <summary>
		/// 稀有度
		/// </summary>
		public Rarity rarity;
		/// <summary>
		/// 单位时间产生树枝基础数量
		/// </summary>
		public int branchesPerTenSeconds;
		/// <summary>
		/// 商店交换时所需的树枝
		/// </summary>
		public int priceAtShopInBranches;
		/// <summary>
		/// 鸟宝宝出现时间
		/// </summary>
		public int childBornTime;
		/// <summary>
		/// 逗留时间
		/// </summary>
		public int stayTime;
		/// <summary>
		/// 偏好地形
		/// </summary>
		public SubMapType preferredType;
		/// <summary>
		/// 鸟宝宝长大时间
		/// </summary>
		public int child2MatrueTime;
	}

	/// <summary>
	/// 信息字典
	/// </summary>
	private Dictionary<string, AvesData> avesDataDic = new Dictionary<string, AvesData>();

	private void Awake() {
		Instance = this;
		DataInit();

	}

	private void DataInit() {
		avesDataDic.Add("池鹭", new AvesData {
			rarity = Rarity.R,
			branchesPerTenSeconds = 8,
			priceAtShopInBranches = 150,
			childBornTime = 100,
			stayTime = 300,
			child2MatrueTime = 200,
			preferredType = SubMapType.Lake
		});
		avesDataDic.Add("山麻雀", new AvesData {
			rarity = Rarity.R,
			branchesPerTenSeconds = 1,
			priceAtShopInBranches = 10,
			childBornTime = 10,
			stayTime = 30,
			child2MatrueTime = 20,
			preferredType = SubMapType.Grassland
		});
		avesDataDic.Add("知更鸟", new AvesData {
			rarity = Rarity.SR,
			branchesPerTenSeconds = 10,
			priceAtShopInBranches = 1000,
			childBornTime = 450,
			stayTime = 900,
			child2MatrueTime = 900,
			preferredType = SubMapType.Grassland
		});
		avesDataDic.Add("褐冠鹃隼", new AvesData {
			rarity = Rarity.SR,
			branchesPerTenSeconds = 10,
			priceAtShopInBranches = 1000,
			childBornTime = 450,
			stayTime = 900,
			child2MatrueTime = 900,
			preferredType = SubMapType.Forest
		});
		avesDataDic.Add("白琵鹭", new AvesData {
			rarity = Rarity.SR,
			branchesPerTenSeconds = 35,
			priceAtShopInBranches = 3500,
			childBornTime = 900,
			stayTime = 1800,
			child2MatrueTime = 1800,
			preferredType = SubMapType.Lake
		});
		avesDataDic.Add("金雕", new AvesData {
			rarity = Rarity.SSR,
			branchesPerTenSeconds = 80,
			priceAtShopInBranches = 8000,
			childBornTime = 3600,
			stayTime = 5400,
			child2MatrueTime = 7200,
			preferredType = SubMapType.Mountain
		});
		avesDataDic.Add("东方白鹳", new AvesData {
			rarity = Rarity.SSR,
			branchesPerTenSeconds = 100,
			priceAtShopInBranches = 10000,
			childBornTime = 7200,
			stayTime = 10800,
			child2MatrueTime = 14400,
			preferredType = SubMapType.Lake
		});

	}

	public int GetBranchesPerTenSecondsByName(string name) {
		AvesData data = avesDataDic[name];
		return data.branchesPerTenSeconds;
	}

	public SubMapType GetAvesPreferredTypeByName(string name) {
		return avesDataDic[name].preferredType;
	}

	public float GetProfitRateWhenAvesMatchingTypeByName(string name) {
		Rarity r = avesDataDic[name].rarity;
		if (r == Rarity.R)
			return 0.01f;
		if (r == Rarity.SR)
			return 0.02f;
		if (r == Rarity.SSR)
			return 0.03f;
		return 0;
	}
}
