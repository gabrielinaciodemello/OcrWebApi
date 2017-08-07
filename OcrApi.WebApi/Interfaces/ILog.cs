using System;

namespace OcrApi.WebApi.Interfaces
{
    public interface ILog
    {
        void Write(Exception ex);
    }
}
