using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseDrag() {
        float distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
        float sizeOfTable = GameObject.Find("Tiles").GetComponent<RectTransform>().rect.height/100;
        float sizeOfRacket = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().size.x;//
        // Новое значение положения ракетки (значение положения курсора мышки) не должно быть больше чем свободное место
        float HalfFreeSpace = sizeOfTable / 2 - sizeOfRacket / 2;
        if (newPosition.x < HalfFreeSpace && newPosition.x > -HalfFreeSpace)
            transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
    }
}
