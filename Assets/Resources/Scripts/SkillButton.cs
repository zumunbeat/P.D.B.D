using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : MonoBehaviour
{
    public BattleManager battleManager;
    public BaseCharacter self;
    public int index;
    private TMP_Text buttonText;
    [SerializeField]
    public GameObject SkillButtonParent;
    

    void Awake()
    {
        SkillButtonParent = GetComponentInParent<SkillButtonParent>().gameObject;
        // ��ų �̸����� ��ư�� �ؽ�Ʈ�� ����
        buttonText = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        if (self.skills != null && self.GetLength() > index)
        {
            buttonText.text = self.GetSkill(index).name;
        }
        else
        {
            buttonText.text = "NULL";
        }
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClickSkillButton);
    }

    void OnClickSkillButton()
    { 
        SkillButtonParent.SetActive(false);
        // ���õ� ��ų�� BattleManager�� ����
        battleManager.OnSkillButtonClick(self.GetSkill(index), self);
    }
}