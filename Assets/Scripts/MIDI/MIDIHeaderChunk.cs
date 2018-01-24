using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MIDI
{
    public class MIDIHeaderChunk
    {
        public const string TYPE = "MThd";

        public MIDIHeaderChunk()
        {

        }

        public Int16 Format { get; set; }
        public Int16 TicksPerQuarterNote { get; set; }
    }
}
