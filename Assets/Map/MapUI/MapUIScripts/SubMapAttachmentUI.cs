using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMapAttachmentUI : MonoBehaviour {

	public static SubMapAttachmentUI Instance;

	public Image[] avatars;

	private Sprite[] avesAvatarSprites;

	private void Awake() {
		Instance = this;
		avesAvatarSprites = Resources.LoadAll<Sprite>("Aves/AvesAvatar");
	}

	private Sprite SearchSprite(string name) {
		if (name == null) {
			return null;
		}
		foreach(var sprite in avesAvatarSprites) {
			if (sprite.name == name) {
				return sprite;
			}
		}
		return null;
	}

	public void UpdateInfo(List<GameObject> aves) {
		string[] avesNames = new string[3];
		for (int i = 0; i < 3; ++i) {
			if (i < aves.Count) {
				avesNames[i] = aves[i].name;
			} else {
				avesNames[i] = null;
			}
		}
		for (int i = 0; i < 3; ++i) {
			avatars[i].sprite = SearchSprite(avesNames[i]);
		}

		Debug.Log("刷新信息面板");
	}


}
