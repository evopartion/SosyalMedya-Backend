using Core.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Utilities.Result.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        // kalıtım verince ampülden ctorları hızlıca oluşturabilirsin
        public DataResult(T data,bool success) : base(success)
        {
            Data = data;
        }

        public DataResult(T data,bool success, string message) : base(success, message)
        {
            // Üst sınıfa ek olarak eklenecek veriyi de ctora ekle
            Data = data;
        }

        public T Data { get; }
    }
}
