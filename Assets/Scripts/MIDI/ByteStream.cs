using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MIDI
{
    public class ByteStream
    {
        public ByteStream(byte[] bytes)
        {
            Bytes = bytes;
            Position = 0;
        }

        public byte Read()
        {
            return Bytes[Position++];
        }

        public Int32 ReadInt32()
        {
            return Read() << 24 | Read() << 16 | Read() << 8 | Read();
        }

        public string ReadString(int length)
        {
            StringBuilder sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                sb.Append((char) Read());
            }

            return sb.ToString();
        }

        public int Position { get; set; }

        public byte[] Bytes { get; private set; }
    }
}
