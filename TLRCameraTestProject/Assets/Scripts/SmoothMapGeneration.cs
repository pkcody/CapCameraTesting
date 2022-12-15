using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMapGeneration : MonoBehaviour
{
    public static SmoothMapGeneration instance;
    // Terrain
    private Terrain terrain;
    private TerrainData terData;

    // Textures used to read/write
    private Texture2D tex;
    private Texture2D redTex;
    private Texture2D greenTex;
    private Texture2D blueTex;
    private int mapResolution = 1000;

    public void Awake()
    {
        terrain = GetComponent<Terrain>();
        byte[] fileData;

        terData = terrain.terrainData;

        //Get Red Tex
        fileData = System.IO.File.ReadAllBytes("Assets\\Textures\\RedGroundTex.png");
        redTex = new Texture2D(mapResolution, mapResolution);
        redTex.LoadImage(fileData);

        //Get Green Tex
        fileData = System.IO.File.ReadAllBytes("Assets\\Textures\\GreenGroundTex.png");
        greenTex = new Texture2D(mapResolution, mapResolution);
        greenTex.LoadImage(fileData);

        //Get Blue Tex
        fileData = System.IO.File.ReadAllBytes("Assets\\Textures\\BlueGroundTex.png");
        blueTex = new Texture2D(mapResolution, mapResolution);
        blueTex.LoadImage(fileData);

        CreateMap();
    }

    public void CreateMap()
    {
        byte[] fileData;

        fileData = System.IO.File.ReadAllBytes("Assets\\coloredPng.png");
        tex = new Texture2D(mapResolution, mapResolution);
        tex.LoadImage(fileData);
        terData = terrain.terrainData;

        Color pixelColor;
        Color biomeColor;

        Texture2D biomeMap = new Texture2D(mapResolution, mapResolution, TextureFormat.RGB24, false);
        for (int y = 0; y < mapResolution; y++)
        {
            for (int x = 0; x < mapResolution; x++)
            {
                int randX = Random.Range(0, redTex.width);
                int randY = Random.Range(0, redTex.height);
                pixelColor = tex.GetPixel(x, y);
                
                if (pixelColor.r == Mathf.Max(pixelColor.r, pixelColor.g, pixelColor.b)) // Red
                {
                    biomeColor = redTex.GetPixel(randX, randY);
                }
                else if (pixelColor.g == Mathf.Max(pixelColor.r, pixelColor.g, pixelColor.b)) // Green
                {
                    biomeColor = greenTex.GetPixel(randX, randY);
                }
                else if (pixelColor.b == Mathf.Max(pixelColor.r, pixelColor.g, pixelColor.b)) // Blue
                {
                    biomeColor = blueTex.GetPixel(randX, randY);
                }
                else // pixel still white, set to green
                {
                    biomeColor = greenTex.GetPixel(randX, randY);
                }

                biomeMap.SetPixel(x,y, biomeColor);
            }
        }

        biomeMap.Apply();
        terData.terrainLayers[0].diffuseTexture = biomeMap;
        System.IO.File.WriteAllBytes("Assets\\SmoothMapGeneration.png", biomeMap.EncodeToPNG());
    }
}
