using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Build;
using UnityEngine;
using static Interface;

public abstract class BaseCharacter : MonoBehaviour, ICharacter
{
    public SkillArr skills; // ĳ������ ��ų ���
    public Stat stat;
    public int energy; // ĳ������ ������
    public int hp;

    private void Start()
    {
        hp = stat.health;
    }
    public abstract void UseSkill(Skill skill,BaseCharacter target);

    public Skill GetSkill(int index) 
    {
        if (index >= 0 && index < skills.skills.Length)
        {
            return skills.skills[index];
        }
        else
        {
            Debug.LogError("Skill index out of range.");
            return null;
        }
    }
    public int gethp()
    {
        return hp;
    }
    public void sethp(int damage)
    {
        hp -= damage;
    }
    public int GetLength()
    {
        return skills.skills.Length;
    }
    public abstract void ActivateSkillButtons();
}
