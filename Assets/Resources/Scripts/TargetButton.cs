using UnityEngine;
using UnityEngine.UI;
using static Interface;

public class TargetButton : MonoBehaviour
{
    public BattleManager battleManager;
    public BaseCharacter target;
    private GameObject targetButtonParent; // 타겟 버튼들의 부모 오브젝트

    void Start()
    {
        targetButtonParent = GetComponentInParent<TargetButtonParent>().gameObject;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClickTargetButton);
    }

    void OnClickTargetButton()
    {
        // 타겟 버튼을 클릭하면 BattleManager에 타겟 정보 전달
        battleManager.OnTargetButtonClick(target);
        
        targetButtonParent.SetActive(false);
    }
}