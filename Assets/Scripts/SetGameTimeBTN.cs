using UnityEngine;
using TMPro;
public class SetGameTimeBTN : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;

    [SerializeField] private FloatValue gameTime;

    void Start()
    {
        input.text =  gameTime.Value.ToString();
    }
    public void CharacterUpdate(string _input)
    {
        if (float.TryParse(_input, out var result))
        {
            gameTime.Value = result;
        }
    }
}
