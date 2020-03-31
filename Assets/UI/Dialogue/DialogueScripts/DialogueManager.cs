using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制对话树的内容
/// </summary>
public class DialogueManager : MonoBehaviour {

	#region internal class
	/// <summary>
	/// 对话框UI
	/// </summary>
	private class DialogueTree {

		private Image avatar;

		private Text name;

		private Text content;

		private Image autoPlayButton;

		private Image historyInfoButton;

		public DialogueTree(Image avatar, Text name, Text content, Image autoPlayButton, Image historyInfoButton) {
			this.avatar = avatar;
			this.name = name;
			this.content = content;
			this.autoPlayButton = autoPlayButton;
			this.historyInfoButton = historyInfoButton;
		}

		public void AvatarChangeTo(Sprite newAvatar) {
			avatar.sprite = newAvatar;
		}

		public void NameChangeTo(string newName) {
			name.text = newName;
		}

		public void AutoPlayButtonChangeTo(Sprite newSprite) {
			autoPlayButton.sprite = newSprite;
		}

		public void HistoryInfoButtonChangeTo(Sprite newSprite) {
			historyInfoButton.sprite = newSprite;
		}
	}

	/// <summary>
	/// 剧本类，不涉及monobehaviour，仅作为剧本解析的容器
	/// </summary>
	private class Script {
		public bool isSingleProtagonist; // 是否单主角
		public List<string> names = new List<string>(); // 剧本对话者的名字
		public List<string> contents = new List<string>(); // 剧本对话内容

		public Script(bool isSingleProtagonist, List<string> names, List<string> contents) {
			this.isSingleProtagonist = isSingleProtagonist;
			this.names = names;
			this.contents = contents;
		}
	}

	#endregion

	#region field
	public static DialogueManager Instance;

	private DialogueTree dialogueTree;

	private Script dialogueScript;
	#endregion

	#region monobehaviour
	private void Awake() {
		Instance = this;
		ParseScript("test.txt");
	}
	#endregion

	#region configure dialogue tree
	public void DialogueAvatarChangeTo(Sprite sprite) {
		dialogueTree.AvatarChangeTo(sprite);
	}

	public void DialogueNameChangeTo(string name) {
		dialogueTree.NameChangeTo(name);
	}

	public void DialogueAutoPlayButtonChangeTo(Sprite sprite) {
		dialogueTree.AutoPlayButtonChangeTo(sprite);
	}

	public void DialogueHistoryInfoButtonChangeTo(Sprite sprite) {
		dialogueTree.HistoryInfoButtonChangeTo(sprite);
	}

	#endregion

	public void ParseScript(string scriptPath) {
		bool isSingleProtagonist = false;
		List<string> names = new List<string>();
		List<string> contents = new List<string>();
		// 读取剧本
		try {
			using (StreamReader sr = new StreamReader(scriptPath)) {
				string txt = sr.ReadToEnd();
				string[] segments = txt.Split(new string[]{ "\r\n#\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < segments.Length; ++i) {
					// 判断剧本是否单主角
					if (i == 0) {
						int result = -1;
						if (int.TryParse(segments[i], out result)) {
							isSingleProtagonist = result == 1 ? true : false;
						}
						continue;
					}
					string[] patch = segments[i].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
					names.Add(patch[0]);
					contents.Add(patch[1]);
				}
			}
		} catch (Exception e) {
			// 向用户显示出错消息
			Debug.LogError(e.Message);
		}
		dialogueScript = new Script(isSingleProtagonist, names, contents);
	}
}
