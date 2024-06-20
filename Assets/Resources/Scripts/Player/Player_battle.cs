using UnityEngine;
using static Interface;

public class Player_battle : BaseCharacter
{
    private string skillpath = "JsonFiles/PlayerSkill";
    private string statpath = "JsonFiles/PlayerStat";
    public Stat playerstat;
    [SerializeField]
    private GameObject playerSkillButtonParent;
    
    void Awake()
    {
        skills = LoadData<SkillArr>(skillpath);
        playerstat = LoadData<Stat>(statpath);
        
    }

    void Update()
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

    public override void UseSkill(Skill skill)
    {
        Debug.Log("Player used skill: " + skill.name);
        // 스킬 사용 로직
    }

    public override void ActivateSkillButtons()
    {
        playerSkillButtonParent.SetActive(true);
    }
}