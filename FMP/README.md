unity的xml读取存储方案

MapEditor里面是一个简单的示例，其功能为保存并读取GameManager/XmlIngameEditor/gameObjects中指定的场景元素的位置和旋转信息
    通过更改XmlIngameEditor的loader变量(类型为RuntimeXmlLoader)，可以实现不同信息的存储格式。具体通过继承RuntimeXmlLoader覆写SaveEntry和LoadEntry实现。
        为RuntimeXmlLoader时，存储并读取x和z轴位置和y轴旋转信息
        为Template1时，存储并读取x，y，z轴位置信息，无视旋转信息。