using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Tool
{
    public class Writer
    {
        public StringBuilder m_sb = new StringBuilder();
        string m_Prev = "";
        public void WriteLine(string str)
        {
            if (str == "public:")
            {
                m_sb.AppendLine(str);
                return;
            }
            if (str == "}" || str == "};")
            {
                RemovePrev();
            }
            m_sb.AppendLine(m_Prev + str);
            if (str.EndsWith("{"))
            {
                AddPrev();
            }
        }
        public void WriteLine(string str, params object[] args)
        {
            m_sb.AppendLine(m_Prev + string.Format(str, args));
            if (str.EndsWith("{"))
            {
                AddPrev();
            }
        }
        public Writer AddPrev()
        {
            m_Prev += "\t";
            return this;
        }
        public void RemovePrev()
        {
            if (m_Prev.Length > 0)
            {
                m_Prev = m_Prev.Remove(0, 1);
            }
        }
        public override string ToString()
        {
            return m_sb.ToString();
        }
        public void Clear()
        {
            m_sb.Clear();
        }
    }
}
