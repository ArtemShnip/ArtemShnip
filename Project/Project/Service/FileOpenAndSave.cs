using Newtonsoft.Json;
using Project.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace Project.Service
{
    class FileOpenAndSave
    {
        private readonly string _path;

        public FileOpenAndSave(string path)
        {
            _path = path;
        }

        public ObservableCollection<ProgramModels> LoadDate()
        {
            var fileExists = File.Exists(_path);
            if (!fileExists)
            {
                File.CreateText(_path).Dispose();
                return new ObservableCollection<ProgramModels>();
            }
            using (var reader = File.OpenText(_path))
            {
                var fileText = reader.ReadToEnd();
                if (fileText != "")
                {
                    return JsonConvert.DeserializeObject<ObservableCollection<ProgramModels>>(fileText);
                }
                else
                {
                    return new ObservableCollection<ProgramModels>();
                }
            }
        }

        public void SaveDate(object programModelsList)
        {
            using (StreamWriter writer = File.CreateText(_path))
            {
                string output = JsonConvert.SerializeObject(programModelsList);
                writer.Write(output);
            }
        }

        public void UpdateListProgram(string name)
        {
            string pathArray = $"{Environment.CurrentDirectory}\\SaveData\\programDataArray.xml";
            string[] retVal;
            XmlSerializer formatter = new XmlSerializer(typeof(string[]));
            using (var stream = new FileStream(pathArray, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                retVal = (string[])formatter.Deserialize(stream);
            }
            string[] newArrayProgram = new string[retVal.Length + 1];
            for (int i = 0; i < retVal.Length; i++)
            {
                newArrayProgram[i] = retVal[i];
                if (i == retVal.Length - 1)
                    newArrayProgram[i + 1] = name;
            }
            using (var stream = new FileStream(pathArray, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                formatter.Serialize(stream, newArrayProgram);
            }
        }
    }
}
