using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBranchesGuide : MonoBehaviour {
	public static CollectBranchesGuide Instance;

	public bool readyForCollectBranchesGuide = false;

	private float timer = 0;

	public float timeThreshold = 10;

	private bool guideLoaded = false;

	private void Awake() {
		Instance = this;

		// remove resources中树枝上的引导脚本，该脚本只需要一个
		GameObject branch = Resources.Load<GameObject>("Branch");
	}

	private void Update() {
		if (!guideLoaded) {
			timer += Time.deltaTime;
			if (timer > timeThreshold) {
				guideLoaded = true;
				GameGuide.Instance.LoadGameGuide("PlotScripts/GameGuide/收取树枝教学.txt", new List<DialogueManager.AttachToSentence> {
					ClickBranchGuide
				});
			}
		}
	}

	private void ClickBranchGuide() {
		GameGuide.Instance.guideMask.SetActive(true);
		DialogueController.Instance.HideDialogue();
	}
}
