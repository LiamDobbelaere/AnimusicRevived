using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedKeyboard : MonoBehaviour {
    public Transform keyLLeft;
    public Transform keyLRight;
    public Transform keyM;
    public Transform keyBlack;

    private List<Transform> keyPattern = new List<Transform>();

	// Use this for initialization
	void Start () {
        keyPattern.Add(keyLRight);
        keyPattern.Add(keyM);
        keyPattern.Add(keyLLeft);
        keyPattern.Add(keyLRight);
        keyPattern.Add(keyM);
        keyPattern.Add(keyM);
        keyPattern.Add(keyLLeft);

        int midiKey = 36;

        for (int k = 0; k < 6; k++)
        {
            for (int i = 0; i < keyPattern.Count; i++)
            {
                var obj = Instantiate(keyPattern[i],
                    transform.position + new Vector3(0, 0, i * 0.5f) + new Vector3(0, 0, k * (keyPattern.Count * 0.5f)),
                    keyPattern[i].rotation, this.transform);

                obj.name = midiKey.ToString();
                obj.gameObject.AddComponent<KeyBehaviour>();

                if (i == 2 || i == 6)
                {
                    midiKey++;
                }
                else
                {
                    midiKey += 2;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
