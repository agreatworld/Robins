using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private void Start() {
		//GameGuide.Instance.LoadGameGuide("PlotScripts/GameGuide/起始引导.txt");
		DialogueManager.Instance.LoadDialogue("PlotScripts/GameGuide/test.txt", new List<DialogueManager.AttachToSentence>{ 
			Test1,
			Test2
		});
	}

	public void Test1() {
		Debug.Log("test1");
		DialogueManager.Instance.UpdateDialogueStatus();
	}
	public void Test2() {
		Debug.Log("test2");
		DialogueManager.Instance.UpdateDialogueStatus();
	}
}
