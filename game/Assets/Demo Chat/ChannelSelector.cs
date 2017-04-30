using System.Net.Mime;
using ExitGames.Client.Photon.Chat;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ChannelSelector : MonoBehaviour, IPointerClickHandler
{
    public string Channel;
	public TextMeshProUGUI label;

	public void Start(){
		label = GetComponentInChildren<TextMeshProUGUI> ();
	}

    public void SetChannel(string channel)
    {
        this.Channel = channel;
		label.text = this.Channel;        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChatBox handler = FindObjectOfType<ChatBox>();
        handler.ShowChannel(this.Channel);
    }
}
