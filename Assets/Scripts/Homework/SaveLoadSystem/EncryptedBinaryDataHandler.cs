using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Homework
{
    public class EncryptedBinaryDataHandler : ISaveDataHandler
    {
        private static string SaveDirectory => Application.persistentDataPath + "/saves";
        private Dictionary<string, string> _stringData = new Dictionary<string, string>();

        public void LoadData()
        {
            if(Directory.Exists(SaveDirectory))
            {
                var directory = new DirectoryInfo(SaveDirectory);
                var files = directory.GetFiles();
                if(files.Length == 0)
                    return;

                var latestFile = files.OrderByDescending(f => f.LastWriteTime).FirstOrDefault();

                if (latestFile != null)
                {
                    //здесь должна быть дешифровка и десериализация из бинари 
                    var sr = new StreamReader(latestFile.FullName);
                    var content = sr.ReadToEnd();
                    _stringData = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                }
            }
        }
        public object GetData<T>()
        {
            var id = typeof(T).ToString();
            if(_stringData.ContainsKey(id))
            {
                var data = _stringData[id];

                return JsonConvert.DeserializeObject<T>(data);
            }

            return null;
        }

        public void SetData<T>(T data)
        {
            var id = data.GetType().ToString();
            if (_stringData.ContainsKey(id))
                _stringData[id] = JsonConvert.SerializeObject(data);
            else
                _stringData.Add(id, JsonConvert.SerializeObject(data));


        }
        public void SaveData()
        {
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);

            var name = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
            var directory = new DirectoryInfo(SaveDirectory);
            directory.Create();
            var stringData = JsonConvert.SerializeObject(_stringData);
            //здесь должна быть зашифровка и сериализация в бинари

            var path = $"{SaveDirectory}/{name}.data";
            var sw = File.CreateText(path);
            sw.Write(stringData);
            sw.Close();
            Debug.Log("saved to :"+path);
        }
    }
}