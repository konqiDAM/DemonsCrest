using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField] Text text;
    private int R, G, B;
    private int mr, mg, mb;
    // Start is called before the first frame update
    void Start()
    {
        R = 50;
        G = 0;
        B = 200;
        mr = 1;
        mg = 1;
        mb = -1;
    }

    // Update is called once per frame
    void Update()
    {
        Color32 textColor32 = new Color32((byte)R,(byte)G,(byte)B, 255);
        text.color = textColor32;
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Level1");
        }
        rainBowColors();
    }
    private void rainBowColors()
    {
        if (R >= 253 || R <= 1)
            mr = -mr;
        if (G >= 253 || G <= 1)
            mg = -mb;
        if (B >= 253 || B <= 1)
            mb = -mb;
        R += mr;
        G += mg;
        B += mb;
    }
}
