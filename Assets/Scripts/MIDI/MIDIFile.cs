using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MIDI
{
    public class MIDIFile
    {
        private List<MIDITrackChunk> _tracks;
        
        public MIDIFile()
        {
            _tracks = new List<MIDITrackChunk>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (MIDITrackChunk trackChunk in TrackChunks)
            {
                foreach(IMIDITrackEvent ev in trackChunk.Events)
                {
                    sb.Append(String.Format("D: {0}", ev.DeltaTime));

                    if (ev.GetType().Equals(typeof(MIDINoteOffEvent))) {
                        MIDINoteOffEvent e = (MIDINoteOffEvent) ev;

                        sb.Append(String.Format(" NoteOffEvent C/K {0}/{1}", e.Channel, e.Key));
                    } else if (ev.GetType().Equals(typeof(MIDINoteOnEvent)))  {
                        MIDINoteOnEvent e = (MIDINoteOnEvent)ev;

                        sb.Append(String.Format(" NoteOnEvent C/K {0}/{1}", e.Channel, e.Key));
                    }

                    sb.Append("\n");
                }
            }


            return sb.ToString();
        }

        public MIDIHeaderChunk HeaderChunk { get; set; }

        public List<MIDITrackChunk> TrackChunks
        {
            get { return _tracks; }
            set { _tracks = value; }
        }
    }
}
