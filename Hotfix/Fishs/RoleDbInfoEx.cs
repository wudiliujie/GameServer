using ETModel;
using Model.Fishs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs
{
    public static class RoleDbInfoEx
    {
        public static void AddAttr(this RoleDbInfo self, AttrType t, int v)
        {
            self.AttrInts.Add(new AttrInt() { K = (int)t, V = v });
        }
        public static void AddAttr(this RoleDbInfo self, AttrType t, long v)
        {
            self.AttrInts.Add(new AttrInt() { K = (int)t, V = v });
        }
        public static void AddAttr(this RoleDbInfo self, AttrType t, string v)
        {
            self.AttrStrs.Add(new AttrStr() { K = (int)t, V = v });
        }
    }
}
