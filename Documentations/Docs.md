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

## 子地块及其动画、UI交互

### 主要功能说明

- 鼠标悬浮在子地块上时，相应子地块换材质
- 点击子地块出现三个功能按钮及一个悬浮按钮
- 点击悬浮按钮收放功能按钮，此时只显示悬浮按钮
- 点击非本地块区域收回所有按钮

### Hierarchy结构

* RootMap
  * SubMaps
    * 0(0~9)	*采取单个数字作为名字便于脚本逻辑实现，共10个地块*
      * Canvas	*Render Mode: World Space*
        * MapButtonsHolder
          * SeedButton
          * FeedButton
          * WaterButton

### 脚本挂载情况

- RootMap
  - MapUIController
    - 单例模式，控制全局的地块交互，内含多个控制UI显示、隐藏的方法，这些方法只是更新该脚本中的控制变量和对相应按钮组上挂载的脚本中的方法进行的简单调用。其他脚本中大部分对UI的控制会调用这些公有方法，除了真正实现控制UI的脚本
    - 结构体`SubMapInfo`中涵括了控制UI所需的全部信息，并且声明了一个该结构体的数组，数组大小与子地块数一致，这些信息在`Awake()`中被加载，涉及到`transform.Find()`和`GetComponent()`，加载的信息总量略微庞大，可能会造成加载卡界面
    - `Update()`中执行鼠标点击检测以及鼠标射线检测，并处理点击对象对地图当前状态的影响
- 0(0~9)
  - MouseEvents
    - 公开更换子地块材质的方法，在MapUIController控制地图状态时会调用到，这个脚本也是`SubMapInfo`的其中之一
    - 鼠标移入、悬浮的检测是在生命周期`OnMousexxx`中实现的，需要加入碰撞器
  - SubMapDifferentiation
    - 定义了地块可变化的地貌，共四种枚举：Lake, Mountain, Forest, Bushveld
- MapButtonsHolder
  - MapButtonsHolderEvents
    - 更新鼠标移入移出悬浮按钮时的相关状态变量，最终目的是实现鼠标在悬浮按钮中时的点击会触发功能按钮的回收，在悬浮按钮外的点击会触发所有按钮的回收
  - MapUIAnimation
    - 控制各按钮显示与隐藏的动画效果，公开了一个Vector3数组(Offsets)便于定义按钮动画的偏移量
    - 涉及到的字段初始化也比较多，但没有MapUIController中多
- Seed/Feed/Water Button
  - MapSeed/MapFeed/MapWater Button
    - 三个脚本分别定义了三个功能按钮的点击事件

### 注意

1. 悬浮按钮要放在相应的子地块范围内，因为这同时涉及到对UI和GameObject的点击事件，它们实现的方式是不同的。如果把悬浮按钮放在相应子地块范围外，点击时会被认为点击在了悬浮按钮和非本子地块区域，收回功能按钮和收回所有按钮都会生效，最终表现出收回所有按钮。

## 对话框

### 逻辑设计

`DialogManager`控制文本流程的交互、推进等

`ContentManager`控制整个UI组的显示，并且对外提供修改的方法，采取单例模式