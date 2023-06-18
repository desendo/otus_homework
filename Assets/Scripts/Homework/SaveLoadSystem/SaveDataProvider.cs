using System;
using System.Collections.Generic;

namespace Homework
{
    public class SaveDataProvider : ISaveDataProvider
    {
        private readonly ISaveDataHandler _dataHandler;

        public SaveDataProvider(ISaveDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public bool TryGetData<T>(out T data)
        {
            data = (T) _dataHandler.GetData<T>();
            return data != null;
        }

        public void SetData<T>(T data)
        {
            _dataHandler.SetData<T>(data);
        }
    }
}