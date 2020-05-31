using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BranchUI : MonoBehaviour {

	[HideInInspector]
	public int branchCount = 0;

	private Text text;

	public static BranchUI Instance {
		private set; get;
	}

	private void Awake() {
		Instance = this;
		text = transform.Find("Text").GetComponent<Text>();
	}

	public void AddCount(int count) {
		branchCount += count;
		text.text = branchCount.ToString();
	}

	public void SubtractCount(int count) {
		int c = branchCount - count;
		if (c >= 0) {
			branchCount = c;
			text.text = branchCount.ToString();
		} else {
			Debug.LogError("树枝不足，消费失败");
		}

	}

}
