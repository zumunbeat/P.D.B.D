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
    [SerializeField]
    public GameObject TargetButtonParent;

    void Start()
    {
        SkillButtonParent = GetComponentInParent<SkillButtonParent>().gameObject;
        // 스킬 이름으로 버튼의 텍스트를 설정
        buttonText = GetComponentInChildren<TMP_Text>();

        if (self.skills != null && self.GetLength() > index)
        {
            buttonText.text = self.GetSkill(index).name;
            Debug.Log(self.GetLength());
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
        TargetButtonParent.SetActive(true);
        SkillButtonParent.SetActive(false);
        // 선택된 스킬을 BattleManager에 전달
        battleManager.OnSkillButtonClick(self.GetSkill(index), self);
    }
}