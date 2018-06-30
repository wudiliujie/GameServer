using Google.Protobuf;
using System;

namespace ETModel
{

    public class OpcodeTypeComponent : Component
    {
        private readonly DoubleMap<ushort, Type> opcodeTypes = new DoubleMap<ushort, Type>();

        private readonly DoubleMap<ushort, Func<IMessage>> opcodeCreates = new DoubleMap<ushort, Func<IMessage>>();

        public ushort GetOpcode(Type type)
        {
            return this.opcodeTypes.GetKeyByValue(type);
        }

        public Type GetType(ushort opcode)
        {
            return this.opcodeTypes.GetValueByKey(opcode);
        }
        public IMessage GetNewMessage(ushort opcode)
        {
            var f = opcodeCreates.GetValueByKey(opcode);
            if (f != null)
            {
                return f();
            }
            Log.Debug("opcode:" + opcode + "不存在");
            return null;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();
        }
        public void RegisterType(ushort opcode, Type t, Func<IMessage> func)
        {
            opcodeTypes.Add(opcode, t);
            opcodeCreates.Add(opcode, func);
        }
    }
}