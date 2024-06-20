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

        // Player�� ���Ḧ characters ����Ʈ�� �߰�
        playerCamps.Add(player);
        playerCamps.AddRange(colleagues);
        enemyCamps.AddRange(enemyCamps);
    }
    private void Update()
    {
        //state machine�� ���� �ϱ���
        switch (state)
        {
            case BattleState.PlayerTurn:
                if (!isStateInitialized)
                {
                    RoleDice();
                    player.ActivateSkillButtons();
                    isStateInitialized = true;
                }
                // �÷��̾��� �� ����
                break;
            case BattleState.ColleagueTurn:
                if (!isStateInitialized)
                {
                    
                    isStateInitialized = true;
                }
                // ���� �� ����
                break;
            case BattleState.EnemyTurn:
                if (!isStateInitialized)
                {
                    
                    isStateInitialized = true;
                }
                // �� �� ����
                break;

        }

    }
    public void SelectSkill(Skill skill,BaseCharacter self)
    {
        if(costUI.GetCost() - skill.cost <0)
        {
            Debug.Log("Cost�� �����մϴ�!");
            return;
        }
        selectedSkill = skill;
        selectCharacter = self;
        costUI.SetCost(costUI.GetCost() - skill.cost);
        Debug.Log("Selected skill: " + skill.name);
        // ��ų ���� �� �ٷ� Ÿ�� ���� �޼��� ȣ��
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
        selectedSkills.Clear(); // ���õ� ��ų ��� �ʱ�ȭ
        // ���� ���·� ��ȯ
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
        isStateInitialized = false; // ���°� ����Ǿ����� ǥ��
    }

    public void OnSkillButtonClick(Skill skill,BaseCharacter self)
    {
        SelectSkill(skill,self);
    }

    public void StartTargetSelection()
    {
        // Ÿ�� ���� UI�� Ȱ��ȭ�ϰų�, ���� �ܰ�� �����ϴ� ���� ���� �߰�
        Debug.Log("Select target for skill: " + selectedSkill.name);
        // ����: UI�� ��� Ÿ�� ������ �����ϴ� ���� �߰� ����
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