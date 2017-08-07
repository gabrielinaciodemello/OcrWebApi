using OcrApi.WebApi.Interfaces;
using System;
using System.IO;

namespace OcrApi.WebApi.Utils
{
    public class FileLog: ILog
    {
        private readonly string _filePath;

        public FileLog(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(Exception ex)
        {
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Dispose();
            }
            using (StreamWriter writer = File.AppendText(_filePath))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace + "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }
    }
}
