using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MIDI
{
    public interface IMIDITrackEvent
    {
        int DeltaTime { get; set; }
    }
}
