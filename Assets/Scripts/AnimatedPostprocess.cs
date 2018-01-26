using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class AnimatedPostprocess : Actor {
    private PostProcessingProfile profile;

	// Use this for initialization
	void Start () {
        var behaviour = GetComponent<PostProcessingBehaviour>();
        profile = Instantiate(behaviour.profile);
        behaviour.profile = profile;
	}
	
	// Update is called once per frame
	void Update () {
        var settings = profile.bloom.settings;
        settings.bloom.intensity = Mathf.Lerp(settings.bloom.intensity, 0.0f, 0.2f);
        profile.bloom.settings = settings;
	}

    public override void NoteOnEvent(int channel, int key, int velocity)
    {
        if (channel != 9 || key != 40) return;

        var settings = profile.bloom.settings;
        settings.bloom.intensity = 1f;
        profile.bloom.settings = settings;
    }
}
