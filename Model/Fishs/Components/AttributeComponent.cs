using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Fishs.Components
{

    public class AttributeData
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public Int64 Exp { get; set; }
        public Int64 Gold { get; set; }
        public int Atk { get; set; }

    }
    /// <summary>
    /// 属性组件
    /// </summary>
    public class AttributeComponent : Component
    {
        public Dictionary<AttrType, long> TempAttrIntPool = new Dictionary<AttrType, long>();
        public Dictionary<AttrType, string> TempAttrStrPool = new Dictionary<AttrType, string>();
        private string _name;
        private int _level;
        private long _exp;
        private long _gold;
        private int _atk;
        public int Level
        {
            get => _level;
            set
            {
                if (_level != value)
                {
                    TempAttrIntPool[AttrType.Level] = value;
                }
                _level = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    TempAttrStrPool[AttrType.Name] = value;
                }
                _name = value;
            }
        }
        public Int64 Exp
        {
            get => _exp;
            set
            {
                if (_exp != value)
                {
                    TempAttrIntPool[AttrType.Exp] = value;
                }
                _exp = value;
            }
        }
        public Int64 Gold
        {
            get => _gold;
            set
            {
                if (_gold != value)
                {
                    TempAttrIntPool[AttrType.Gold] = value;
                }
                _gold = value;
            }
        }
        public int Atk
        {
            get => _atk;
            set
            {
                if (_atk != value)
                {
                    TempAttrIntPool[AttrType.Atk] = value;
                }
                _atk = value;
            }
        }
    }
}
