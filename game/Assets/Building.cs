using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    [SerializeField]
    private GameObject buildings;
    [SerializeField]
    private GameObject sidebar;
    [SerializeField]
    private GameObject sidebar_button;
    // Use this for initialization
    void Start () {
        buildings.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void openBuilding()
    {
        buildings.SetActive(true);
        sidebar.SetActive(false);
        sidebar_button.SetActive(false);
        
    }

    public void closeBuilding()
    {
        buildings.SetActive(false);
        sidebar.SetActive(true);
        sidebar_button.SetActive(true);

    }
}
