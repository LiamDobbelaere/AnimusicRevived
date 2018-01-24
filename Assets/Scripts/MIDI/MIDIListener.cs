using Assets.Scripts.MIDI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIDIListener : MonoBehaviour {
    private AudioSource sawTone;
    private MIDITrackChunk currentTrackChunk;
    private int currentEvent;

	// Use this for initialization
	void Start () {
        sawTone = GetComponent<AudioSource>();

        TextAsset asset = Resources.Load("storm") as TextAsset;

        var reader = new MIDIFileReader(asset.bytes);
        reader.Read();

        currentTrackChunk = reader.MIDIFile.TrackChunks[reader.MIDIFile.TrackChunks.Count - 1];
        currentEvent = 0;

        Play();
    }

    void Play()
    {
        StartCoroutine("TickEvent", 0f);
    }

    IEnumerator TickEvent(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        IMIDITrackEvent ev = currentTrackChunk.Events[currentEvent];

        if (ev.GetType().Equals(typeof(MIDINoteOnEvent)))
        {
            MIDINoteOnEvent noteOnEvent = (MIDINoteOnEvent)ev;
            float note = ((float) noteOnEvent.Key.note);
            float transpose = (noteOnEvent.Key.octave - 5.0f) * 12f;

            //AudioSource newSource = gameObject.AddComponent<AudioSource>();
            //newSource.clip = sawTone.clip;

            sawTone.pitch = Mathf.Pow(2, (note + transpose) / 12.0f);
            sawTone.Play();

        }
        else if (ev.GetType().Equals(typeof(MIDINoteOffEvent)))
        {
            MIDINoteOffEvent noteOffEvent = (MIDINoteOffEvent)ev;

            //sawTone.Stop();
        }

        if (++currentEvent < currentTrackChunk.Events.Count) StartCoroutine("TickEvent", ev.DeltaTime / (96.0f * 3.5f));
    }

	// Update is called once per frame
	void Update () {
		
	}
}
