using Newtonsoft.Json;
using Project.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Project.Service
{
    class FileOpenAndSave
    {
        private readonly string fileNamePrograms = $"{Environment.CurrentDirectory}\\Service\\programDataArray.xml";
        private readonly string fileHistoryRanPrograms;

        public FileOpenAndSave(string path)
        {
            fileHistoryRanPrograms = path;
        }

        public ObservableCollection<ProgramModels> LoadDate()
        {
            var fileExists = File.Exists(fileHistoryRanPrograms);
            if (!fileExists)
            {
                File.CreateText(fileHistoryRanPrograms).Dispose();
                return new ObservableCollection<ProgramModels>();
            }
            using (var reader = File.OpenText(fileHistoryRanPrograms))
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
            using (StreamWriter writer = File.CreateText(fileHistoryRanPrograms))
            {
                string output = JsonConvert.SerializeObject(programModelsList);
                writer.Write(output);
            }
        }

        public void UpdateListProgram(string name)
        {
            XDocument xDocument = XDocument.Load(fileNamePrograms);
            XElement xElement = xDocument.Element("programs");
            if (!xElement.Elements("program").Select(pr => pr.Value).Contains(name))
            {
                xElement.Add(new XElement("program", name));
                xDocument.Save(fileNamePrograms);
            }
        }
    }
}
