using UnityEngine;
using System.Collections;

public class TextureChanger : MonoBehaviour {

    private Color[] colors;
    private int index=0;
	// Use this for initialization
	void Start () {
        colors = new Color[4];
        colors[0] = new Color(0.2f,0.5f,0.8f);
        colors[1] = new Color(0.6f,0.9f,0.1f);
        colors[2] = new Color(0.5f,0.7f,0.2f);
        colors[3] = new Color(0.9f,0.2f,0.4f);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<SpriteRenderer>().color = colors[index];
            if (index == 3)
                index = -1;
            index++;
        }
	}
}
