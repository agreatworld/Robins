using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class GameGuide : MonoBehaviour {

	public static GameGuide Instance;

	private bool isWaitingForScriptEnding = false;

	/// <summary>
	/// 共八个剧本，此索引用于加载所需剧本
	/// </summary>
	private int index = 0;

	private void Awake() {
		Instance = this;
	}

	private void Update() {
		if (isWaitingForScriptEnding) {
			if (!DialogueController.Instance.isDialoguePlaying) {
				isWaitingForScriptEnding = false;
				// 刚播放完一组剧本
				switch (index) {
					case 0:
						LoadGameGuide("PlotScripts/GameGuide/起始引导.txt", null);
						break;
					case 1:
						LoadGameGuide("PlotScripts/GameGuide/入住教学.txt", new List<DialogueManager.AttachToSentence>{ 
							ShowPasser,
							ClickPasserGuide,
							ClickSubMapGuide
						});
						break;
					
					
				}
			}
		}
	}


	public void LoadGameGuide(string scriptPath, List<DialogueManager.AttachToSentence> attachToSentences) {
		DialogueManager.Instance.LoadDialogue(scriptPath, attachToSentences);
		++index;
		isWaitingForScriptEnding = true;
	}

	#region 入住教学委托
	private void ShowPasser() {
		GameObject passer = Resources.Load<GameObject>("GameGuide/Passer");
		GameObject go =  Instantiate(passer, new Vector2(-7, 0), Quaternion.identity) as GameObject;
		go.GetComponent<SpriteRenderer>().DOFade(1, 1.2f);
		DialogueManager.Instance.UpdateDialogueStatus();
	}
	private void ClickPasserGuide() {
		DialogueController.Instance.HideDialogue();
		Instantiate(Resources.Load<GameObject>("GameGuide/GameGuideMask"));
	}
	private void ClickSubMapGuide() {

	}
	#endregion
}
