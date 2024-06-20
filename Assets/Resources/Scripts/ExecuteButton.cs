using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExecuteButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        
        Button button = GetComponent<Button>();
   
        button.onClick.AddListener(Execute);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Execute()
    {
        BattleManager.Instance.ExecuteSelectedSkills();
    }
}
