using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AvesDispatcher : MonoBehaviour {
	/// <summary>
	/// 阶梯数据存储
	/// </summary>
	private Dictionary<string, int> avesRankDic = new Dictionary<string, int>();
	/// <summary>
	/// 可选鸟类
	/// </summary>
	private List<string> candidates = new List<string>();
	/// <summary>
	/// 各鸟类是否第一次出现
	/// </summary>
	private Dictionary<string, bool> avesFirstShowUp = new Dictionary<string, bool>();
	private int rank = 1;
	private void Awake() {
		avesRankDic.Add("山麻雀", 1);
		avesRankDic.Add("池鹭", 1);
		avesRankDic.Add("褐冠鹃隼", 2);
		avesRankDic.Add("白琵鹭", 3);
		avesRankDic.Add("金雕", 3);
		avesRankDic.Add("知更鸟", 3);
		avesRankDic.Add("东方白鹳", 3);
		candidates.Add("山麻雀");
		candidates.Add("池鹭");
		avesFirstShowUp.Add("山麻雀", true); // 新手引导引入山麻雀
		avesFirstShowUp.Add("池鹭", false);
		avesFirstShowUp.Add("褐冠鹃隼", false);
		avesFirstShowUp.Add("白琵鹭", false);
		avesFirstShowUp.Add("金雕", false);
		avesFirstShowUp.Add("知更鸟", false);
		avesFirstShowUp.Add("东方白鹳", false);
	}

	private void AddAves() {
		int index = Random.Range(0, candidates.Count);
		avesFirstShowUp[candidates[index]] = true;
		AvesSettleManager.Instance.AddAves(candidates[index]);
		UpdateRank();
	}

	private void UpdateRank() {
		if (CheckRankConditions()) {
			++rank;
			if (rank > 3) {
				rank = 3;
			} else {
				candidates.Clear();
				foreach (var pair in avesRankDic) {
					if (pair.Value <= rank) {
						candidates.Add(pair.Key);
					}
				}
			}

		}
	}

	private bool CheckRankConditions() {
		bool upgrade = true;
		foreach (var aves in candidates) {
			if (!avesFirstShowUp[aves]) {
				upgrade = false;
				break;
			}
		}
		return upgrade;
	}
}
