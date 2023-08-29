unity的xml读取存储方案

MapEditor里面是一个简单的示例，其功能为保存并读取GameManager/XmlIngameEditor/gameObjects中指定对象的指定信息
    通过更改XmlIngameEditor的loader变量(类型为RuntimeXmlLoader)，可以实现不同信息的存储和格式。具体通过继承RuntimeXmlLoader覆写SaveEntry和LoadEntry实现。场景中的两个示例为：
        为XYPositionYRotationTemplate时，存储并读取x和z轴位置和y轴旋转信息至FormationPreset。
        为XYZPositionTemplate时，存储并读取x，y，z轴位置信息至FormationPreset2，无视旋转信息。