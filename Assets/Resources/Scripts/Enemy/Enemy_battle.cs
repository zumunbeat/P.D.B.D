using static Interface;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

public class Enemy_battle : BaseCharacter
{
    private string skillpath = "JsonFiles/EnemySkill";
    private string statpath = "JsonFiles/EnemyStat";
    public Stat enemystat;
    [SerializeField]
    private BattleManager battlemanager;
    [SerializeField]
    private BaseCharacter player;
    [SerializeField]
    private List<Colleague_battle> colleagues;
    private List<BaseCharacter> playercamps = new List<BaseCharacter>();
    public void Init()
    {
        player= GameObject.Find("Player").GetComponent<BaseCharacter>();
        colleagues = new List<Colleague_battle>(GameObject.Find("Colleagues").GetComponentsInChildren<Colleague_battle>());
        skills = LoadData<SkillArr>(skillpath);
        enemystat = LoadData<Stat>(statpath);
        playercamps.Add(player);
        playercamps.AddRange(colleagues);
    }
    public void Update()
    {
    }
    public T LoadData<T>(string path)
    {
        return JsonManager.LoadJson<T>(path);
    }

    public void SaveData<T>(T data, string path)
    {
        JsonManager.SaveJson(data, path);
    }
    public void EnemyAI()
    {
        battlemanager.SelectSkill(ThisTurnEnemyDecision(this), this);
        battlemanager.SelectTarget(Randomtarget());
    }
    public override void UseSkill(Skill skill, BaseCharacter target)
    {
        Debug.Log("Enemy used skill: " + skill.name+ " to " +target.name);
        // 스킬 사용 로직
    }
    public Skill ThisTurnEnemyDecision(Enemy_battle obj)
    {
        //랜덤한 적의 스킬을 선택해서 해당정보를 넘겨주자.
        int decision = UnityEngine.Random.Range(0, obj.GetLength());
        
        return obj.GetSkill(decision);
    }
    public BaseCharacter Randomtarget()
    {
        int rand = Random.Range(0, playercamps.Count);
        return playercamps[rand];  
    }
    public override void ActivateSkillButtons()
    {
    }
    
}