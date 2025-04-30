using UnityEngine;

public class FitSpriteToScreen : MonoBehaviour
{
    [SerializeField] private Vector2 startSpriteSize;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startSpriteSize = sr.bounds.size;
        FitToScreen();

    }



    void OnEnable()
    {

        // if (sr == null)
        // {
        //     sr = GetComponent<SpriteRenderer>();
        //     startSpriteSize = sr.bounds.size;
        //     FitToScreen();
        // }
        FitToScreen();
    }
    [ContextMenu("FitToScreen")]
    void FitToScreen()
    {


        if (sr == null) return;


        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Screen.width / Screen.height;


        float spriteHeight = startSpriteSize.y;
        float spriteWidth = startSpriteSize.x;


        transform.localScale = new Vector3(screenWidth / spriteWidth, screenHeight / spriteHeight, 1f);
    }
}
