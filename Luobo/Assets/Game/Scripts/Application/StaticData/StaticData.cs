using System.Collections.Generic;
using System;

// ****************************************************************
// 功能：静态数据类 (在实际工作中这个类是不用自己写的,是通过工具,从策划写好的Excel表装换而成的,网络协议也是用工具生成代码的)
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class StaticData : Singleton<StaticData> {
    private Dictionary<int, MonsterInfo> monsterInfos = new Dictionary<int, MonsterInfo>();
    private Dictionary<int, LuoboInfo> luoboInfos = new Dictionary<int, LuoboInfo>();
    private Dictionary<int, TowerInfo> towerInfos = new Dictionary<int, TowerInfo>();
    private Dictionary<int, BulletInfo> bulletInfos = new Dictionary<int, BulletInfo>();

    public StaticData() {
        InitMonsterInfos();
        InitTowerInfos();
        InitBulletInfos();
        InitLuoboInfos();
    }

    private void InitMonsterInfos() {
        monsterInfos.Add(0, new MonsterInfo { id = 0, hp = 3, moveSpeed = 1.5f, gold = 1 });
        monsterInfos.Add(1, new MonsterInfo { id = 1, hp = 5, moveSpeed = 1.5f, gold = 1 });
        monsterInfos.Add(2, new MonsterInfo { id = 2, hp = 7, moveSpeed = 1.5f, gold = 1 });
        monsterInfos.Add(3, new MonsterInfo { id = 3, hp = 9, moveSpeed = 1.5f, gold = 1 });
        monsterInfos.Add(4, new MonsterInfo { id = 4, hp = 11, moveSpeed = 1.5f, gold = 1 });
        monsterInfos.Add(5, new MonsterInfo { id = 5, hp = 13, moveSpeed = 1.5f, gold = 1 });

        monsterInfos.Add(101, new MonsterInfo { id = 101, hp = 20, moveSpeed = 1f, gold = 2 });
        monsterInfos.Add(102, new MonsterInfo { id = 102, hp = 22, moveSpeed = 1f, gold = 2 });
        monsterInfos.Add(103, new MonsterInfo { id = 103, hp = 24, moveSpeed = 1f, gold = 2 });
        monsterInfos.Add(104, new MonsterInfo { id = 104, hp = 26, moveSpeed = 1f, gold = 2 });
        monsterInfos.Add(105, new MonsterInfo { id = 105, hp = 28, moveSpeed = 1f, gold = 2 });

        monsterInfos.Add(200, new MonsterInfo { id = 200, hp = 40, moveSpeed = 0.8f, gold = 3 });
    }

    private void InitLuoboInfos() {
        luoboInfos.Add(0, new LuoboInfo { id = 0, hp = 10 });
    }

    private void InitTowerInfos() {
        towerInfos.Add(0, new TowerInfo { id = 0, prefabName = "Bottle", normalIcon = "Bottle01.png", disableIcon = "Bottle00.png", maxLevel = 3, basePrice = 5, shotRate = 2, guardRange = 1.2f, useBulletID = 0 });
        towerInfos.Add(1, new TowerInfo { id = 1, prefabName = "Fan", normalIcon = "Fan01.png", disableIcon = "Fan00.png", maxLevel = 3, basePrice = 10, shotRate = 1, guardRange = 1.5f, useBulletID = 1 });
    }

    private void InitBulletInfos() {
        bulletInfos.Add(0, new BulletInfo { id = 0, prefabName = "BottleBullet", baseSpeed = 5f, baseAttack = 1 });
        bulletInfos.Add(1, new BulletInfo { id = 1, prefabName = "FanBullet", baseSpeed = 3f, baseAttack = 1 });
    }

    public MonsterInfo GetMonsterInfo(int monsterType) {
        if (!monsterInfos.ContainsKey(monsterType)) {
            throw new Exception("没找到指定怪物的静态数据: " + monsterType);
        }
        return monsterInfos[monsterType];
    }

    public LuoboInfo GetLuoboInfo() {
        return luoboInfos[0];
    }

    public TowerInfo GetTowerInfo(int id) {
        if (!towerInfos.ContainsKey(id)) {
            throw new Exception("没找到指定塔的静态数据: " + id);
        }
        return towerInfos[id];
    }

    public BulletInfo GetBulletInfo(int id) {
        if (!bulletInfos.ContainsKey(id)) {
            throw new Exception("没找到指定子弹的静态数据: " + id);
        }
        return bulletInfos[id];
    }
}

