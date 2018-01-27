using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSnare : Actor {
    public Transform ball;
    public Color hitColor;

    private Transform ballSpawner;
    private Material drumTop;

    private bool isPressed;
    private Vector3 originalScale;
    private Vector3 scale;

	// Use this for initialization
	void Start () {
        ballSpawner = transform.Find("BallSpawner");
        drumTop = GetComponent<Renderer>().materials[2];
        originalScale = transform.localScale;
        scale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        //if (!isPressed)
        //{
            scale.Set(Mathf.Lerp(scale.x, originalScale.x, 0.1f), 
                Mathf.Lerp(scale.y, originalScale.y, 0.1f), 
                Mathf.Lerp(scale.z, originalScale.z, 0.1f));
            transform.localScale = scale;

            drumTop.SetColor("_Color", Color.Lerp(drumTop.GetColor("_Color"), Color.white, 0.1f));
        //}
	}

    public override void NoteOnEventDelayed(int channel, int key, int velocity)
    {
        if (channel != 9 || key != 40) return; //Only care about the snare (lol)
        
        Instantiate(ball, ballSpawner.position, ball.rotation);
    }

    public override void NoteOnEvent(int channel, int key, int velocity)
    {
        if (channel != 9 || key != 40) return;

        isPressed = true;

        scale.Set(1.1f, 1.1f, 0.9f);
        transform.localScale = scale;

        drumTop.SetColor("_Color", hitColor);
    }

    public override void NoteOffEvent(int channel, int key)
    {
        if (channel != 9 || key != 40) return;

        isPressed = false;
    }
}
