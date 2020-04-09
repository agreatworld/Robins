using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMapManager : MonoBehaviour {

	#region field
	private List<GameObject> avesSettled = new List<GameObject>();

	/// <summary>
	/// 地块唯一标识，一块地只能有一种鸟，暂用string标识，考虑改为枚举
	/// </summary>
	public string avesFlag;

	/// <summary>
	/// 产出树枝计时器
	/// </summary>
	private float manufactureBranchesTimer = 0;

	[SerializeField]
	private float manufactureBranchesTimeThreshold = 10f;

	#endregion


	#region monobehaviour
	private void Awake() {

	}

	private void Update() {
		ManufactureBranches();
	}
	#endregion

	#region private methods
	private void ManufactureBranches() {
		if (avesSettled.Count == 0) {
			// 没有已入住鸟类，无树枝产出
			return;
		}
		manufactureBranchesTimer += Time.deltaTime;
		if (manufactureBranchesTimer < manufactureBranchesTimeThreshold) {
			// 时间未满不产出树枝
			return;
		}
		manufactureBranchesTimer = 0;
		// 根据入住鸟类数量、当地建筑设施等产出树枝
		int count = avesSettled.Count;
		Debug.Log("产出树枝" + count);
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

	public void AddNewAves(GameObject newAves) {
		if (avesSettled.Count == 0) {
			// 若地块还没有任何鸟类，标记地块标识
			avesFlag = newAves.name;
		}
		avesSettled.Add(newAves);
		newAves.transform.position = transform.position;
	}

	#endregion

}
