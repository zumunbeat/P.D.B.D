using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Interface;
public class SelectedSkill
{
    public BaseCharacter self;
    public Skill skill;
    public BaseCharacter target;

    public SelectedSkill(BaseCharacter self, Skill skill, BaseCharacter target)
    {
        this.self = self;
        this.skill = skill;
        this.target = target;
    }
}
public enum BattleState
{
    PlayerTurn,
    ColleagueTurn,
    Execute,
    EnemyTurn,
    ResetTurn,
}

public class BattleManager : MonoBehaviour
{
    private int cost;
    private int turn;
    [SerializeField]
    private Cost_UI costUI;
    [SerializeField]    
    private ExecuteButton  executeButton;
    public static BattleManager Instance;
    private Player_battle player;
    private List<Colleague_battle> colleagues;
    private List<Enemy_battle> enemies;
    private List<SelectedSkill> selectedSkills = new List<SelectedSkill>();
    private Skill selectedSkill;
    private BaseCharacter selectCharacter;
    private BaseCharacter selectedCharacter;
    [SerializeField]
    private GameObject TargetButtonParent;
    
    private BattleState state;
    private bool isStateInitialized = false;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player_battle>();
        if (player == null)
        {
            Debug.LogError("Player_battle component not found!");
            return;
        }

        colleagues = new List<Colleague_battle>(GameObject.Find("Colleagues").GetComponentsInChildren<Colleague_battle>());
        if (colleagues.Count == 0)
        {
            Debug.LogError("No Colleague_battle components found!");
            return;
        }

        enemies = new List<Enemy_battle>(GameObject.Find("Enemies").GetComponentsInChildren<Enemy_battle>());
        if (enemies == null)
        {
            Debug.LogError("Enemies_battle component not found!");
            return;
        }
        state = BattleState.PlayerTurn;

    }
    private void Update()
    {
        //state machine을 통해 턴구조
        switch (state)
        {
            case BattleState.PlayerTurn:
                Debug.Log("PlayerTurn");
                if (!isStateInitialized)
                {
                    
                    RoleDice();
                    player.ActivateSkillButtons();
                    isStateInitialized = true;
                }
                break;
            case BattleState.ColleagueTurn:
                if (!isStateInitialized)
                {
                    Debug.Log("ColleagueTurn");
                    foreach (Colleague_battle colleague in colleagues)
                    {
                        colleague.ActivateSkillButtons();
                    }
                    isStateInitialized = true;
                }

                break;
            case BattleState.Execute:
                if (!isStateInitialized)
                {
                    executeButton.gameObject.SetActive(true);
                }
                isStateInitialized = true;
                break;
            case BattleState.EnemyTurn:
                if (!isStateInitialized)
                {
                    Debug.Log("EnemyTurn");
                    foreach (Enemy_battle enemy in enemies)
                    {
                        enemy.EnemyAI();
                    }
                    isStateInitialized = true;
                    ExecuteSelectedSkills();
                }
                break;
        }

    }
    
    public void SelectSkill(Skill skill,BaseCharacter self)
    {
        if(costUI.GetCost() - skill.cost <0)
        {
            Debug.Log("Cost가 부족합니다!");
            StartTargetSelection(false);
        }
        selectedSkill = skill;
        selectCharacter = self;
        costUI.SetCost(costUI.GetCost() - skill.cost);
        // 스킬 선택 후 바로 타겟 선택 메서드 호출
        StartTargetSelection(true);
    }

    public void SelectTarget(BaseCharacter target)
    {
        if (selectedSkill != null&&selectCharacter != null)
        {
            selectedSkills.Add(new SelectedSkill(selectCharacter,selectedSkill, target));
            
            selectedSkill = null;
            selectCharacter = null;
            switchstate();
        }
        else
        {
            Debug.LogError("No skill selected!");
        }
        return;
    }

    public void ExecuteSelectedSkills()
    {
        foreach (SelectedSkill selectedSkill in selectedSkills)
        {
            
            selectedSkill.self.UseSkill(selectedSkill.skill,selectedSkill.target);
        }
        selectedSkills.Clear(); // 선택된 스킬 목록 초기화
        switchstate();
    }

    public void OnSkillButtonClick(Skill skill,BaseCharacter self)
    {
        SelectSkill(skill,self);
    }

    public void StartTargetSelection(bool Isremain)
    {
        if (Isremain == true)
        {
            TargetButtonParent.SetActive(true);
        }
        else
        {
            ExecuteButtonActive();
        }
    }
    public void OnTargetButtonClick(BaseCharacter target)
    {
        SelectTarget(target);
    }
    public void RoleDice()
    {
        cost = Random.Range(3, 7);
        costUI.gameObject.SetActive(true);
        costUI.SetCost(cost);
    }
    public void ExecuteButtonActive()
    {
        executeButton.gameObject.SetActive(true);
    }
    public void switchstate( )
    {
        if (isActiveAndEnabled != true) return;
        // 다음 상태로 전환
        isStateInitialized = false;
        switch (state)
        {
            case BattleState.PlayerTurn:
                state = BattleState.ColleagueTurn;
                break;
            case BattleState.ColleagueTurn:
                state = BattleState.Execute;
                break;
            case BattleState.Execute:
                state = BattleState.EnemyTurn;
                break;
            case BattleState.EnemyTurn:
                state = BattleState.PlayerTurn;
                break;
            
        } // 상태가 변경되었음을 표시
        Debug.Log("상태전환");
    }
    public BaseCharacter getplayer()
    {
        return player;
    }
    public List<Colleague_battle> getColleagues()
    {
        return colleagues;
    }
}