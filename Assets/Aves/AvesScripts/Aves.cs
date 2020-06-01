using UnityEngine;

public class Aves : MonoBehaviour {

	/// <summary>
	/// 每一个实例赋予一个唯一标识，用来标注小鸟的父母以支持交配环节的逻辑处理
	/// </summary>
	public static int Index {
		private set; get;
	}

	/// <summary>
	/// 性别
	/// </summary>
	public bool isMale {
		private set; get;
	}

	/// <summary>
	/// 鸟类是否成熟，默认为成熟的，通过交配孵出的小鸟是不成熟的，但是随着时间推移会成熟
	/// </summary>
	public bool isMature {
		get; set;
	} = true;

	/// <summary>
	/// 是否由其他鸟养育而来
	/// </summary>
	public bool isFromCopulation {
		private set; get;
	} = false;

	/// <summary>
	/// 父母的标识号，若非养育型鸟类则记为0，标识符从1开始
	/// </summary>
	public int[] parentsIndex = new int[2];

	/// <summary>
	/// 标识符
	/// </summary>
	public int ID {
		private set; get;
	}

	/// <summary>
	/// 鸟出现时触发的剧情（-1表示无）
	/// </summary>
	private int plotIndex = -1;

	/// <summary>
	/// 所在子地图
	/// </summary>
	private SubMapManager subMapManager = null;

	private float manipulateBranchesTimer = 0;

	private float manipulateBranchesTime = 10;

	private void Awake() {
		ID = ++Index;
		var nameDetails = transform.name.Split('-');
		isMale = nameDetails[1] == "雄";
		if (nameDetails[1] == "雄") {
			isMale = true;
		} else if (nameDetails[1] == "雌") {
			isMale = false;
		} else {
			transform.name = nameDetails[0] + '-' + "小";
			InitForAvesBaby();
		}
		plotIndex = AvesPlotTriggerHandler.Instance.CheckAvesAndUpdateInfo(nameDetails[0], isMale, !isMale);
	}

	private void Update() {
		HandleBranch();
	}

	private void HandleBranch() {
		manipulateBranchesTimer += Time.deltaTime;
		if (manipulateBranchesTimer < manipulateBranchesTime)
			return;
		manipulateBranchesTimer = 0;
		if (subMapManager == null) {
			subMapManager = transform.parent.GetComponent<SubMapManager>();
		}
		string name = this.name.Split('-')[0];
		int count = AvesDataBase.Instance.GetBranchesPerTenSecondsByName(name);
		subMapManager.ManufactureBranches(count, name);
	}

	public void HandlePlots() {
		if (plotIndex < 0)
			return;
		PlotTrigger pt = transform.parent.Find("PlotTrigger").GetComponent<PlotTrigger>();
		string name = this.name.Split('-')[0];
		pt.SetPlotPath("PlotScripts/Plots/" + name + "/" + name + plotIndex);
		pt.gameObject.SetActive(true);

	}

	private void InitForAvesBaby() {
		Debug.Log("小鸟初始化");

		// 性别
		if (Random.Range(0, 100) < 50) {
			isMale = true;
			Debug.Log("这只宝宝是雄的");
		} else {
			isMale = false;
			Debug.Log("这只宝宝是雌的");
		}

		// 成熟性
		isMature = false;

		// 非野生型
		isFromCopulation = true;

		// 父母标识已在外部赋值

		// 更换sprite
		Debug.LogError("暂未处理小鸟宝宝的sprite，这与小鸟性别有关");

	}


}
