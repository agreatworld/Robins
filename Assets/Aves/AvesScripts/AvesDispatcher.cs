using System.Collections.Generic;
using UnityEngine;

public class AvesDispatcher : MonoBehaviour {

	private float timer = 0;
	private float time = 10;
	private bool[] handled = {
		false, false, false
	};

	/// <summary>
	/// 阶梯一鸟类
	/// </summary>
	private List<string> candidatesRank1 = new List<string>();

	/// <summary>
	/// 各鸟类是否已出现多次
	/// </summary>
	private Dictionary<string, bool> hasShownUp = new Dictionary<string, bool>();

	/// <summary>
	/// 阶梯二鸟类
	/// </summary>
	private List<string> candidatesRank2 = new List<string>();

	/// <summary>
	/// 阶梯三鸟类
	/// </summary>
	private List<string> candidatesRank3 = new List<string>();


	private int rank = 1;
	private void Awake() {
		candidatesRank1.Add("山麻雀");
		candidatesRank1.Add("池鹭");
		candidatesRank2.Add("大鵟");
		candidatesRank2.Add("褐冠鹃隼");
		candidatesRank3.Add("黑冠鹃隼");
		candidatesRank3.Add("白琵鹭");
		candidatesRank3.Add("金雕");
		candidatesRank3.Add("知更鸟");
		candidatesRank3.Add("东方白鹳");
		hasShownUp.Add("山麻雀", false);
		hasShownUp.Add("池鹭", false);
		hasShownUp.Add("大鵟", false);
		hasShownUp.Add("褐冠鹃隼", false);
		hasShownUp.Add("黑冠鹃隼", false);
		hasShownUp.Add("白琵鹭", false);
		hasShownUp.Add("金雕", false);
		hasShownUp.Add("知更鸟", false);
		hasShownUp.Add("东方白鹳", false);
	}

	private void Update() {
		HandleTimer();
	}

	private void ResetHandled() {
		handled[0] = false;
		handled[1] = false;
		handled[2] = false;
	}

	private void HandleTimer() {
		if (GameGuide.Instance.isGameGuiding)
			return;
		timer += Time.deltaTime;
		if (rank == 1) {
			if (timer > time) {
				AddAves(1);
				timer = 0;
			}
			return;
		}
		if (rank == 2) {
			if (timer > time && !handled[0]) {
				AddAves(1);
				handled[0] = true;
			}
			if (timer > time * 2 && !handled[1]) {
				AddAves(2);
				timer = 0;
				ResetHandled();
			}
			return;
		}
		if (rank == 3) {
			if (timer > time && !handled[0]) {
				AddAves(1);
				handled[0] = true;
			}
			if (timer > time * 2 && !handled[1]) {
				AddAves(2);
				handled[1] = true;
			}
			if (timer > time * 3 && !handled[2]) {
				AddAves(3);
				timer = 0;
				ResetHandled();
			}
			return;
		}
	}
	private void AddAves(int rank) {
		int index = 0;
		string name = null;
		switch (rank) {
			case 1:
				index = Random.Range(0, candidatesRank1.Count);
				name = candidatesRank1[index];
				break;
			case 2:
				index = Random.Range(0, candidatesRank2.Count);
				name = candidatesRank2[index];
				break;
			case 3:
				index = Random.Range(0, candidatesRank3.Count);
				name = candidatesRank3[index];
				break;
		}
		hasShownUp[name] = true;
		AvesSettleManager.Instance.AddAves(name);
		UpdateRank();
	}
	private void UpdateRank() {
		bool shouldUpgradeRank = true;
		if (rank == 1) {
			foreach (var aves in candidatesRank1) {
				if (!hasShownUp[aves]) {
					shouldUpgradeRank = false;
					break;
				}
			}
		} else if (rank == 2) {
			foreach (var aves in candidatesRank2) {
				if (!hasShownUp[aves]) {
					shouldUpgradeRank = false;
					break;
				}
			}
		} else if (rank == 3) {
			foreach (var aves in candidatesRank2) {
				if (!hasShownUp[aves]) {
					shouldUpgradeRank = false;
					break;
				}
			}
		}
		if (shouldUpgradeRank) {
			if (rank + 1 <= 3) {
				++rank;
				if (rank == 2) {
					time = 12;
				} else if (rank == 3) {
					time = 15;
				}
			}
		}
	}
}
