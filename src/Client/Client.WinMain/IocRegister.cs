using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

using Afx.Ioc;
using Afx.Utils;
using Client.Common;

namespace Client.WinMain
{
    public static class IocRegister
    {
        const string IOC_REGISTER_FILE = "Config/IocRegister.xml";
        const string DEFAULT_IMPLEMENT_FILE = "Config/DefaultImplement.xml";

        public static void Register()
        {
            IocUtils.LoadDefaultImplement(DEFAULT_IMPLEMENT_FILE);

            LoadIoc();
        }

        private static void LoadIoc()
        {
            var filepath = PathUtils.GetFileFullPath(IOC_REGISTER_FILE);
            if (!File.Exists(filepath)) throw new FileNotFoundException(IOC_REGISTER_FILE + " is not found!", IOC_REGISTER_FILE);

            var container = IocUtils.DefaultContainer;
            container.AddGlobalAop<AopLog>();
            XmlDocument doc = new XmlDocument();
            using (var fs = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                doc.Load(fs);
            }

            if (doc.DocumentElement == null) throw new ArgumentException(IOC_REGISTER_FILE + " is error!");

            foreach(XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node is XmlElement && node.Name == "Register")
                {
                    var el = node as XmlElement;
                    var s = el.GetAttribute("base");
                    if (string.IsNullOrEmpty(s)) throw new ArgumentException(IOC_REGISTER_FILE + " is error!");
                    var v = el.GetAttribute("value");
                    if (string.IsNullOrEmpty(v)) throw new ArgumentException(IOC_REGISTER_FILE + " is error!");
                    var arr = s.Split(',');
                    if (arr.Length != 2) if (string.IsNullOrEmpty(s)) throw new ArgumentException(IOC_REGISTER_FILE + " is error!");
                    string name = arr[0].Trim();
                    string nametype = arr[1].Trim();
                    if (string.IsNullOrEmpty(name)) throw new ArgumentException(IOC_REGISTER_FILE + " is error!");
                    if (string.IsNullOrEmpty(nametype)) throw new ArgumentException(IOC_REGISTER_FILE + " is error!");
                    var baseAssembly = Assembly.Load(nametype);
                    var baseType = baseAssembly.GetType(name, true);
                    var classAssembly = Assembly.Load(v);
                    container.Register(baseType, classAssembly);
                }
            }
        }

    }
}
