namespace OcrApi.WebApi.Interfaces
{
    public interface IOcrReader
    {
        string ReadImageBase64(string imageBase64);
    }
}
