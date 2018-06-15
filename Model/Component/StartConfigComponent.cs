using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ETModel
{
    [ObjectSystem]
    public class StartConfigComponentSystem : AwakeSystem<StartConfigComponent, string, int>
    {
        public override void Awake(StartConfigComponent self, string a, int b)
        {
            self.Awake(a, b);
        }
    }

    public class StartConfigComponent : Component
    {
        private Dictionary<int, StartConfig> configDict;

        public StartConfig StartConfig { get; private set; }

        public StartConfig DBConfig { get; private set; }

        public StartConfig RealmConfig { get; private set; }

        public StartConfig LocationConfig { get; private set; }

        public List<StartConfig> MapConfigs { get; private set; }

        public List<StartConfig> GateConfigs { get; private set; }

        public void Awake(string path, int appId)
        {
            this.configDict = new Dictionary<int, StartConfig>();
            this.MapConfigs = new List<StartConfig>();
            this.GateConfigs = new List<StartConfig>();

            try
            {
                var strData = File.ReadAllText(path, encoding: System.Text.Encoding.UTF8);
                JArray jsonArray = JsonConvert.DeserializeObject<JArray>(strData);
                foreach (JObject jsonData in jsonArray)
                {   
                    StartConfig startConfig = new StartConfig();
                    startConfig.AppId = jsonData["AppId"].Value<int>();
                    startConfig.AppType = Enum.Parse<AppType>(jsonData["AppType"].Value<string>(), true);
                    startConfig.ServerIP = jsonData["ServerIP"].Value<string>();
                    var outerConfig = jsonData["OuterConfig"]?.Value<JObject>();
                    if (outerConfig != null)
                    {
                        OuterConfig cfg = new OuterConfig();
                        cfg.Host = outerConfig["Host"].Value<string>();
                        cfg.Port = outerConfig["Port"].Value<int>();
                        cfg.BeginInit();
                        cfg.EndInit();
                        startConfig.AddComponent(cfg);
                    }
                    var innerConfig = jsonData["InnerConfig"]?.Value<JObject>();
                    if (innerConfig != null)
                    {
                        InnerConfig cfg = new InnerConfig();
                        cfg.Host = innerConfig["Host"].Value<string>();
                        cfg.Port = innerConfig["Port"].Value<int>();
                        cfg.BeginInit();
                        cfg.EndInit();
                        startConfig.AddComponent(cfg);
                    }
                    var httpConfig = jsonData["HttpConfig"]?.Value<JObject>();
                    if (httpConfig != null)
                    {
                        HttpConfig cfg = new HttpConfig();
                        cfg.Url = httpConfig["Url"].Value<string>();
                        cfg.AppId = httpConfig["AppId"].Value<int>();
                        cfg.AppKey = httpConfig["AppKey"].Value<string>();
                        cfg.ManagerSystemUrl = httpConfig["ManagerSystemUrl"].Value<string>();
                        cfg.BeginInit();
                        cfg.EndInit();
                        startConfig.AddComponent(cfg);
                    }
                    var dbConfig = jsonData["DBConfig"]?.Value<JObject>();
                    if (dbConfig != null)
                    {
                        DBConfig cfg = new DBConfig();
                        cfg.ConnectionString = dbConfig["ConnectionString"].Value<string>();
                        cfg.DBName = dbConfig["DBName"].Value<string>();
                        cfg.BeginInit();
                        cfg.EndInit();
                        startConfig.AddComponent(cfg);
                    }
                    var clientConfig = jsonData["ClientConfig"]?.Value<JObject>();
                    if (clientConfig != null)
                    {
                        ClientConfig cfg = new ClientConfig();
                        cfg.Host = clientConfig["Host"].Value<string>();
                        cfg.Port = clientConfig["Port"].Value<int>();
                        cfg.BeginInit();
                        cfg.EndInit();
                        startConfig.AddComponent(cfg);
                    }


                    startConfig.BeginInit();
                    startConfig.EndInit();
                    this.configDict.Add(startConfig.AppId, startConfig);
                }
            }
            catch (Exception e)
            {
                Log.Error($"config错误: {path} {e}");
            }

            this.StartConfig = this.Get(appId);
        }

        public StartConfig Get(int id)
        {
            try
            {
                return this.configDict[id];
            }
            catch (Exception e)
            {
                throw new Exception($"not found startconfig: {id}", e);
            }
        }

        public StartConfig[] GetAll()
        {
            return this.configDict.Values.ToArray();
        }

        public int Count
        {
            get
            {
                return this.configDict.Count;
            }
        }
    }
}
