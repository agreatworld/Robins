using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMapAttachmentUI : MonoBehaviour {

	public static SubMapAttachmentUI Instance;

	/// <summary>
	/// 当前面板关联的子地图管理器
	/// </summary>
	public SubMapManager currentSubMapManager;

	#region giving values from hierarchy panel
	public Image[] avatars;

	public Image subMapTypeImage;
	#endregion

	#region resources
	private Sprite[] avesAvatarSprites;

	private Sprite mountainAvatar;

	private Sprite lakeAvatar;

	private Sprite forestAvatar;

	private Sprite grasslandAvatar;
	#endregion
	private void Awake() {
		Instance = this;
		avesAvatarSprites = Resources.LoadAll<Sprite>("Aves/AvesAvatar");
		mountainAvatar = Resources.Load<Sprite>("MapUI/SubMapTypeAvatar/Mountain");
		lakeAvatar = Resources.Load<Sprite>("MapUI/SubMapTypeAvatar/Lake");
		forestAvatar = Resources.Load<Sprite>("MapUI/SubMapTypeAvatar/Forest");
		grasslandAvatar = Resources.Load<Sprite>("MapUI/SubMapTypeAvatar/Grassland");
	}

	private Sprite SearchAvesAvatarSprite(string name) {
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

	private Sprite SearchSubMapTypeSprite(SubMapType type) {
		switch (type) {
			case SubMapType.Mountain:
				return mountainAvatar;
			case SubMapType.Lake:
				return lakeAvatar;
			case SubMapType.Forest:
				return forestAvatar;
			case SubMapType.Grassland:
				return grasslandAvatar;
			default:
				return null;
		}
	}

	public void UpdateInfo(SubMapManager subMapManager, List<GameObject> aves, SubMapType type) {
		string[] avesNames = new string[3];
		for (int i = 0; i < 3; ++i) {
			if (i < aves.Count) {
				avesNames[i] = aves[i].name;
			} else {
				avesNames[i] = null;
			}
		}
		for (int i = 0; i < 3; ++i) {
			avatars[i].sprite = SearchAvesAvatarSprite(avesNames[i]);
		}
		currentSubMapManager = subMapManager;
		//subMapTypeImage.sprite = SearchSubMapTypeSprite(type);
		var sprite = SearchSubMapTypeSprite(type);
		if (sprite != null) {
			subMapTypeImage.sprite = sprite;
		} else {
			Debug.LogError("未获取地图类型的头像");
		}

		Debug.Log("刷新信息面板");
	}


}
