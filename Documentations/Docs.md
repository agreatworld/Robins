# 开发文档

## 背包

背包采取MVP结构，分别为`BagModel`、`BagView`、`BagPresenter`。三者均挂载于`BagPanel`，这是一个作为背包界面根节点的GameObject（Panel）。出于数据初始化的需要，在ProjectSetting中将三者(`void Awake()`)运行的顺序设置为BagModel->BagPresenter->BagView。Model层与View层之间的数据同步在BagPresenter的`void Start()`方法中进行。

MVP均采用单例模式，但是控制在`Bag`命名空间中，所有的字段赋值均via script，杜绝公开给编辑器后拖拽赋值。

在编辑器中，对Scroll View区域采用Grid Layout，每行限制4列。编辑器中所有的UI命名规范为TypeName，比如背景图片ImageBackground、种子选项卡ImageSeedTab，以便于区分。

在脚本中，可能出现多个脚本中有类似的字段，采取后缀进行区分。例如BagModel和BagView中都有关于卡牌的字典，前者是cardsModel，后者是cardsView，它们的键值对类型不完全一致。

## 游戏机制框架

以玩家点击回合结束按钮为界限，划分回合开始、进行、结束三个阶段。

主体的顺序为：
1. 结束当前回合
   1. 清算阶段
      1. 自动拾取未拾取的羽毛
2. 更新回合状态信息
   1. 更新回合信息
   2. 更新季节气候信息
3. 开始下一回合
   1. 准备阶段
      1. 补满行动次数
      2. 现存的鸟产出羽毛
      3. 商店物品刷新
   2. 判定阶段
      1. 树木是否培育成功
      2. 新鸟入住判定
      3. 树的状态变化（可能根据季节更替有轻微变化，但目前这part加进去概率较小）
   3. 触发阶段
      1. 新入住鸟类相关剧情动画


