using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Fishs.Components
{
    public class UnitManageComponent : Component
    {
        private readonly Dictionary<long, Model.Fishs.Entitys.Unit> unitPool = new Dictionary<long, Model.Fishs.Entitys.Unit>();
        public void Add(Model.Fishs.Entitys.Unit unit)
        {
            this.unitPool.Add(unit.Id, unit);
        }
    }
}
