using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour {
    public bool isPressed;

    private Renderer renderer;
    private Quaternion startRotation;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (isPressed)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation * Quaternion.AngleAxis(5f, Vector3.up), 0.2f);
            renderer.material.SetColor("_Color", Color.Lerp(renderer.material.GetColor("_Color"), Color.yellow, 0.2f));
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, 0.2f);
            renderer.material.SetColor("_Color", Color.Lerp(renderer.material.GetColor("_Color"), Color.white, 0.2f));
        }
	}
}
