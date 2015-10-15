using UnityEngine;
using System.Collections;

public class zombieAnimator : MonoBehaviour {
    public Sprite[] sprites;
    public float fps;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
       spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
	
	}
	
	// Update is called once per frame
	void Update () {
        int index = (int)(Time.timeSinceLevelLoad * fps);
        index = index % sprites.Length;
        spriteRenderer.sprite = sprites[index];

    }
}
