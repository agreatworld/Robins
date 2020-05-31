using UnityEngine;

public class PlotTrigger : MonoBehaviour {

	private string plotPath;

	private void Awake() {
		gameObject.SetActive(false);
	}

	private void OnMouseUpAsButton() {
		LoadPlot();
		gameObject.SetActive(false);
	}

	public void SetPlotPath(string plotPath) {
		this.plotPath = plotPath;
	}

	private void LoadPlot() {
		DialogueManager.Instance.LoadDialogue(plotPath, null);
	}
}
