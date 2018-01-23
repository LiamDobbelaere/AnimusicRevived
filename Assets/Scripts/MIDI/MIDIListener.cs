using Assets.Scripts.MIDI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIDIListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TextAsset asset = Resources.Load("doremi69") as TextAsset;

        var reader = new MIDIFileReader(asset.bytes);
        reader.Read();

        var midiFile = reader.MIDIFile;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
