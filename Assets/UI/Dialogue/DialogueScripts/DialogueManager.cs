using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Net;
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

		public void Init(string name, string content, Sprite sprite) {
			this.name.text = name;
			avatar.sprite = sprite;
			ContentChangeTo(content);
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
			if (time == 0) {
				return;
			}
			content.DOText(newContent, time).OnComplete(()=>{
				Instance.dialogueScript.GetReact()?.Invoke();
			});
		}

		public void UpdateStatus() {
			lastContentFinished = true;
		}

		public void Reset() {
			avatar.sprite = null;
			name.text = "";
			content.text = "";
			lastContentFinished = true;
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

		/// <summary>
		/// 控制每一条语句对应的动画交互
		/// key：语句的索引序列值
		/// value：对应的动画方法索引值，从0计数，-1表示没有对应的交互
		/// </summary>
		public Dictionary<int, int> reactDic = new Dictionary<int, int>();

		/// <summary>
		/// 存放需要调用的方法
		/// </summary>
		private List<AttachToSentence> attachToSentence = new List<AttachToSentence>();

		public Script(bool isSingleProtagonist, List<string> names, List<string> contents, Dictionary<int, int> reactDic, List<AttachToSentence> attachToSentence) {
			this.isSingleProtagonist = isSingleProtagonist;
			this.names = names;
			this.contents = contents;
			index = -1;
			atScriptsEnd = false;
			this.reactDic = reactDic;
			this.attachToSentence = attachToSentence;
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

		public string GetNextName() {
			return atScriptsEnd ? "" : names[index];
		}

		public AttachToSentence GetReact() {
			int reactIndex = reactDic[index];
			return reactIndex == -1 ? null : attachToSentence[reactIndex];
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
	public delegate void AttachToSentence();


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
	private void ParseScript(string scriptPath, List<AttachToSentence> reacts) {
		bool isSingleProtagonist = false;
		List<string> names = new List<string>();
		List<string> contents = new List<string>();
		Dictionary<int, int> reactDic = new Dictionary<int, int>();
		int count = 0; // 需要委托方法的数量
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
					// patches[0]存放了对话者的名字，patches[1]存放语句内容
					string[] patches = segments[i].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
					names.Add(patches[0]);
					string[] words = patches[1].Split('*');
					// words[0]是标志位，定义这一语句是否有伴随交互；words[1]存放具体内容
					int key = contents.Count, value = -1;
					if (words.Length == 1) {
						value = -1;
						contents.Add(words[0]);
					} else {
						value = count++;
						contents.Add(words[1]);
					}
					reactDic.Add(key, value);
				}
			}
		} catch (Exception e) {
			// 向用户显示出错消息
			Debug.LogError(e.Message);
		}
		dialogueScript = new Script(isSingleProtagonist, names, contents, reactDic, reacts);
		InitDialogue();
	}

	/// <summary>
	/// 初始化对话树UI，加载头像和名字
	/// </summary>
	private void InitDialogue() {
		Debug.LogError("还未初始化头像");
		dialogueScript.UpdateIndex();
		dialogueTree.Init(dialogueScript.GetNextName(), dialogueScript.GetNextContent(), null);
	}

	public void PlayNext() {
		// 若上一条语句未显示完毕，暂不显示下一条
		if (!dialogueTree.lastContentFinished) {
			return;
		}
		dialogueScript.UpdateIndex();
		dialogueTree.ContentChangeTo(dialogueScript.GetNextContent());
		if (!dialogueScript.isSingleProtagonist) {
			dialogueTree.NameChangeTo(dialogueScript.GetNextName());
		}
	}

	public void ResetAll() {
		dialogueScript.Reset();
		dialogueTree.Reset();
	}

	private void LoadScript(string scriptPath, List<AttachToSentence> reacts) {
		ParseScript(scriptPath, reacts);
		DialogueController.Instance.ShowDialogue();
	}

	public void LoadDialogue(string scriptPath, List<AttachToSentence> reacts) {
		LoadScript(scriptPath, reacts);
	}

	public void UpdateDialogueStatus() {
		dialogueTree.UpdateStatus();
	}

	#endregion




}
