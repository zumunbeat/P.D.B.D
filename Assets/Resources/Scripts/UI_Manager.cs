using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Manager : MonoBehaviour
{
    public UIDocument _doc;
    private Button _TestButton;
    private Slider _TestSlider;
    private Toggle _TestToggle;

    void Awake()
    {
        //_doc = GetComponent<UIDocument>();
        _TestButton = _doc.rootVisualElement.Q<Button>("MainButton");
        _TestSlider = _doc.rootVisualElement.Q<Slider>("MainSlide");
        _TestToggle = _doc.rootVisualElement.Q<Toggle>("MainToggle");

        //event ÇÔ¼öÀÓ
        _TestButton.clicked += TestButtonClicked;
        
        

        /*
        _TestSlider.RegisterValueChangedCallback(v => {
            var oldValue = v.previousValue;
            var newValue = v.newValue;
            Debug.Log("Hello"); 
        });
        */

        _TestSlider.RegisterValueChangedCallback(x => { Debug.Log("Hello2"); });

        _TestToggle.RegisterValueChangedCallback(x => {
            if (x.newValue)
            {
                Debug.Log("Mute ON");
            }
            else
            {
                Debug.Log("Mute OFF");
            }
        });
    }

    private void TestButtonClicked()
    {
        Debug.Log("Save Button Clicked");

    }
}
