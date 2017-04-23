using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBar : MonoBehaviour {

    [SerializeField] private GameObject chat;
    [SerializeField] private GameObject collapse;
    [SerializeField] private GameObject show;
    // Use this for initialization
    void Start()
    {
        show.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideObject()
    {
        chat.SetActive(false);
        collapse.SetActive(false);
        show.SetActive(true);
    }

    public void ShowObject()
    {
        show.SetActive(false);
        chat.SetActive(true);
        collapse.SetActive(true);
    }
}
