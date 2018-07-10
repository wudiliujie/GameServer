using System;
using System.Net;
using System.Threading.Tasks;

namespace ETModel
{
	public class BenchmarkComponent: Component
	{
        public Session Session;

        public int k;

		public long time1 = TimeHelper.ClientNow();
	}
}