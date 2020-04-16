using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameGuide : MonoBehaviour {

	public static GameGuide Instance;

	[HideInInspector]
	public GameObject guideMask;

	[HideInInspector]
	/// <summary>
	/// 是否正在进行新手引导
	/// </summary>
	public bool isGameGuiding = true;

	[HideInInspector]
	/// <summary>
	/// 新手引导期间是否启用鼠标对地图的事件
	/// </summary>
	public bool enableMouseEvents = false;

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
		AvesSettleManager.Instance.AddAves("山麻雀");
		GameObject passer = AvesSettleManager.Instance.transform.Find("AvesSettleManager").Find("山麻雀").gameObject;
		AvesSettleManager.Instance.gameObject.GetComponent<Canvas>().sortingOrder = 110;
		passer.AddComponent<PasserSettleGuide>();
		DialogueManager.Instance.UpdateDialogueStatus();
	}
	private void ClickPasserGuide() {
		DialogueController.Instance.HideDialogue();
		guideMask = Instantiate(Resources.Load<GameObject>("GameGuide/GameGuideMask")) as GameObject;
		// 等待回调方法
	}
	private void ClickSubMapGuide() {

		// 等待回调方法，并再其中进行后续工作
	}

	#endregion

	#region 交配教学
	public void LoadCopulationGuide() {
		Debug.Log("加载交配教学");
	}
	#endregion

}
