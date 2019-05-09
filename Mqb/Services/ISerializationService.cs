using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Services
{
    public interface ISerializationService : IService
    {
        string Serialize<T>(T item);
        T Deserialize<T>(string data);
    }
}
