using System.Collections;
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
				string scriptDirectory = "PlotScripts/GameGuide/";
				string scriptName;
				switch (index) {
					case 0:
						scriptName = "起始引导.txt";
						break;
					case 1:
						scriptName = "入住教学.txt";
						break;
					case 2:
						scriptName = "收取树枝教学.txt";
						break;
					case 3:
						scriptName = "设施建造教学.txt";
						break;
					case 4:
						scriptName = "交配教学.txt";
						break;
					case 5:
						scriptName = "开拓土地教学.txt";
						break;
					case 6:
						scriptName = "迁移教学.txt";
						break;
					case 7:
						scriptName = "商店教学.txt";
						break;
					default:
						scriptName = "小木屋.txt";
						break;
				}
				string scriptPath = scriptDirectory + scriptName;
				LoadGameGuide(scriptPath);
			}
		}
	}


	public void LoadGameGuide(string scriptPath) {
		//DialogueManager.Instance.LoadScript(scriptPath);
		++index;
		isWaitingForScriptEnding = true;
	}



}
