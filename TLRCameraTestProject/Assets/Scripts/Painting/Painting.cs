using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Painting : MonoBehaviour
{
    public CustomCursor cursor;
    private Camera cam;
    public Slider radiusSlider;

    public int radius;

    public Color redColor;
    public Color blueColor;
    public Color greenColor;
    public Color currColor;

    public Texture2D tex;
    public Renderer rend;

    void Awake()
    {
        foreach (GameObject go in PlayerSpawning.instance.players)
        {
            if (go != null)
            {
                go.GetComponent<PlayerInput>().SwitchCurrentActionMap("Painting");
                go.GetComponent<PlayerInput>().defaultActionMap = "Painting";
                go.GetComponent<PlayerPainting>().enabled = true;
            }
        }

        cam = GetComponent<Camera>();
        currColor = Color.white;

        //ChangeRadiusSize();
        tex = rend.material.mainTexture as Texture2D;
    }

    
    public void SetTextureColor()
    {
        Color[] checkColors = tex.GetPixels();

        for (int i = 0; i < checkColors.Length; i++)
        {
            //print(checkColors[i].linear);
            if (checkColors[i] == Color.white)
            {
                checkColors[i] = Color.green;
                print("there is WHITE");
                //return;
            }
            
        }

        System.IO.File.WriteAllBytes("Assets\\coloredPng.png", tex.EncodeToPNG());
        print("aply");
    }
    
    public void ClearTexture()
    {
        Color[] resetColors = tex.GetPixels();

        for (int i = 0; i < resetColors.Length; i++)
        {
            resetColors[i] = Color.white;
        }

        tex.SetPixels(resetColors);
        tex.Apply();
    }
}
