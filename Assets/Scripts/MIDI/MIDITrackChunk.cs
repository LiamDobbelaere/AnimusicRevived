using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MIDI
{
    public class MIDITrackChunk
    {
        private List<IMIDITrackEvent> _events;
        
        public const string TYPE = "MTrk";

        public MIDITrackChunk()
        {
            _events = new List<IMIDITrackEvent>();
        }

        public List<IMIDITrackEvent> Events
        {
            get { return _events; }
            set { _events = value; }
        }
        
    }
}
