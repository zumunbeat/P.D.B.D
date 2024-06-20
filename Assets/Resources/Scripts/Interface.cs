using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public interface ICharacter
    {
        void UseSkill(Skill skill,BaseCharacter target);
        Skill GetSkill(int index);
    }
}
