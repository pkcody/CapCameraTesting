using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public string Name;

    public ItemObject recipeItem1;
    public ItemObject recipeItem2;
    public ItemObject recipeItem3;
    public ItemObject recipeItem4;

    public ItemObject recipeItem5;

    //public Recipe(string name, ItemObject i1, ItemObject res)
    //{
    //    Name = name;
    //    recipeItem1 = i1;
    //    recipeResult = res;
    //}
    //public Recipe(string name, ItemObject i1, ItemObject i2, ItemObject res)
    //{
    //    Name = name;
    //    recipeItem1 = i1;
    //    recipeItem2 = i2;
    //    recipeResult = res;
    //}
    //public Recipe(string name, ItemObject i1, ItemObject i2, ItemObject i3, ItemObject res)
    //{
    //    Name = name;
    //    recipeItem1 = i1;
    //    recipeItem2 = i2;
    //    recipeItem3 = i3;
    //    recipeResult = res;
    //}
    public Recipe(string name, ItemObject i1, ItemObject i2, ItemObject i3, ItemObject i4, ItemObject i5)
    {
        Name = name;
        recipeItem1 = i1;
        recipeItem2 = i2;
        recipeItem3 = i3;
        recipeItem4 = i4;
        recipeItem5 = i5;
    }

}

public class RecipeMaker : MonoBehaviour
{
    public static RecipeMaker instance;

    public ItemObject WoodRes;
    public ItemObject MetalRes;
    public ItemObject Empty;

    //building this
    public ItemObject BodyPartHeadRes;
    public ItemObject Tower2Res;
    public ItemObject Tower3Res;

    //class
    public Recipe Head;
    public Recipe Tower2;
    public Recipe Tower3;

    public List<Recipe> recipes = new List<Recipe>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CreateRecipes();
    }
    public void CreateRecipes()
    {
        Head = new Recipe("Rare Head", WoodRes, MetalRes, Empty, Empty, BodyPartHeadRes);
        Tower2 = new Recipe("Tower 2", WoodRes, WoodRes, WoodRes, Empty, Tower2Res);
        Tower3 = new Recipe("Tower 3", WoodRes, MetalRes, MetalRes, MetalRes, Tower3Res);
        recipes.Add(Head);
        recipes.Add(Tower2);
        recipes.Add(Tower3);
    }
}