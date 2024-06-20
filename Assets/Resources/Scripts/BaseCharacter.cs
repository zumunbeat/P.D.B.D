using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using static Interface;

public abstract class BaseCharacter : MonoBehaviour, ICharacter
{
    public SkillArr skills; // 캐릭터의 스킬 목록
    public int energy; // 캐릭터의 에너지
    

    public abstract void UseSkill(Skill skill);

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
    public int GetLength()
    {
        return skills.skills.Length;
    }
    public abstract void ActivateSkillButtons();
}
