using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapUIAnimation : MonoBehaviour {

	private RectTransform[] buttonTransforms;
	private Image[] buttons;
	/// <summary>
	/// 根按钮
	/// </summary>
	private RectTransform rootTransform;
	/// <summary>
	/// 根按钮sprite
	/// </summary>
	private Image rootButton;
	[SerializeField]
	private Vector3[] offsets = {
		new Vector3(0.5f, 0.3f, 0),
		new Vector3(0.7f, 0, 0),
		new Vector3(0.5f, -0.3f, 0)
	};
	private Vector3[] startPos = new Vector3[3];
	private Vector3 buttonScale;
	private Vector3 rootScale;
	[HideInInspector]
	public bool buttonsShowing = false;
	[HideInInspector]
	public bool rootShowing = false;
	[HideInInspector]
	public bool mouseInsideRootButton = false;

	private void Awake() {
		// 初始化字段
		var transforms = GetComponentsInChildren<RectTransform>(); // 返回的第一个物体是根物体，所以需要处理一下
		rootTransform = transforms[0];
		rootButton = rootTransform.GetComponent<Image>();

		buttonTransforms = new RectTransform[transforms.Length - 1];
		buttons = new Image[transforms.Length - 1];
		for (int i = 0; i < buttonTransforms.Length; ++i) {
			buttonTransforms[i] = transforms[i + 1];
			startPos[i] = buttonTransforms[i].position;
			buttons[i] = buttonTransforms[i].GetComponent<Image>();
		}
		rootScale = rootTransform.localScale;
		buttonScale = buttonTransforms[0].localScale;

		for (int i = 0; i < offsets.Length; ++i) {
			offsets[i] *= buttonScale.x;
		}
		
		// 初始化按钮UI
		rootButton.DOFade(0, 0.01f);
		foreach (var buttonTransform in buttonTransforms) {
			buttonTransform.position = rootTransform.transform.position;
			buttonTransform.localScale = rootTransform.localScale;
		}
		UpdateAlphas();

	}

	public void ShowButtons() {
		buttonsShowing = true;
		rootShowing = true;
		UpdateAlphas();
		for (int i = 0; i < 3; ++i) {
			var go = buttonTransforms[i].transform;
			Vector3 offset = offsets[i];
			Vector3 endValue1 = startPos[i] + offset;
			Vector3 endValue2 = endValue1 - 0.1f * offset;
			Sequence sequence = DOTween.Sequence();
			sequence.SetDelay(i * 0.15f);
			sequence.Append(go.DOMove(endValue1, 0.4f));
			go.DOScale(buttonScale, 0.2f);
			sequence.Append(go.DOMove(endValue2, 0.2f));
		}
	}

	public void ShowAll() {
		rootShowing = true;
		rootButton.DOFade(1, 0.4f);
		ShowButtons();
	}

	public void HideButtons() {
		buttonsShowing = false;
		rootShowing = true;
		UpdateAlphas();
		for (int i = 0; i < 3; ++i) {
			var go = buttonTransforms[i].transform;
			go.DOMove(rootTransform.position, 0.3f);
			go.DOScale(rootScale, 0.2f);
		}
	}

	public void HideAll() {
		rootShowing = false;
		rootButton.DOFade(0, 0.4f);
		HideButtons();
	}

	private void UpdateAlphas() {
		float time = 0.2f;
		float rootAlpha = 0, buttonAlpha = 0;
		if (rootShowing) {
			rootAlpha = 1;
		}
		if (buttonsShowing) {
			buttonAlpha = 1;
		}
		Sequence sequence = DOTween.Sequence();
		for (int i = 0; i < buttons.Length; ++i) {
			sequence.Insert(i * time, buttons[i].DOFade(buttonAlpha, time));
			if (i == buttons.Length - 1) {
				sequence.Insert((i + 1) * time, rootButton.DOFade(rootAlpha, time));
			}
		}
		foreach (var button in buttons) {
			button.raycastTarget = buttonsShowing;
		}
		rootButton.raycastTarget = rootShowing;

	}


}
