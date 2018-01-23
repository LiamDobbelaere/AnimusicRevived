using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MIDI
{
    public interface IMIDIChunk
    {
        string Type { get; set; }
        Int32 Length { get; set; }
    }
}
