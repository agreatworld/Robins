using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvesTransitionManager : MonoBehaviour {

	public static AvesTransitionManager Instance {
		private set; get;
	}

	/// <summary>
	/// 转移源
	/// </summary>
	private SubMapManager source;

	/// <summary>
	/// 转移目的地
	/// </summary>
	private SubMapManager dest;


	[HideInInspector]
	public bool waitForDest;
	private void Awake() {
		Instance = this;
		waitForDest = false;
	}


	public void SetSource(SubMapManager source) {
		this.source = source;
		waitForDest = true;
	}

	public void SetDest(SubMapManager dest) {
		this.dest = dest;
		TransitionAves();
		MapUIController.Instance.ResetAllSubMaps();
	}

	/// <summary>
	/// 迁移鸟类，成功返回ture，失败返回false
	/// </summary>
	/// <returns></returns>
	public bool TransitionAves() {
		if (!Check(source.avesFlag, source.GetAvesSettled().Count, dest.avesFlag, dest.GetAvesSettled().Count)) {
			return false;
		}
		// 迁移鸟类
		foreach (var aves in source.GetAvesSettled()) {
			Aves a = aves.GetComponent<Aves>();
			dest.AddNewAves(aves, a.parentsIndex[0], a.parentsIndex[1]);
		}
		source.ClearAves();
		return true;
	}

	/// <summary>
	/// 检查是否满足转移条件
	/// </summary>
	/// <param name="sourceAvesName"></param>
	/// <param name="sourceAvesCount"></param>
	/// <param name="destinationAvesName"></param>
	/// <param name="destinationAvesCount"></param>
	/// <returns></returns>
	private bool Check( string sourceAvesName, 
						int sourceAvesCount, 
						string destinationAvesName,
						int destinationAvesCount) {
		if (destinationAvesName == "") {
			return true;
		}
		if (destinationAvesName != sourceAvesName) {
			return false;
		}
		if (sourceAvesCount + destinationAvesCount > 3) {
			return false;
		}
		return true;
	}

}
