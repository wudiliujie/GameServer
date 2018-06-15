using System;

namespace ETModel
{
    public class MessageAttribute : Attribute
    {
        public ushort Opcode { get; }

        public MessageAttribute(ushort opcode)
        {
            this.Opcode = opcode;
        }
        public MessageAttribute(MSG opcode)
        {
            this.Opcode = Convert.ToUInt16(opcode);
        }
    }
}