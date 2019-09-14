using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    public float healthValue;
	private RectTransform healthBar;
	private GameObject mainCamera;

	private float initSize;

	// Use this for initialization
	void Start ()
	{
		healthBar = transform.GetChild(1).GetComponent<RectTransform>();
		initSize = healthBar.localScale.x;
        transform.localRotation = Camera.main.transform.localRotation;
		GetComponent<Canvas>().enabled = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (healthValue < 1 && GetComponent<Canvas>().enabled == false)
		{
			GetComponent<Canvas>().enabled = true;
		}

		if (!float.IsNaN(healthValue))
		{
			healthBar.localScale = new Vector3(initSize * healthValue, healthBar.localScale.y, healthBar.localScale.z);
		}


	}
    
}
