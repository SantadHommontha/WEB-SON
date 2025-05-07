using System;
using System.Collections.Generic;
using UnityEngine;

public class SandAnimationController : MonoBehaviour
{
    [SerializeField] private PlaySpriteAnimation[] sandSetAB;

    [SerializeField] private PlaySpriteAnimation sandSetC;

    [SerializeField] private IntValue score;
    [SerializeField] private Vector3Value lastSandPosition;
    [SerializeField] private Transform point;

    [SerializeField] private List<PlaySpriteAnimation> playSpriteAnimations = new List<PlaySpriteAnimation>();

    [SerializeField] PlaySpriteAnimation waterGun;
    [SerializeField] private PlaySpriteAnimation bucget;

    private int lastScore = 0;
    void Start()
    {
        lastScore = 0;
        score.OnValueChange += OnScoreUpdate;
    }

    private void OnScoreUpdate(int _score)
    {
        Debug.Log(_score);
        if (_score > playSpriteAnimations.Count * 10 && _score > 0)
        {
            Create();

        }
        else if (_score <= (playSpriteAnimations.Count - 1) * 10)
        {
            Delete();

        }
        if (_score < 0)
        {
            waterGun.PlayUpUntil();
        }
        
        if (_score <= 0 && playSpriteAnimations.Count > 0)
        {
            while (playSpriteAnimations.Count > 0)
            {
                Delete();
            }
        }
    }

    [ContextMenu("Up")]
    private void Up()
    {
        score.Value += 10;
    }
    [ContextMenu("Down")]
    private void Down()
    {
        score.Value -= 10;
    }


    [ContextMenu("Create")]
    private void Create()
    {
        bucget.PlayUp();
        Vector3 position = CalculatorPosition();

        var s = Instantiate(sandSetAB[UnityEngine.Random.Range(0, sandSetAB.Length)], position, Quaternion.identity, point);

        if (playSpriteAnimations.Count > 0)
            playSpriteAnimations[playSpriteAnimations.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = 2;
        s.GetComponent<SpriteRenderer>().sortingOrder = 1;
        playSpriteAnimations.Add(s);
        lastSandPosition.Value = s.transform.position;
        Vector3 hPosition = CalculatorPosition();

        var h = Instantiate(sandSetC, hPosition, Quaternion.identity, point);

        s.playSpriteAnimation = h;
        s.PlayUp();


    }



    [ContextMenu("Delete")]
    private void Delete()
    {
        if (playSpriteAnimations.Count == 0) return;
        waterGun.PlayUp();
        var last = playSpriteAnimations[playSpriteAnimations.Count - 1];

        last.OnanimationCompleted = last.Delete;
        last.PlayDown();
        lastSandPosition.Value = last.transform.position;
        playSpriteAnimations.RemoveAt(playSpriteAnimations.Count - 1);
    }

    private Vector3 CalculatorPosition()
    {
        Vector3 result = Vector3.zero;

        if (playSpriteAnimations.Count > 0)
        {
            var lastSprite = playSpriteAnimations[playSpriteAnimations.Count - 1];

            var sp = lastSprite.gameObject.GetComponent<SpriteRenderer>();
            result = lastSprite.transform.position + new Vector3(0, sp.bounds.size.y / 3f, 0);
            return result;
        }
        else
        {
            var sand = sandSetAB[UnityEngine.Random.Range(0, sandSetAB.Length)];
            var sp = sand.gameObject.GetComponent<SpriteRenderer>();
            result = point.position + new Vector3(0, sp.bounds.size.y / 3f, 0);
            return result;
        }
    }


}
