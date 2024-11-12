using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private Image image;
    [SerializeField] private Gradient gradient;

    private float coef;

    private void Start()
    {
        coef = 255 / slider.maxValue;
    }

    public void UpdateHealthBar(int currentValue)
    {
        slider.value = currentValue;
        image.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetTransparency(bool activate)
    {
        slider.gameObject.SetActive(activate);
    }

}
