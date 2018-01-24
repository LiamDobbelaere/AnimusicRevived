using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MIDI
{
    public enum Note
    {
        C,
        Cs,
        D,
        Ds,
        E,
        F,
        Fs,
        G,
        Gs,
        A,
        As,
        B
    }

    public struct MIDIKey
    {
        public byte octave;
        public Note note;
        
        public static MIDIKey From(byte midiKey)
        {
            MIDIKey result;

            byte octave = (byte) (midiKey / 12);
            Note note = (Note) (midiKey % 12);

            result.octave = octave;
            result.note = note;

            return result;
        }

        public override string ToString()
        {
            return String.Format("{0}{1}", note, octave);
        }
    }
}
