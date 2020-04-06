using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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

		public bool lastContentFinished {
			private set; get;
		} = true;

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

		public void ContentChangeTo(string newContent) {
			lastContentFinished = false;
			float time = newContent.ToCharArray().Length * 0.065f;
			content.text = null;
			content.DOText(newContent, time).OnComplete(()=>{
				lastContentFinished = true;
			});
		}

		public void Reset() {
			avatar.sprite = null;
			name.text = "";
			content.text = "";
		}

	}

	/// <summary>
	/// 剧本类，不涉及monobehaviour，仅作为剧本解析的容器
	/// </summary>
	private class Script {
		private List<string> names = new List<string>(); // 剧本对话者的名字
		private List<string> contents = new List<string>(); // 剧本对话内容
		public bool isSingleProtagonist {
			private set; get;
		}

		public bool atScriptsEnd {
			private set; get;
		}
		/// <summary>
		/// 播放文本信息时用的索引，第一次刷新时递增为0
		/// </summary>
		private int index = -1;

		public Script(bool isSingleProtagonist, List<string> names, List<string> contents) {
			this.isSingleProtagonist = isSingleProtagonist;
			this.names = names;
			this.contents = contents;
			index = -1;
			atScriptsEnd = false;
		}

		/// <summary>
		/// 获取信息前更新索引值
		/// </summary>
		public void UpdateIndex() {

			if (++index >= contents.Count) {
				atScriptsEnd = true;
			}
			if (atScriptsEnd) {
				DialogueController.Instance.HideDialogue();
				return;
			}
		}

		public string GetNextContent() {
			return atScriptsEnd ? "" : contents[index];
		}

		public void Reset() {
			names.Clear();
			contents.Clear();
			isSingleProtagonist = true;
			atScriptsEnd = false;
			index = -1;
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
		Transform dialogue = transform.Find("Dialogue");
		Image avatar = dialogue.Find("Avatar").GetComponent<Image>();
		Text name = dialogue.Find("Name").GetComponent<Text>();
		Text content = dialogue.Find("Content").GetComponent<Text>();
		Image autoPlayButton = dialogue.Find("AutoPlayButton").GetComponent<Image>();
		Image historyInfoButton = dialogue.Find("HistoryInfoButton").GetComponent<Image>();
		dialogueTree = new DialogueTree(avatar, name, content, autoPlayButton, historyInfoButton);
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
	private void ParseScript(string scriptPath) {
		bool isSingleProtagonist = false;
		List<string> names = new List<string>();
		List<string> contents = new List<string>();
		// 读取剧本
		try {
			using (StreamReader sr = new StreamReader(scriptPath)) {
				string txt = sr.ReadToEnd();
				string[] segments = txt.Split(new string[] { "\r\n#\r\n" }, StringSplitOptions.RemoveEmptyEntries);
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
		InitDialogue();
	}

	/// <summary>
	/// 初始化对话树UI，加载头像和名字
	/// </summary>
	private void InitDialogue() {
		Debug.LogError("还未初始化对话树");
	}

	public void PlayNext() {
		// 若上一条语句未显示完毕，暂不显示下一条
		if (!dialogueTree.lastContentFinished) {
			return;
		}
		dialogueScript.UpdateIndex();
		dialogueTree.ContentChangeTo(dialogueScript.GetNextContent());
	}

	public void ResetAll() {
		dialogueScript.Reset();
		dialogueTree.Reset();
	}

	public void LoadScript(string scriptPath) {
		ParseScript(scriptPath);
		DialogueController.Instance.ShowDialogue();

	}

	#endregion




}
