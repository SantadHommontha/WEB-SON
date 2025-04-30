using UnityEngine;
using System.Collections;
using System;
public class PlaySpriteAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] allSprtite;
    [SerializeField] private bool backToFirst = false;
    [SerializeField] private float framePerTime;
    [SerializeField] private float playTime = 1f;
    public PlaySpriteAnimation playSpriteAnimation;
    private SpriteRenderer spriteRenderer;
    private bool up = false;
    private int spriteIndex = 0;
    private int changeValue = 1;
    private Coroutine co_timeAnimation;

    public Action OnanimationCompleted;


    void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        up = false;
        spriteIndex = 0;
        spriteRenderer.sprite = allSprtite[0];
        if (playSpriteAnimation)
            playSpriteAnimation.SetSpriteToNone();
    }
    public void SetSpriteToFirstSprite()
    {
        spriteRenderer.sprite = allSprtite[0];
    }
    public void SetSpriteToNone()
    {
        spriteRenderer.sprite = null;
    }
    [ContextMenu("PlayUP")]
    public void PlayUp()
    {

        changeValue = 1;
        spriteIndex = -1;
        if (playSpriteAnimation)
        {
            playSpriteAnimation.OnanimationCompleted = null;
            OnanimationCompleted += playSpriteAnimation.PlayUp;
            OnanimationCompleted += ClearEvent;

        }
        PlayAni();
    }
    [ContextMenu("PlayDown")]
    public void PlayDown()
    {

        changeValue = -1;
        spriteIndex = allSprtite.Length;

        if (playSpriteAnimation)
        {
            playSpriteAnimation.OnanimationCompleted += PlayAni;
            playSpriteAnimation.OnanimationCompleted += playSpriteAnimation.SetSpriteToNone;

            playSpriteAnimation.PlayDown();
        }
        else
            PlayAni();

    }



    public void Play()
    {
        if (up)
            PlayDown();
        else
            PlayUp();
    }
    private void PlayAni()
    {
        Debug.Log(gameObject.name);
        if (co_timeAnimation != null)
        {
            StopCoroutine(co_timeAnimation);
            co_timeAnimation = null;
        }
        co_timeAnimation = StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        float time = playTime / framePerTime;
        int count = 0;

        while (count < allSprtite.Length)
        {
            yield return new WaitForSeconds(time);
            spriteRenderer.sprite = allSprtite[NextSprite()];
            count++;
        }
        if (spriteIndex == 0)
            up = false;
        else
            up = true;
        co_timeAnimation = null;
        OnanimationCompleted?.Invoke();

        if (backToFirst)
            spriteRenderer.sprite = allSprtite[0];
    }

    private int NextSprite()
    {
        spriteIndex += changeValue;
        return UnityEngine.Mathf.Clamp(spriteIndex, 0, allSprtite.Length - 1);
    }
    public void SetSpriteByIndex(int _index)
    {
        spriteRenderer.sprite = allSprtite[_index];
    }

    public void ClearEvent()
    {
        OnanimationCompleted = null;
    }
    public void Delete()
    {
        if (playSpriteAnimation)
            playSpriteAnimation.Delete();
        Destroy(gameObject);
    }

}
