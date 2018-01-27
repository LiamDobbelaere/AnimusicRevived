using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AnimatedKeyboard : Actor
{
    public Transform keyLLeft;
    public Transform keyLRight;
    public Transform keyM;
    public Transform keyMSR;
    public Transform keyMSL;
    public Transform keyBlack;

    private List<Transform> keyPattern = new List<Transform>();

    public Color[] whiteKeyColors = new Color[7];
    public Color[] blackKeyColors = new Color[5];

    public int listenChannel;

    private float[] blackKeyOffsets = new float[7] {0.21f, 0.3f, 0.21f, 0.21f, 0.25f, 0.29f, 0.21f};
    // Use this for initialization
    void Awake()
    {
        if (this.transform.childCount == 0)
        {
            var children = new List<GameObject>();
            foreach (Transform child in this.transform)
            {
                children.Add(child.gameObject);
            }
            children.ForEach(child => DestroyImmediate(child));

            keyPattern.Add(keyLRight);
            keyPattern.Add(keyM);
            keyPattern.Add(keyLLeft);
            keyPattern.Add(keyLRight);
            keyPattern.Add(keyMSR);
            keyPattern.Add(keyMSL);
            keyPattern.Add(keyLLeft);

            int midiKey = 36;

            for (int k = 0; k < 8; k++)
            {
                int blackKey = 0;

                for (int i = 0; i < keyPattern.Count; i++)
                {
                    Transform obj = null;

                    obj = Instantiate(keyPattern[i],
                    transform.position + new Vector3(0, 0, i * 0.5f) + new Vector3(0, 0, k * (7 * 0.5f)),
                    keyPattern[i].rotation, this.transform);

                    obj.name = midiKey.ToString();
                    obj.gameObject.AddComponent<KeyBehaviour>().color = whiteKeyColors[i];

                    if (i != 2 && i != 6)
                    {
                        midiKey++;

                        obj = Instantiate(keyBlack,
                        transform.position + new Vector3(0, 0, i * 0.5f) + new Vector3(0, 0, k * (7 * 0.5f)) + new Vector3(0, 0.1f, blackKeyOffsets[i]),
                        keyBlack.rotation, this.transform);
                        obj.name = midiKey.ToString();
                        var kb = obj.gameObject.AddComponent<KeyBehaviour>();
                        kb.color = blackKeyColors[blackKey];
                        kb.defaultColor = Color.black;

                        blackKey++;
                    }

                    midiKey++;
                }
            }
        }
    }

    public override void NoteOnEvent(int channel, int key, int velocity)
    {
        Debug.Log(channel.ToString());

        if (channel == listenChannel)
        {
            var child = transform.Find(key.ToString());

            if (child != null)
            {
                child.GetComponent<KeyBehaviour>().isPressed = true;
            }
        }
    }

    public override void NoteOffEvent(int channel, int key)
    {
        if (channel == listenChannel)
        {
            var child = transform.Find(key.ToString());

            if (child != null)
            {
                child.GetComponent<KeyBehaviour>().isPressed = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
