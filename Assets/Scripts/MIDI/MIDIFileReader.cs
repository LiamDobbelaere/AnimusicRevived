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

            while (_byteStream.CanRead)
            {
                ReadChunk();
            }
        }

        private void ReadChunk()
        {
            string type = _byteStream.ReadString(4);
            int length = _byteStream.ReadInt32();

            switch (type)
            {
                case MIDIHeaderChunk.TYPE:
                    ReadHeaderChunk(length);
                    break;
                case MIDITrackChunk.TYPE:
                    ReadTrackChunk(length);
                    break;
                default:
                    if (Verbose) Debug.Log(String.Format("Skipping chunk with type {0}", type));
                    SkipChunk(length);
                    break;
            }
        }

        private void SkipChunk(int length)
        {
            for (int i = 0; i < length; i++)
                _byteStream.Read();
        }

        private void ReadHeaderChunk(int length)
        {
            if (Verbose) Debug.Log(String.Format("Reading header chunk with length {0}", length));
            MIDIFile.HeaderChunk = new MIDIHeaderChunk();

            Int16 format = _byteStream.ReadInt16();
            Int16 tracks = _byteStream.ReadInt16();
            Int16 division = _byteStream.ReadInt16();

            if (Verbose) Debug.Log(String.Format("Header chunk F/T/D is {0}/{1}/{2}", format, tracks, division));

            MIDIFile.HeaderChunk.Format = format;
            if (format != 1) throw new Exception(String.Format("Unsupported midi format {0}", format));

            byte division_msb = (byte)((division >> 8) & 0xFFu);
            if (division_msb != 0) throw new Exception("SMTPE frame unit time not supported");

            //Note, don't do this if division msb is 1, but we don't support that anyway
            MIDIFile.HeaderChunk.TicksPerQuarterNote = division; 
            
            for (int i = 0; i < length - 6; i++)
            {
                _byteStream.Read();
            }
        }

        private void ReadTrackChunk(int length)
        {
            if (Verbose) Debug.Log(String.Format("Reading track chunk with length {0}", length));

            MIDITrackChunk newTrackChunk = new MIDITrackChunk();

            MIDIFile.TrackChunks.Add(newTrackChunk);

            while (length > 0)
            {
                length -= ReadTrackEvent(newTrackChunk);
            }
        }

        private int ReadTrackEvent(MIDITrackChunk trackChunk)
        {
            int startPosition = _byteStream.Position;

            int deltaTime = _byteStream.ReadVariableLength();
            int eventType = _byteStream.Read();

            IMIDITrackEvent trackEvent = null;

            if (eventType == 0xFF) //Meta events
            {
                ReadMetaEvent(_byteStream.Read());
            }
            else if (eventType >= 0xB0 && eventType <= 0xBF) //Channel mode messages 
            {
                if (Verbose) Debug.Log(String.Format("Unhandled channel mode message {0:x2}", eventType));
                _byteStream.Read();
                _byteStream.Read();
            }
            else if (eventType >= 0x80 && eventType <= 0xEF) //Channel voice messages
            {
                trackEvent = ReadChannelVoiceEvent(eventType);
            }
            else 
            {
                if (Verbose) Debug.Log(String.Format("Unknown event type {0:x2}", eventType));
            }

            if (trackEvent != null)
            {
                trackEvent.DeltaTime = deltaTime;
                trackChunk.Events.Add(trackEvent);
            }

            return _byteStream.Position - startPosition;
        }

        private void ReadMetaEvent(int type)
        {
            int length = _byteStream.ReadVariableLength();

            if (Verbose) Debug.Log(String.Format("Found meta event type {0:x2} with length {1}", type, length));

            switch (type)
            {
                default:
                    if (Verbose) Debug.Log(String.Format("Ignoring meta event {0:x2}", type));

                    for (int i = 0; i < length; i++)
                    {
                        _byteStream.Read();
                    }
                    break;
            }
        }

        private IMIDITrackEvent ReadChannelVoiceEvent(int eventType)
        {
            if (eventType < 0xC0 || eventType > 0xDF)
            {
                //2 Data bytes available here
                byte a = _byteStream.Read();
                byte b = _byteStream.Read(); 

                if (eventType >= 0x80 && eventType <= 0x8F) //Note off
                {
                    byte midiChannel = (byte) (eventType - 0x80);

                    MIDINoteOffEvent ev = new MIDINoteOffEvent(midiChannel, a);

                    if (Verbose) Debug.Log(String.Format("Note off message C/K {0}/{1}", ev.Channel, ev.Key));

                    return ev;
                }
                else if (eventType >= 0x90 && eventType <= 0x9F) 
                {
                    byte midiChannel = (byte)(eventType - 0x90);
                    
                    MIDINoteOnEvent ev = new MIDINoteOnEvent(midiChannel, a);

                    if (Verbose) Debug.Log(String.Format("Note on message C/K {0}/{1}", ev.Channel, ev.Key));

                    return ev;
                }
                else
                {
                    if (Verbose) Debug.Log(String.Format("Unhandled channel voice message {0:x2}", eventType));
                }
            }
            else
            {
                //1 Data byte available here
                byte a = _byteStream.Read();

                if (Verbose) Debug.Log(String.Format("Unhandled channel voice message {0:x2}", eventType));
            }

            return null;
        }

        public MIDIFile MIDIFile { get; set; }

        public bool Verbose { get; set; }
    }
}
