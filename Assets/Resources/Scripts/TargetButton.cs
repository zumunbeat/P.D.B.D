using UnityEngine;
using UnityEngine.UI;
using static Interface;

public class TargetButton : MonoBehaviour
{
    public BattleManager battleManager;
    public BaseCharacter target;
    private GameObject targetButtonParent; // Ÿ�� ��ư���� �θ� ������Ʈ

    void Start()
    {
        targetButtonParent = GetComponentInParent<TargetButtonParent>().gameObject;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClickTargetButton);
    }

    void OnClickTargetButton()
    {
        // Ÿ�� ��ư�� Ŭ���ϸ� BattleManager�� Ÿ�� ���� ����
        battleManager.OnTargetButtonClick(target);
        
        targetButtonParent.SetActive(false);
    }
}