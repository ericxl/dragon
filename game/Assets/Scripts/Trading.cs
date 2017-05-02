using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;
using PlayFab.ClientModels;


public class Trading : MonoBehaviour {
    //User Data
    [SerializeField]
    private GameObject GoldAmount;
    [SerializeField]
    private GameObject HP;
    [SerializeField]
    private GameObject MP;
    [SerializeField]
    private GameObject XP;
    [SerializeField]
    private GameObject ATTACK;
    [SerializeField]
    private GameObject DEFENCE;
    public GameObject LVL;


    //System items
    [SerializeField]
    private GameObject WaterAmount;
    [SerializeField]
    private GameObject WoodAmount;
    [SerializeField]
    private GameObject DiamondAmount;
    [SerializeField]
    private GameObject CoalAmount;

    //UI
    public GameObject ShopMoreInfoSlide;
    private Animator anim;

    // Use this for initialization
    void Start () {
        // getUserData();
        anim = ShopMoreInfoSlide.GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void exitShop()
    {
        SceneManager.LoadScene("Game");
    }

    public void buyResources(string type)
    {
        string amount = null;
        if (!(resourcesS2I(GoldAmount.GetComponent<Text>().text) > 0))
        {
            Debug.Log("Cannot add resources anymore!");
            return;
        }
        switch (type)
            {
                case "Water":
                    amount = WaterAmount.GetComponent<Text>().text;
                    WaterAmount.GetComponent<Text>().text = "x" + (resourcesS2I(amount)+1);
                break;
                case "Wood":
                    amount = WoodAmount.GetComponent<Text>().text;
                    WoodAmount.GetComponent<Text>().text = "x" + (resourcesS2I(amount) + 1);
                break;
                case "Gold":
                    amount = GoldAmount.GetComponent<Text>().text;
                    GoldAmount.GetComponent<Text>().text = "x" + (resourcesS2I(amount) + 1);
                break;
                case "Diamond":
                    amount = DiamondAmount.GetComponent<Text>().text;
                    DiamondAmount.GetComponent<Text>().text = "x" + (resourcesS2I(amount) + 1);
                break;
                case "Coal":
                    amount = CoalAmount.GetComponent<Text>().text;
                    CoalAmount.GetComponent<Text>().text = "x" + (resourcesS2I(amount) + 1);
                break;
            }

        GoldAmount.GetComponent<Text>().text = "x" + (resourcesS2I(GoldAmount.GetComponent<Text>().text) - 1);
        




    }

    private int resourcesS2I(string amount)
    {
        string RegPattern = "(x)";
        string[] substrings = Regex.Split(amount, RegPattern);
        int a = Int32.Parse(substrings[2]);
        return a;
    }

    private void getUserData()
    {
        PlayFab.PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {   Keys = null    }, 
        result => {
            if ((result.Data == null) || (result.Data.Count == 0)) { Debug.Log("No user data available"); }
            else {
                HP.GetComponent<Text>().text = result.Data["HP"].Value;
                MP.GetComponent<Text>().text = result.Data["MP"].Value;
                XP.GetComponent<Text>().text = result.Data["XP"].Value;
                ATTACK.GetComponent<Text>().text = result.Data["ATTACK"].Value;
                DEFENCE.GetComponent<Text>().text = result.Data["DEFENCE"].Value;
                LVL.GetComponent<Text>().text = result.Data["LVL"].Value;
                GoldAmount.GetComponent<Text>().text = result.Data["GOLD"].Value;
            }
        }, error => {
            Debug.Log("Got error retrieving user data:"+ error.ErrorMessage);
        });
    }

    public void setUserData()
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>(){
                 { "GOLD", "999" },
                 { "LVL", "1" },
                 { "HP", "100" },
                 { "XP", "0" },
                 { "MP", "100" },
                 { "ATTACK", "20" },
                 { "DEFENCE", "0" }
             }
        };

        PlayFab.PlayFabClientAPI.UpdateUserData(request, (result) =>
        {
            Debug.Log("Successfully set user data");
        }, (error) =>
        {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.ErrorDetails);
        });
    }


    public void ShowMoreInfo()
    {
        anim.enabled = true;
        anim.Play("ShopMoreInfoIn");
    }
    public void HideMoreInfo()
    {
        anim.Play("ShopMoreInfoOut");
        //set back the time scale to normal time scale
        //Time.timeScale = 1;
    }














}


