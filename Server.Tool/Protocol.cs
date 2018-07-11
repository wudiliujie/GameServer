using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Server.Tool
{
    public class TypeMapping
    {
        public TypeMapping(string cs, string proto, string js)
        {
            CS = cs;
            Proto = proto;
            JS = js;
        }
        public string CS { get; set; }
        public string Proto { get; set; }
        public string JS { get; set; }
    }
    public enum ServerType
    {
        GameServer,
        Web,
        Client,
        Resource
    }
    public class P
    {
        public int i { get; set; }
        public string n { get; set; }
        public string d { get; set; }
        public string t { get; set; }
        /// <summary>
        /// 是否是数组
        /// </summary>
        public bool a { get; set; }
    }
    public class Pack
    {
        public Pack()
        {
            Props = new List<P>();
        }
        public string n { get; set; }
        public string d { get; set; }
        //public string s { get; set; }
        //public string t { get; set; }
        public bool db { get; set; }
        public string pt { get; set; }
        /// <summary>
        /// 是否是资源结构
        /// </summary>
        public bool r { get; set; }
        public List<P> Props { get; set; }
        public string GetName()
        {
            return n;
            //if (string.IsNullOrWhiteSpace(s))
            //{
            //    return n;
            //}
            //return string.Format("{0}2{1}_{2}", s, t, n);
        }
        public string GetPrev()
        {
            return "";
            //if (string.IsNullOrWhiteSpace(s))
            //{
            //    return "";
            //}
            //return string.Format("{0}2{1}", s, t);
        }
    }
    public class Protocol
    {
        List<Pack> m_Struct = new List<Pack>();
        List<Pack> m_Packs = new List<Pack>();
        Dictionary<string, int> m_Ids = new Dictionary<string, int>();
        int m_nMaxId = 0;
        private string m_sFileName = "";
        //Dictionary<ServerType, HashSet<string>> m_Relation = new Dictionary<ServerType, HashSet<string>>();
        Dictionary<string, TypeMapping> m_TypeMapping = new Dictionary<string, TypeMapping>();
        public Protocol()
        {
            //m_Relation.Add(ServerType.GameServer, new HashSet<string>() { "S2C", "C2S", "S2D", "D2S", "S2WEB", "WEB2S","S2L" });
            //m_Relation.Add(ServerType.Client, new HashSet<string>() { "S2C", "C2S", "C2WEB", "WEB2C" });
            //m_Relation.Add(ServerType.Web, new HashSet<string>() { "C2WEB", "WEB2C", "S2WEB", "WEB2S" });
            //m_Relation.Add(ServerType.Resource, new HashSet<string>());

            m_TypeMapping.Add("int", new TypeMapping("int", "int32", "number"));
            m_TypeMapping.Add("long", new TypeMapping("int", "int64", "protobuf.Long"));
            m_TypeMapping.Add("string", new TypeMapping("string", "string", "string"));
            m_TypeMapping.Add("bytes", new TypeMapping("byte[]", "bytes", "Uint32Array"));
            m_TypeMapping.Add("byte", new TypeMapping("bytes", "bytes", "Uint32Array"));
            m_TypeMapping.Add("bool", new TypeMapping("bool", "bool", "boolean"));

        }

        public void Read(string fileName)
        {
            m_sFileName = fileName;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            XmlNode node = xmlDoc.SelectSingleNode("pack");
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == "ids")
                {
                    foreach (XmlNode id in item.ChildNodes)
                    {
                        int idx = Convert.ToInt32(id.Attributes["i"].Value);
                        m_Ids.Add(id.Attributes["n"].Value, idx);
                        if (idx > m_nMaxId)
                        {
                            m_nMaxId = idx;
                        }
                    }
                    continue;
                }

                Pack model = new Pack();
                model.d = item.Attributes["d"].Value;
                model.n = item.Attributes["n"].Value;
                //model.s = item.Attributes["s"]?.Value ?? "";
                //model.t = item.Attributes["t"]?.Value ?? "";
                model.db = !(item.Attributes["db"]?.Value == null);
                model.r = !(item.Attributes["r"]?.Value == null);
                model.pt = item.Attributes["pt"]?.Value ?? "";
                foreach (XmlNode prop in item.ChildNodes)
                {
                    var p = new P();
                    p.a = !(prop.Attributes["a"]?.Value == null);
                    p.d = prop.Attributes["d"].Value;
                    p.i = Convert.ToInt32(prop.Attributes["i"].Value);
                    p.n = prop.Attributes["n"].Value;
                    p.t = prop.Attributes["t"].Value;
                    model.Props.Add(p);
                }
                if (item.Name == "s")
                {
                    m_Struct.Add(model);
                }
                else if (item.Name == "p")
                {
                    m_Packs.Add(model);
                }
            }
        }
        /// <summary>
        /// 分配编号
        /// </summary>
        public void AllotId()
        {
            foreach (var item in m_Packs)
            {
                if (!m_Ids.ContainsKey(item.GetName()))
                {
                    m_Ids.Add(item.GetName(), GetNewId());
                }
            }
            //保存xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_sFileName);
            XmlNode node = xmlDoc.SelectSingleNode("/pack/ids");
            node.RemoveAll();
            foreach (var item in m_Ids)
            {
                var c = xmlDoc.CreateElement("id");
                c.SetAttribute("i", item.Value.ToString());
                c.SetAttribute("n", item.Key);
                node.AppendChild(c);
            }
            xmlDoc.Save(m_sFileName);

        }
        private int GetNewId()
        {
            m_nMaxId++;
            return m_nMaxId;
        }

        /// <summary>
        /// 生成proto文件
        /// </summary>
        /// <param name="fileName"></param>
        public void GenerateProto(string fileName, ServerType serverType)
        {
            Writer writer = new Writer();
            writer.WriteLine("syntax = \"proto3\";");
            writer.WriteLine("package ETModel;");
            if (serverType != ServerType.Resource)
            {
                writer.WriteLine("enum MSG{");
                writer.WriteLine("_Default = 0;");

                var ids = GetNeedIds(serverType);
                foreach (var item in ids)
                {
                    var pack = GetPackByName(item);
                    if (pack != null)
                    {
                        writer.WriteLine("_{0} = {1}; //{2}", item, m_Ids[item], pack.d);
                    }
                }
                writer.WriteLine("}");
            }

            //写入需要的结构体
            var list = GetNeedStruct(serverType);
            list.AddRange(GetNeedPacket(serverType));
            foreach (var item in list)
            {
                writer.WriteLine("//{0}", item.d);
                writer.WriteLine("message {0}{{", item.GetName());
                foreach (var p in item.Props)
                {
                    writer.WriteLine("{0} {1} = {2};//{3}", GetProtoType(p.t, p.a), p.n, p.i, p.d);
                }

                if (item.pt == "IRequest")
                {
                    writer.WriteLine("int32 RpcId = 99;");
                }
                if (item.pt == "IActorRequest")
                {
                    writer.WriteLine("int32 RpcId = 99;");
                    writer.WriteLine("int64 ActorId = 100;");
                }

                if (item.pt == "IResponse" || item.pt== "IActorResponse")
                {
                    writer.WriteLine("int32 Tag = 98;");
                    writer.WriteLine("int32 RpcId = 99;");
                    writer.WriteLine("string Message = 100;");
                }

                writer.WriteLine("}");
                writer.WriteLine("");
            }
            File.WriteAllText(fileName, writer.ToString(), Encoding.UTF8);
        }
        public void GenerateTS(string fileName, ServerType type)
        {
            Writer writer = new Writer();
            writer.WriteLine("enum MSG{");
            var ids = GetNeedIds(type);
            foreach (var item in ids)
            {
                var pack = GetPackByName(item);
                if (pack != null)
                {
                    writer.WriteLine("{0} = {1}, //{2}", item, m_Ids[item], pack.d);
                }
            }
            writer.WriteLine("}");
            writer.WriteLine("");
            var list = GetNeedStruct(type);
            list.AddRange(GetNeedPacket(type));
            foreach (var item in list)
            {
                writer.WriteLine("class {0} {{", item.GetName());
                foreach (var p in item.Props)
                {
                    string prev = "\"";
                    if (!m_TypeMapping.ContainsKey(p.t))
                    {
                        prev = "";
                    }
                    if (p.a)
                    {
                        //writer.WriteLine("@protobuf.MapField.d({0},\"string\", {2}{1}{2})", p.i, GetProtoType(p.t, false), prev);
                        writer.WriteLine("public {0} :Array<{1}>;", p.n, GetJSType(p.t, p.a));
                    }
                    else
                    {
                        //writer.WriteLine("@protobuf.Field.d({0}, {2}{1}{2})", p.i, GetProtoType(p.t, p.a), prev);
                        writer.WriteLine("public {0}: {1};", p.n, GetJSType(p.t, p.a));
                    }

                }
                writer.WriteLine("}");
            }

            File.WriteAllText(fileName, writer.ToString(), Encoding.UTF8);
        }

        public void GenerateGameProtoEx(string fileName)
        {
            Writer writer = new Writer();
            writer.WriteLine("using System;");
            writer.WriteLine("namespace ETModel");
            writer.WriteLine("{");
            foreach (var item in m_Packs)
            {
                if (string.IsNullOrWhiteSpace(item.pt))
                {
                    continue;
                }
                writer.WriteLine("public partial class {0} : {1}", item.n, item.pt);
                writer.WriteLine("{");

                writer.WriteLine("}");
            }

            writer.WriteLine("public static class RegisterClass");
            writer.WriteLine("{");
            writer.WriteLine("public static void Register(this OpcodeTypeComponent self)");
            writer.WriteLine("{");
            foreach (var item in m_Packs)
            {
                writer.WriteLine("self.RegisterType({0}, typeof({1}), () => {{ return new {1}(); }});", m_Ids[item.GetName()], item.GetName());
            }

            writer.WriteLine("}");
            writer.WriteLine("}");

            writer.WriteLine("}");
            File.WriteAllText(fileName, writer.ToString(), Encoding.UTF8);
        }


        private List<string> GetNeedIds(ServerType type)
        {
            List<Pack> list = GetNeedPacket(type);
            List<string> result = new List<string>();
            foreach (var item in list)
            {
                if (m_Ids.ContainsKey(item.GetName()))
                {
                    result.Add(item.GetName());
                }
            }
            return result;
        }
        /// <summary>
        /// 获取需要的Struct
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<Pack> GetNeedStruct(ServerType type)
        {
            List<Pack> list = GetNeedPacket(type);
            List<Pack> result = new List<Pack>();
            foreach (var item in list)
            {
                foreach (var prop in item.Props)
                {
                    foreach (var @struct in m_Struct)
                    {
                        if (prop.t == @struct.n)
                        {

                            bool find = false;
                            foreach (var other in result)
                            {
                                if (@struct.n == other.n)
                                {
                                    find = true;
                                }
                            }
                            if (!find)
                            {
                                result.Add(@struct);
                            }
                    
                        }
                    }
                }
            }
            foreach (var item in m_Struct)
            {
                if (item.db && type == ServerType.GameServer || item.db && type == ServerType.Web || item.r && type == ServerType.Resource)
                {
                    bool find = false;
                    foreach (var other in result)
                    {
                        if (item.n == other.n)
                        {
                            find = true;
                        }
                    }
                    if (!find)
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }
        private List<Pack> GetNeedPacket(ServerType type)
        {
            List<Pack> list = new List<Pack>();
            //var hash = m_Relation[type];

            foreach (var item in m_Packs)
            {
                //if (hash.Contains(item.GetPrev())) //需要生成的包
                //{
                list.Add(item);
                //}
            }
            return list;
        }
        public Pack GetPackByName(string name)
        {
            foreach (var item in m_Packs)
            {
                if (item.GetName() == name)
                {
                    return item;
                }
            }
            return null;
        }
        private string GetProtoType(string type, bool isArray)
        {
            string outtype = type;
            if (m_TypeMapping.ContainsKey(type))
            {
                outtype = m_TypeMapping[type].Proto;
            }
            if (isArray)
            {
                outtype = "repeated " + outtype;
            }
            return outtype;
        }
        private string GetJSType(string type, bool isArray)
        {
            string outtype = type;
            if (m_TypeMapping.ContainsKey(type))
            {
                outtype = m_TypeMapping[type].JS;
            }
            return outtype;
        }
    }
}
