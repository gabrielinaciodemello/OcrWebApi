using System;
using OcrApi.WebApi.Interfaces;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace OcrApi.WebApi.Services
{
    public class TesseractReader : IOcrReader
    {
        private readonly string _tempImagePath;
        private readonly string _tesseractPath;

        public TesseractReader(string tempImagePath, string tesseractPath)
        {
            _tempImagePath = tempImagePath;
            _tesseractPath = tesseractPath;
        }

        public string ReadImageBase64(string imageBase64)
        {
            var imagePath = "";
            try
            {
                imagePath = SaveImageFromBase64(imageBase64);
                return StartProcess(imagePath);
            }
            finally
            {
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
        }

        private string SaveImageFromBase64(string imageBase64)
        {
            try
            {
                var path = $@"{_tempImagePath}\{Guid.NewGuid()}.jpg";
                using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(imageBase64)))
                {
                    Image image = Image.FromStream(stream);
                    image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                return path;
            }
            catch
            {
                throw new InvalidOperationException("Invalid file");
            }
        }

        private string StartProcess(string imagePath)
        {
            var arguments = $"{imagePath} stdout -l por";
            var processInfo = new ProcessStartInfo
            {
                Arguments = arguments,
                FileName = _tesseractPath,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            var process = new Process
            {
                StartInfo = processInfo
            };
            var start = process.Start();

            var processOut = string.Empty;
            if (start)
            {
                process.PriorityClass = ProcessPriorityClass.BelowNormal;

                while (!process.StandardOutput.EndOfStream)
                {
                    processOut += process.StandardOutput.ReadLine() + Environment.NewLine;
                }
            }

            return processOut;
        }
    }
}
