using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Common
{
    public static class Utils
    {
        private const long KB_SIZE = 1L << 10;
        private const long MB_SIZE = 1L << 20;
        private const long GB_SIZE = 1L << 30;
        private const long TB_SIZE = 1L << 40;
        private const long PB_SIZE = 1L << 50;
        private const long EB_SIZE = 1L << 60;

        public static string ToAutoUnit(long size)
        {
            string s = string.Empty;
            if (size < KB_SIZE)
            {
                s = string.Format("{0}B", size);
            }
            else if (size < MB_SIZE)
            {
                s = string.Format("{0:f2}KB", size * 1.0M / KB_SIZE);
            }
            else if (size < GB_SIZE)
            {
                s = string.Format("{0:f2}MB", size * 1.0M / MB_SIZE);
            }
            else if (size < TB_SIZE)
            {
                s = string.Format("{0:f2}GB", size * 1.0M / GB_SIZE);
            }
            else if (size < PB_SIZE)
            {
                s = string.Format("{0:f2}TB", size * 1.0M / TB_SIZE);
            }
            else if (size < EB_SIZE)
            {
                s = string.Format("{0:f2}PB", size * 1.0M / PB_SIZE);
            }
            else
            {
                s = string.Format("{0:f2}EB", size * 1.0M / EB_SIZE);
            }

            return s;
        }

        public static string Combine(string path, string child, params string[] param)
        {
            StringBuilder str = new StringBuilder();
            str.Append(path ?? "");
            if (!string.IsNullOrEmpty(child) && child != "\\")
            {
                bool isEnd = str.Length > 0 && str[str.Length - 1] == '\\';
                bool isStart = child[0] == '\\';
                if (isEnd && isStart) str.Append(child.Substring(1));
                else if (!isEnd && !isStart) str.Append("\\" + child);
                else str.Append(child);
            }
            if(param != null && param.Length > 0)
            {
                foreach(var s in param)
                {
                    if (!string.IsNullOrEmpty(s) && s != "\\")
                    {
                        bool isEnd = str.Length > 0 && str[str.Length - 1] == '\\';
                        bool isStart = s[0] == '\\';
                        if (isEnd && isStart) str.Append(s.Substring(1));
                        else if (!isEnd && !isStart) str.Append("\\" + s);
                        else str.Append(s);
                    }
                }
            }

            return str.ToString();
        }

        public static string GetParent(string path)
        {
            string s = path;
            if (s.EndsWith("\\")) s = s.TrimEnd('\\');
            int i = s.LastIndexOf('\\');
            if (i > 0) s = s.Substring(0, i);
            else s = "";

            if (!s.EndsWith("\\")) s = s + "\\";

            return s;
        }

        public static string GetName(string path)
        {
            string s = (path ?? "").Trim('\\');
            int i = s.LastIndexOf('\\');
            if (i >= 0 && s.Length > 0) s = s.Substring(i + 1);

            
            return s;
        }

        public static string GetRelative(string full, string parent)
        {
            string s = "";
            if (!string.IsNullOrEmpty(parent) && !string.IsNullOrEmpty(full))
            {
                full = full.TrimEnd('\\');
                parent = parent.TrimEnd('\\');
                if(string.Compare(full, parent, true) == 0)
                {
                    s = "\\";
                }
                if(full.Length > parent.Length)
                {
                    s = full.Substring(parent.Length);
                    if (!s.StartsWith("\\")) s = "\\" + s;
                }
            }

            return s;
        }

        public static string FormatDirectory(string dir)
        {
            string s = dir;
            if (string.IsNullOrEmpty(s)) s = "\\";
            else if (!s.StartsWith("\\")) s = "\\" + s;
            if (s != "\\") s = s.TrimEnd('\\');

            s = s.ToLower();

            return s;
        }

        public static string FormatName(string name)
        {
            return (name ?? "").ToLower();
        }
    }
}
