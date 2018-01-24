using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MIDI
{
    public class MIDINoteOnEvent : IMIDITrackEvent
    {
        public MIDINoteOnEvent(byte channel, byte key)
        {
            Channel = channel;
            Key = MIDIKey.From(key);
        }

        public override string ToString()
        {
            return String.Format("Note On C/K {0}/{1}", Channel, Key);
        }

        public MIDIKey Key { get; set; }
        public byte Channel { get; set; }
        public int DeltaTime { get; set; }
    }
}
