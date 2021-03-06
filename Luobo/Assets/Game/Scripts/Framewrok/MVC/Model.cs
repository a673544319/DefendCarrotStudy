using UnityEngine;

// ****************************************************************
// 功能：MVC框架数据基类
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public abstract class Model  {
    // 模型标识
    public abstract string Name { get; }

    protected void SendEvent(string eventName,object arg = null) {
        MVC.SendEvent(eventName, arg);
    }
}

