using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour {
	private int count = 0;

	public void AddCount(int count) {
		this.count += count;
	}

	private void OnMouseUpAsButton() {
		AudioSource.PlayClipAtPoint(AudioManager.Instance.pickBranchesClip, Vector3.zero);
		BranchUI.Instance.AddCount(count);
		count = 0;
		gameObject.SetActive(false);
	}
}
