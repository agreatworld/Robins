using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameMechanics {

	/// <summary>
	/// 回合控制器提供的功能按照回合开始、回合进行、回合结束三个节点分类
	/// 回合开始：准备阶段、判定阶段、触发阶段
	/// 回合进行是由玩家自由操作的时间，不需要为这个阶段提供方法
	/// 回合结束：清算阶段
	/// 回合结束之后刷新回合的状态、季节等信息
	/// 只开放一个root method供外部调用
	/// </summary>
	public class RoundsController : MonoBehaviour {

		#region 数据集合
		public enum Season {
			Spring,
			Summer,
			Autumn,
			Winter
		};
		#endregion

		#region field
		public static RoundsController Instance;

		/// <summary>
		/// 回合数，每个季节3回合，共4季节12回合
		/// </summary>
		public int round;

		/// <summary>
		/// 当前季节
		/// </summary>
		public Season currentSeason;

		#endregion

		#region Mono
		private void Awake() {
			// 字段初始化
			Instance = this;
			round = 1; // 从第1回合开始
			currentSeason = Season.Spring; // 从春季开始
		}
		#endregion


		/// <summary>
		/// 1. 结束当前回合
		/// 2. 更新回合状态信息
		/// 2. 开始下一回合
		/// </summary>
		public void EnterNextRound() {
			RoundsOver();
			UpdateRoundStatus();
			RoundsBegin();
		}

		#region 回合状态

		/// <summary>
		/// 更新回合状态
		/// 1. 回合数迭代
		/// 2. 季节相关判定
		/// </summary>
		private void UpdateRoundStatus() {
			UpdateRounds();
			UpdateSeasonsAndClimates();
		}

		/// <summary>
		/// 更新回合信息
		/// </summary>
		private void UpdateRounds() {

		}

		/// <summary>
		/// 更新季节气候信息
		/// </summary>
		private void UpdateSeasonsAndClimates() {

		}

		#endregion

		#region 回合开始

		/// <summary>
		/// 1. 准备阶段
		/// 2. 判定阶段
		/// 3. 触发阶段
		/// </summary>
		private void RoundsBegin() {
			PrepareStage();
			CheckStage();
			ReactStage();
		}

		/// <summary>
		/// 准备阶段
		/// 1. 补满行动次数
		/// 2. 现存的鸟出产羽毛
		/// 3. 商店物品刷新
		/// </summary>
		private void PrepareStage() {

		}

		/// <summary>
		/// 判定阶段
		/// 1. 树木是否培育成功
		/// 2. 新鸟入住判定
		/// 3. 树的状态（可能根据季节更替有轻微变化，但这part加进去概率较小）
		/// </summary>
		private void CheckStage() {

		}


		/// <summary>
		/// 触发阶段，根据判定阶段的结果作出一系列反馈
		/// 1. 新入住鸟类相关的剧情动画
		/// </summary>
		private void ReactStage() {

		}

		#endregion

		#region 回合结束

		/// <summary>
		/// 1. 清算阶段
		/// </summary>
		private void RoundsOver() {
			ConclusionStage();
		}

		/// <summary>
		/// 清算阶段
		/// 1. 自动拾取未拾取的羽毛
		/// </summary>
		private void ConclusionStage() {

		}

		#endregion
	}
}

