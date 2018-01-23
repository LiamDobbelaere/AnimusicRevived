using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.MIDI
{
    public class MIDIFileReader
    {
        private ByteStream _byteStream;

        public MIDIFileReader(byte[] bytes)
        {
            _byteStream = new ByteStream(bytes);
        }

        public void Read()
        {
            MIDIFile = new MIDIFile();
        
            ReadHeaderChunk();
        }

        private void ReadHeaderChunk()
        {
            string type = _byteStream.ReadString(4);
            if (type != MIDIHeaderChunk.TYPE) 
                throw new Exception(String.Format("Invalid header chunk type {0}", type));

            int length = _byteStream.ReadInt32();

            MIDIFile.HeaderChunk = new MIDIHeaderChunk(length);

            Debug.Log(String.Format("{0}, {1}", MIDIFile.HeaderChunk.Type, MIDIFile.HeaderChunk.Length));
        }

        public MIDIFile MIDIFile { get; set; }
    }
}
