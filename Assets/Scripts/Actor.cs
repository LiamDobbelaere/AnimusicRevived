using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
    public virtual void NoteOnEvent(int channel, int key, int velocity)
    {

    }

    public virtual void NoteOffEvent(int channel, int key)
    {

    }

    public virtual void NoteOnEventDelayed(int channel, int key, int velocity)
    {

    }

    public virtual void NoteOffEventDelayed(int channel, int key)
    {

    }
}
