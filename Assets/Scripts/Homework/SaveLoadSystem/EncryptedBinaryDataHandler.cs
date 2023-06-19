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
        private readonly EncryptionUtil _encryptionUtil;

        public EncryptedBinaryDataHandler()
        {
            _encryptionUtil = new EncryptionUtil();
        }

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
                    var fileData = File.ReadAllBytes(latestFile.FullName);
                    fileData = _encryptionUtil.Decrypt(fileData);
                    var data = System.Text.Encoding.UTF8.GetString(fileData);
                    _stringData = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
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
            var path = $"{SaveDirectory}/{name}.bin";
            var fileStream = new FileStream(path, FileMode.Create);
            byte[] arrayBytes = System.Text.Encoding.UTF8.GetBytes(stringData);
            arrayBytes = _encryptionUtil.Encrypt(arrayBytes);
            fileStream.Write(arrayBytes, 0, arrayBytes.Length);
            Debug.Log("saved to :"+path);
        }
    }
}