using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorsCounter : MonoBehaviour {

    Text counter;
    public static int colorsNo = 0;
    // Use this for initialization
    void Start() {
        counter = gameObject.GetComponent<Text>();
        counter.text = "colors: " + colorsNo;
    }

    // Update is called once per frame
    void Update() {
        counter.text = "colors: " + colorsNo;
    }
    public static void Countcolor()
    {
        colorsNo += 1;
    }
}
