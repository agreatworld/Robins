using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMapManager : MonoBehaviour {


	#region internal structrue
	private struct Construction {
		public GameObject establishment;
		public int rank;
	}
	#endregion

	#region field
	private List<GameObject> avesSettled = new List<GameObject>();

	private Dictionary<GameObject, Aves> avesScriptsDic = new Dictionary<GameObject, Aves>();

	public SubMapType subMapType;

	[HideInInspector]
	/// <summary>
	/// 树枝
	/// </summary>
	public Branch branch;

	[HideInInspector]
	/// <summary>
	/// 地块唯一标识，一块地只能有一种鸟，暂用string标识，考虑改为枚举
	/// </summary>
	public string avesFlag;

	/// <summary>
	/// 子地图上的设施建造信息
	/// </summary>
	private Construction construction;

	/// <summary>
	/// 孵化小鸟计时器
	/// </summary>
	private float copulationCheckTimer = 0;

	/// <summary>
	/// 孵化小鸟时间间隔
	/// </summary>
	private float copulationCheckThresholdTime;
	#endregion



	#region monobehaviour
	private void Awake() {
		avesFlag = "";
		branch = transform.Find("Branch").gameObject.GetComponent<Branch>();
		branch.gameObject.SetActive(false);
		CalculateCopulationThresholdTime();
	}

	private void Update() {
		HandleAvesCopulation();

	}
	#endregion

	#region private methods

	private void CalculateCopulationThresholdTime() {
		copulationCheckThresholdTime = 10;
	}

	public void ManufactureBranches(int count, string avesName) {

		if (subMapType == AvesDataBase.Instance.GetAvesPreferredTypeByName(avesName)) {
			float rate = AvesDataBase.Instance.GetProfitRateWhenAvesMatchingTypeByName(name);
			count = (int)(count * rate);
		}
		branch.AddCount(count);
		branch.gameObject.SetActive(true);


	}


	private void HandleAvesCopulation() {
		copulationCheckTimer += Time.deltaTime;
		if (copulationCheckTimer < copulationCheckThresholdTime) {
			return;
		}
		// 重置计时器
		copulationCheckTimer = 0;

		if (avesSettled.Count != 2) {
			// 入住鸟数不等于2，不考虑交配：1不满足交配条件；3已达地块容纳最大数量
			return;
		}

		// 缓存两只鸟的信息
		Aves aves1 = avesScriptsDic[avesSettled[0]];
		Aves aves2 = avesScriptsDic[avesSettled[1]];

		// 对入住鸟类的性别进行鉴定
		if (aves1.isMale == aves2.isMale) {
			return;
		}
		// 对入住鸟类的成熟性进行鉴定，小鸟不宜
		if (!aves1.isMature || !aves2.isMature) {
			return;
		}
		// 对入住鸟类的进行伦理鉴定
		if (aves1.isFromCopulation) {
			if (aves1.parentsIndex[0] == aves2.ID || aves1.parentsIndex[1] == aves2.ID) {
				return;
			}
		}
		if (aves2.isFromCopulation) {
			if (aves2.parentsIndex[0] == aves1.ID || aves2.parentsIndex[1] == aves1.ID) {
				return;
			}
		}

		// 产出小鸟
		Debug.Log("小鸟出生");
		int fatherID, motherID;
		fatherID = aves1.isMale ? aves1.ID : aves2.ID;
		motherID = aves1.isMale ? aves2.ID : aves1.ID;
		AddNewAves(Instantiate(avesSettled[0]), fatherID, motherID);
		HandleCopulationGuide();
	}

	private void HandleCopulationGuide() {
		if (GameGuide.Instance.isGameGuiding) {
			if (!GameGuide.Instance.copulationGuideLoaded) {
				GameGuide.Instance.LoadCopulationGuide2();
				GameGuide.Instance.copulationGuideLoaded = true;
			}
		}
	}

	#endregion

	#region public methods
	public bool AvailableForNewAves() {
		if (avesSettled.Count <= 3) {
			return true;
		} else {
			return false;
		}
	}

	public bool AddNewAves(GameObject newAves, int father = 0, int mother = 0) {
		var words = newAves.name.Split('-');
		if (avesSettled.Count == 0) {
			// 若地块还没有任何鸟类，标记地块标识
			avesFlag = words[0];
		} else {
			if (avesFlag != words[0]) {
				Debug.Log("该地图已被其他鸟类占领");
				return false;
			}
		}
		avesSettled.Add(newAves);
		Aves aves = newAves.GetComponent<Aves>();
		aves.parentsIndex[0] = father;
		aves.parentsIndex[1] = mother;
		avesScriptsDic.Add(newAves, aves);
		newAves.transform.position = transform.position;
		newAves.transform.parent = transform;
		newAves.SetActive(true);
		newAves.GetComponent<Aves>().HandlePlots();
		return true;
	}


	public void AddBranchEventsForGuide() {
		branch.gameObject.AddComponent<BranchEventsForGuide>();
	}

	/// <summary>
	/// 设施建造
	/// </summary>
	public void Construct(GameObject establishment, int rank) {
		if (GameGuide.Instance.isGameGuiding) {
			DialogueController.Instance.ShowDialogue();
			DialogueManager.Instance.UpdateDialogueStatus();
			DialogueManager.Instance.PlayNext();
		}
		construction.establishment = establishment;
		construction.rank = rank;
		construction.establishment.transform.parent = transform;
		construction.establishment.transform.localPosition = Vector2.zero;
	}

	public List<GameObject> GetAvesSettled() {
		return avesSettled;
	}

	/// <summary>
	/// 清除鸟类
	/// </summary>
	public void ClearAves() {
		avesFlag = null;
		avesSettled.Clear();
		avesScriptsDic.Clear();
	}

	#endregion

}
