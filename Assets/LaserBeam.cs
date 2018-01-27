using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : Actor
{
    public Color32 On;
    public Color32 Off;
    public Animator LaserAnim;
    public int MIDIChannel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public override void NoteOnEvent(int channel, int key, int velocity)
    {
        if (channel == MIDIChannel && key != 40) {
            GetComponent<Renderer>().material.SetColor("_Color", On);
            float VelocityF = (float)(velocity) / 20;
            LaserAnim.SetFloat("Speed", VelocityF);
            Debug.Log("Velocity : " + velocity);
        }
    }
    public override void NoteOffEvent(int channel, int key)
    {
        if (channel == MIDIChannel)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Off);
            LaserAnim.SetFloat("Speed", 0);
        }
    }
}
