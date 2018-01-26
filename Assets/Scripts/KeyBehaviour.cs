using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour {
    public bool isPressed;
    public Color color;
    public Color defaultColor = Color.white;

    private Renderer myRenderer;
    private Quaternion startRotation;

	// Use this for initialization
	void Start () {
        myRenderer = GetComponent<Renderer>();
        
        startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (isPressed)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation * Quaternion.AngleAxis(5f, Vector3.up), 0.8f);
            myRenderer.material.SetColor("_Color", Color.Lerp(myRenderer.material.GetColor("_Color"), color, 0.8f));
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, 0.2f);
            myRenderer.material.SetColor("_Color", Color.Lerp(myRenderer.material.GetColor("_Color"), defaultColor, 0.05f));
        }
	}
}
