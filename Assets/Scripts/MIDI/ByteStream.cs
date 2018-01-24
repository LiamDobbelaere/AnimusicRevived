using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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

        public Int16 ReadInt16()
        {
            return (Int16) (Read() << 8 | Read());
        }

        public string ReadString(int length)
        {
            StringBuilder sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                sb.Append((char)Read());
            }

            return sb.ToString();
        }

        public int ReadVariableLength()
        {
            List<byte> bytes = new List<byte>();

            byte currentByte = Read();

            while (((currentByte >> 7) & 0xFFu) == 1)
            {
                currentByte &= 0x7F;
                bytes.Add(currentByte);
                
                currentByte = Read();
            }

            currentByte &= 0x7F;
            bytes.Add(currentByte);

            int result = 0;
            for (int i = 0; i < bytes.Count; i++)
            {
                result |= (int)bytes[i] << ((bytes.Count - 1 - i) * 7);
            }

            return result;
        }

        public int Position { get; set; }

        public byte[] Bytes { get; private set; }

        public bool CanRead
        {
            get
            {
                return Position < Bytes.Length;
            }
        }
    }
}
