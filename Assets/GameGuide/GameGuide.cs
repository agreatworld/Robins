using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class GameGuide : MonoBehaviour {

	public static GameGuide Instance;

	/// <summary>
	/// 是否正在进行新手引导
	/// </summary>
	public bool isGameGuiding = true;

	private void Awake() {
		Instance = this;
	}
	private void Start() {
		LoadGameGuide("PlotScripts/GameGuide/起始引导.txt", new List<DialogueManager.AttachToSentence> {
			LoadSettleGuide
		});

	}

	public void LoadGameGuide(string scriptPath, List<DialogueManager.AttachToSentence> attachToSentences) {
		DialogueManager.Instance.LoadDialogue(scriptPath, attachToSentences);
	}

	#region 起始引导委托
	private void LoadSettleGuide() {
		LoadGameGuide("PlotScripts/GameGuide/入住教学.txt", new List<DialogueManager.AttachToSentence> {
				ShowPasser,
				ClickPasserGuide,
				ClickSubMapGuide
			});
	}
	#endregion

	#region 入住教学委托
	private void ShowPasser() {
		GameObject passer = Resources.Load<GameObject>("GameGuide/Passer");
		GameObject go = Instantiate(passer, new Vector2(-7, 0), Quaternion.identity) as GameObject;
		go.AddComponent<PasserSettleGuide>();
		go.GetComponent<SpriteRenderer>().DOFade(1, 1.2f);
		DialogueManager.Instance.UpdateDialogueStatus();
	}
	private void ClickPasserGuide() {
		DialogueController.Instance.HideDialogue();
		GameObject mask = Instantiate(Resources.Load<GameObject>("GameGuide/GameGuideMask")) as GameObject;
		PasserSettleGuide.Instance.guideMask = mask;
		// 等待回调方法
	}
	private void ClickSubMapGuide() {
		PasserSettleGuide.Instance.getReadyForSettleDown = true;
		// 等待回调方法，并再其中进行后续工作
	}
	#endregion

	#region 收取树枝教学委托

	#endregion
}
