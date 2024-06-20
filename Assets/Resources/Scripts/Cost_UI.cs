using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Cost_UI : MonoBehaviour
{
    TMP_Text Cost_text;
    int cost = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cost_text = GetComponent<TMP_Text>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCost(int cost)
    {
        this.cost = cost;
        Cost_text.text = cost.ToString();
    }
    public int GetCost()
    {
        return cost;
    }
    void UseCost(int price)
    {
        cost -= price;
        Cost_text.text = cost.ToString();
    }
}
