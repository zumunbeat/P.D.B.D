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
    EnemyTurn
}

public class BattleManager : MonoBehaviour
{
    private int cost;
    private int turn;
    [SerializeField]
    public Cost_UI costUI;
    public ExecuteButton  executeButton;
    public static BattleManager Instance;
    public Player_battle player;
    public List<Colleague_battle> colleagues;
    public Enemies_battle enemies;
    private List<SelectedSkill> selectedSkills = new List<SelectedSkill>();
    private List<ICharacter> playerCamps = new List<ICharacter>();
    private List<ICharacter> enemyCamps = new List<ICharacter>();
    private Skill selectedSkill;
    private BaseCharacter selectCharacter;
    private BaseCharacter selectedCharacter;

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

        enemies = GameObject.Find("Enemies").GetComponent<Enemies_battle>();
        if (enemies == null)
        {
            Debug.LogError("Enemies_battle component not found!");
            return;
        }

        // Player와 동료를 characters 리스트에 추가
        playerCamps.Add(player);
        playerCamps.AddRange(colleagues);
        enemyCamps.AddRange(enemyCamps);
    }
    private void Update()
    {
        //state machine을 통해 턴구조
        switch (state)
        {
            case BattleState.PlayerTurn:
                if (!isStateInitialized)
                {
                    RoleDice();
                    player.ActivateSkillButtons();
                    isStateInitialized = true;
                }
                // 플레이어의 턴 로직
                break;
            case BattleState.ColleagueTurn:
                if (!isStateInitialized)
                {
                    
                    isStateInitialized = true;
                }
                // 동료 턴 로직
                break;
            case BattleState.EnemyTurn:
                if (!isStateInitialized)
                {
                    
                    isStateInitialized = true;
                }
                // 적 턴 로직
                break;

        }

    }
    public void SelectSkill(Skill skill,BaseCharacter self)
    {
        if(costUI.GetCost() - skill.cost <0)
        {
            Debug.Log("Cost가 부족합니다!");
            return;
        }
        selectedSkill = skill;
        selectCharacter = self;
        costUI.SetCost(costUI.GetCost() - skill.cost);
        Debug.Log("Selected skill: " + skill.name);
        // 스킬 선택 후 바로 타겟 선택 메서드 호출
        StartTargetSelection();
    }

    public void SelectTarget(BaseCharacter target)
    {
        if (selectedSkill != null&&selectCharacter != null)
        {
            selectedSkills.Add(new SelectedSkill(selectCharacter,selectedSkill, target));
            Debug.Log("Selected target: " + target.gameObject.name);
            selectedSkill = null;
            selectCharacter = null;
        }
        else
        {
            Debug.LogError("No skill selected!");
        }
    }

    public void ExecuteSelectedSkills()
    {
        foreach (SelectedSkill selectedSkill in selectedSkills)
        {
            selectedSkill.self.UseSkill(selectedSkill.skill);
        }
        selectedSkills.Clear(); // 선택된 스킬 목록 초기화
        // 다음 상태로 전환
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
        }
        isStateInitialized = false; // 상태가 변경되었음을 표시
    }

    public void OnSkillButtonClick(Skill skill,BaseCharacter self)
    {
        SelectSkill(skill,self);
    }

    public void StartTargetSelection()
    {
        // 타겟 선택 UI를 활성화하거나, 다음 단계로 진행하는 등의 로직 추가
        Debug.Log("Select target for skill: " + selectedSkill.name);
        // 예시: UI를 열어서 타겟 선택을 유도하는 로직 추가 가능
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
    public void WhoFirst(GameObject obj) {
        if (obj.CompareTag("player") == true) state = BattleState.PlayerTurn;
        if (obj.CompareTag("enemy") == true) state = BattleState.EnemyTurn;
        return;
    }
}