using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour {
	private int count = 0;

	public void AddCount(int count) {
		this.count += count;
	}

	private void OnMouseUpAsButton() {
		Debug.Log("捡了" + count + "根树枝");
		BranchUI.Instance.AddCount(count);
		count = 0;
		gameObject.SetActive(false);
	}
}
