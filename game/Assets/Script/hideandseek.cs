using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideandseek : MonoBehaviour {

    [SerializeField] private GameObject chat;
    [SerializeField] private GameObject button;
    
    public bool hide = false;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void HideObject()
    {
        
        chat.SetActive(hide);
        hide = !hide;
        if (hide)
        {
            button.transform.Translate(0, -198, 0);
        }
        else
        {
            button.transform.Translate(0, 198, 0);

        }
        

    }
    
}
