using UnityEngine;
using UnityEngine.UI;

public class BattleBar : MonoBehaviour
{
    [SerializeField] private Slider battleBar;


    public void ScoreUpdate(int _score)
    {
        battleBar.value = Mathf.Clamp(_score, battleBar.minValue, battleBar.maxValue);
    }
}
