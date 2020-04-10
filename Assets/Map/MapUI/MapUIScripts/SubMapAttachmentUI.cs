using UnityEngine;

public class SubMapAttachmentUI : MonoBehaviour {

	public static SubMapAttachmentUI Instance;

	private void Awake() {
		Instance = this;
	}

	public void UpdateInfo() {
		Debug.Log("刷新信息面板");
	}


}
