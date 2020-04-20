using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aves : MonoBehaviour {

	/// <summary>
	/// 每一个实例赋予一个唯一标识，用来标注小鸟的父母以支持交配环节的逻辑处理
	/// </summary>
	public static int Index {
		private set; get;
	}

	/// <summary>
	/// 性别
	/// </summary>
	public bool isMale {
		private set; get;
	}

	/// <summary>
	/// 鸟类是否成熟，默认为成熟的，通过交配孵出的小鸟是不成熟的，但是随着时间推移会成熟
	/// </summary>
	public bool isMature {
		get; set;
	} = true;

	/// <summary>
	/// 是否由其他鸟养育而来
	/// </summary>
	public bool isFromCopulation {
		private set; get;
	} = false;

	/// <summary>
	/// 父母的标识号，若非养育型鸟类则记为0，标识符从1开始
	/// </summary>
	public int[] parentsIndex = new int[2];

	/// <summary>
	/// 标识符
	/// </summary>
	public int ID {
		private set; get;
	}

	private void Awake() {
		ID = ++Index;
		var nameDetails = transform.name.Split('-');
		isMale = nameDetails[1] == "雄(Clone)";
		if (nameDetails[1] == "雄(Clone)") {
			isMale = true;
		} else if (nameDetails[1] == "雌(Clone)") {
			isMale = false;
		} else {
			InitForAvesBaby();
		}
	}

	private void InitForAvesBaby() {
		Debug.Log("小鸟初始化");

		// 性别
		if (Random.Range(0, 100) < 50) {
			isMale = true;
			Debug.Log("这只宝宝是雄的");
		} else {
			isMale = false;
			Debug.Log("这只宝宝是雌的");
		}

		// 成熟性
		isMature = false;

		// 非野生型
		isFromCopulation = true;

		// 父母标识已在外部赋值

		// 更换sprite
		Debug.LogError("暂未处理小鸟宝宝的sprite，这与小鸟性别有关");
		
	}
}
