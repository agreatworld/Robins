# 子UI的IPointerClick方法不调用

父UI脚本实现了IpointerClick/Up/DownHandler，而子UI脚本只实现了IPointerClickHandler

子UI脚本中的Click检测方法会覆盖父UI脚本中的Click检测方法，但是由于没有重写Up/Down检测方法，所以并未对子UI进行鼠标Up/Down检测，所以对子UI的Click事件没有被检测到，但是仍然会覆盖父UI脚本中的Click方法。