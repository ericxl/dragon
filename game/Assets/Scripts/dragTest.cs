using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragTest : MonoBehaviour {

    public GameObject map;

    public Vector3 center;
    public Vector3 touchp;
    public Vector3 offset;
    public Vector3 newcenter;

    RaycastHit hit;

    public bool dragging = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*click to drag*/
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                map = hit.collider.gameObject;
                center = map.transform.position;
                touchp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset = touchp - center;
                dragging = true;

            }
            if (Input.GetMouseButton(0))
            {
                if (dragging)
                {
                    touchp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    newcenter = touchp - offset;
                    map.transform.position = new Vector3(newcenter.x, newcenter.y, newcenter.z);

                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                dragging = false;
            }


        }

       /*foreach(Touch touch in Input.touches)
        {
            switch(touch.phase)
            {

                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if(Physics.SphereCast(ray, 0.3f, out hit) ){
                        map = hit.collider.gameObject;
                        center = map.transform.position;
                        touchp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        offset = touchp - center;
                        dragging = true;

                    }

                    break;

                case TouchPhase.Moved:
                    
                    if(dragging)
                    {
                        touchp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        newcenter = touchp - offset;
                        map.transform.position = new Vector3(newcenter.x, newcenter.y, newcenter.z);

                    }

                    break;


                case TouchPhase.Ended:
                    dragging = false;
                    break;  
            }

        }*/
	}
}
