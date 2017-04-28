using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler{
	private Image bgImg;
	private Image joystickImg;
	private Vector2 inputvector;

	private void Start() {
		bgImg = GetComponent<Image> ();
		joystickImg = transform.GetChild (0).GetComponent<Image> ();
	}

	public virtual void OnDrag(PointerEventData ped) {
		Vector2 pos;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			inputvector = new Vector2 (pos.x*2 + 1, pos.y*2 - 1);
			inputvector = (inputvector.magnitude > 1.0f) ? inputvector.normalized : inputvector;
			Debug.Log (inputvector);
			joystickImg.rectTransform.anchoredPosition = 
				new Vector2 (inputvector.x * (bgImg.rectTransform.sizeDelta.x / 3), inputvector.y * (bgImg.rectTransform.sizeDelta.x / 3));
			
		
		}
	}

	public virtual void OnPointerDown(PointerEventData ped) {
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped) {
		inputvector = Vector2.zero;
		joystickImg.rectTransform.anchoredPosition = Vector2.zero;
	}

	public float Horizontal() {
		
		if (inputvector.x != 0) {
			return inputvector.x;
		} else {
			return Input.GetAxisRaw ("Horizontal");
		}
	}

	public float Vertical() {
		if (inputvector.y != 0) {
			return inputvector.y;
		} else {
			return Input.GetAxisRaw ("Vertical");
		}
	}
}
