using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MIDI
{
    public class MIDIHeaderChunk : IMIDIChunk
    {
        public static string TYPE = "MThd";

        public MIDIHeaderChunk(Int32 length)
        {
            Type = TYPE;
            Length = length;
        }

        public string Type { get; set; }
        public Int32 Length { get; set; }
    }
}
