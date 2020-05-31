using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class AvesPlotTriggerHandler : MonoBehaviour {
	private class PlotInfo {
		public bool isFirst;
		public bool firstMale;
		public bool shouldCheckForSecond;
		public bool firstChild;
		public bool child2Mature;
		public PlotInfo() {
			isFirst = true;
			firstMale = false;
			shouldCheckForSecond = false;
			firstChild = true;
			child2Mature = true;
		}
	}

	private Dictionary<string, PlotInfo> showUpInfoDic = new Dictionary<string, PlotInfo>();

	public static AvesPlotTriggerHandler Instance {
		private set; get;
	}

	private void Awake() {
		Instance = this;
		showUpInfoDic.Add("山麻雀", new PlotInfo());
		showUpInfoDic.Add("池鹭", new PlotInfo());
		showUpInfoDic.Add("大鵟", new PlotInfo());
		showUpInfoDic.Add("褐冠鹃隼", new PlotInfo());
		showUpInfoDic.Add("黑冠鹃隼", new PlotInfo());
		showUpInfoDic.Add("白琵鹭", new PlotInfo());
		showUpInfoDic.Add("金雕", new PlotInfo());
		showUpInfoDic.Add("知更鸟", new PlotInfo());
		showUpInfoDic.Add("东方白鹳", new PlotInfo());
	}

	/// <summary>
	/// 检测指定鸟类是否是第一次出现，并更新值
	/// </summary>
	/// <param name="name">
	/// 鸟类名称
	/// </param>
	/// <returns>
	/// 返回剧本编号，-1表示不触发剧情
	/// </returns>
	public int CheckAvesAndUpdateInfo(string name, bool isMale, bool isChild) {
		PlotInfo info = showUpInfoDic[name];
		if (isChild && info.firstChild) {
			info.firstChild = false;
			return 2;
		}
		if (info.isFirst) {
			info.isFirst = false;
			info.firstMale = isMale;
			info.shouldCheckForSecond = true;
			return 0;
		} else {
			if (info.shouldCheckForSecond) {
				if (isMale == info.firstMale) {
					return -1;
				} else {
					info.shouldCheckForSecond = false;
					return 1;
				}
				
			}
		}
		return -1;
	}

}
