using TMPro;
using UnityEngine;

public class ValueDisplayText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private IntValue intValue;
    [SerializeField] private FloatValue floatValue;

    void Start()
    {
        if (intValue) intValue.OnValueChange += IntValueChange;
        if (floatValue) floatValue.OnValueChange += FloatValueChange;
    }

    private void IntValueChange(int _value)
    {
        ChangeText(_value.ToString());
    }

    private void FloatValueChange(float _value)
    {
        ChangeText(_value.ToString());
    }

    public void ChangeText(string _newText)
    {
        text.text = _newText;
    }
}