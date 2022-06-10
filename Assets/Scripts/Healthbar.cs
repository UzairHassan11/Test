using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image fill;

    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;
        fill.color = _gradient.Evaluate(1f);
    }
    
    public void SetHealth(int health)
    {
        _slider.value = health;
        fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }
}
