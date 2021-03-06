using System;
using UnityEngine;

// ****************************************************************
// 功能：Map的入口类
// 创建：蔡泽深
// 时间：2017/06/05
// 修改内容：										修改者姓名：
// ****************************************************************

public class Spawner : View {
    private Map map = null;
    private Luobo luobo;
    private GameModel gameModel;
    private RoundModel roundModel;

    public override string Name {
        get {
            return Consts.V_Spawner;
        }
    }

    public override void RegisterEvents() {
        attentionEvnets.Add(Consts.E_SpawnMonster);
        attentionEvnets.Add(Consts.E_EnterScene);
        attentionEvnets.Add(Consts.E_SpawnTower);
    }

    public override void HandleEvent(string eventName, object args) {
        switch (eventName) {
            case Consts.E_EnterScene:
                SceneArgs sceneArgs = args as SceneArgs;
                if (sceneArgs.Name == Consts.Level) {
                    // 获取数据
                    gameModel = GetModel<GameModel>();
                    roundModel = GetModel<RoundModel>();

                    // 加载地图
                    map = GetComponent<Map>();
                    map.tileClickEvent += OnMapTileClick;
                    map.LoadLevel(gameModel.PlayLevel);

                    // 加载萝卜
                    Vector3[] path = map.Path;
                    SpawnLuobo(path[path.Length - 1]);
                }
                break;
            case Consts.E_SpawnMonster:
                SpawnMonsterArgs smArgs = args as SpawnMonsterArgs;
                SpawnMonster(smArgs.monsterType);
                break;
            case Consts.E_SpawnTower:
                SpawnTowerArgs stArgs = args as SpawnTowerArgs;
                SpawnTower(stArgs.towerID, stArgs.pos);
                break;
        }
    }

    // 地图格子被点击
    private void OnMapTileClick(object sender, TileClickEvnetArgs e) {
        MapTileClickArgs mTArgs = new MapTileClickArgs { map = sender as Map, tile = e.tile, mouseButton = e.mouseButton };
        SendEvent(Consts.E_MapTileClick, mTArgs);
    }

    // 怪物到达终点
    private void OnMonsterReached(Monster obj) {
        // 萝卜掉血
        luobo.Damage(1);

        UnSpawnMonster(obj.gameObject);
    }

    // 怪物血量改变
    private void OnMonsterHPChanged(int hp, int maxHP) {

    }

    // 怪物死亡
    private void OnMonsterDied(Role obj) {
        // 加钱
        Monster monster = obj as Monster;
        gameModel.Gold += monster.Gold;

        UnSpawnMonster(obj.gameObject);
    }

    // 萝卜死亡
    private void OnLuoboDied(Role obj) {
        SendEvent(Consts.E_EndLevel, new EndLevelArgs { LevelID = gameModel.CurrentLevelIndex, IsWin = false });
    }

    // 生产怪物
    private void SpawnMonster(int monsterType) {
        // 构建预设体名
        string prefabName = "Monster" + monsterType.ToString("D3");

        // 从对象池取出怪物
        GameObject go = Game.Instance.ObjectPool.Spawn(prefabName);

        Monster monster = go.GetComponent<Monster>();
        monster.readched += OnMonsterReached;
        monster.hpChanged += OnMonsterHPChanged;
        monster.died += OnMonsterDied;
        monster.LoadRoadPath(map.Path);
    }

    // 生产塔
    private void SpawnTower(int towerID, Vector3 pos) {
        // 生成塔
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerID);
        GameObject go = Game.Instance.ObjectPool.Spawn(info.prefabName);

        // 设置塔信息
        Tower tower = go.GetComponent<Tower>();
        Tile tile = map.GetTile(pos);
        tower.transform.position = pos;
        tower.Load(towerID, tile,map.MapRect);
        
        // 消耗金币(写这里感觉不对,应该在控制器写吧)
        gameModel.Gold -= tower.BasePrice;
    }

    // 生产萝卜
    private void SpawnLuobo(Vector3 pos) {
        GameObject go = Game.Instance.ObjectPool.Spawn("Luobo");
        luobo = go.GetComponent<Luobo>();
        luobo.transform.position = pos;
        luobo.died += OnLuoboDied;
    }

    // 回收怪物
    private void UnSpawnMonster(GameObject go) {
        Game.Instance.ObjectPool.Unspawn(go);

        // 判断是否胜利
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        if (!luobo.IsDead && roundModel.AllRoundsComplete && monsters.Length <= 0) {
            SendEvent(Consts.E_EndLevel, new EndLevelArgs { LevelID = gameModel.CurrentLevelIndex, IsWin = true });
        }
    }
}

