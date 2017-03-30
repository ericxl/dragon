using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chatbox : MonoBehaviour {

    public List<string> chatHistory = new List<string>();
    [SerializeField] private InputField message;
    [SerializeField] private Text chat_box;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void send()
    {
        string m = message.text;
        m = m + "\n";
        chat_box.text += m;

    }


}
