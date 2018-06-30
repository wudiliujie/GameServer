using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class OpcodeTypeComponentSystem : AwakeSystem<OpcodeTypeComponent>
    {
        public override void Awake(OpcodeTypeComponent self)
        {
            self.Awake();
        }
    }

    public static class OpcodeTypeComponentEx
    {
        public static void Awake(this OpcodeTypeComponent self)
        {
            self.Register();
            //Type[] types = DllHelper.GetMonoTypes();
            //foreach (Type type in types)
            //{
            //    object[] attrs = type.GetCustomAttributes(typeof(MessageAttribute), false);
            //    if (attrs.Length == 0)
            //    {
            //        continue;
            //    }

            //    MessageAttribute messageAttribute = attrs[0] as MessageAttribute;
            //    if (messageAttribute == null)
            //    {
            //        continue;
            //    }

            //    self.opcodeTypes.Add(messageAttribute.Opcode, type);
            //}
        }
    }

}
