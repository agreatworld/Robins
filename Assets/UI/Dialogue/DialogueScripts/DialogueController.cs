﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制对话树UI的显示隐藏
/// </summary>
public class DialogueController : MonoBehaviour {

	public static DialogueController Instance;

	public GameObject dialogue;

	private void Awake() {
		Instance = this;
		dialogue = transform.Find("Dialogue").gameObject;
	}

	public void HideDialogue() {
		dialogue.SetActive(false);
	}

	public void ShowDialogue() {
		dialogue.SetActive(true);
	}
}
