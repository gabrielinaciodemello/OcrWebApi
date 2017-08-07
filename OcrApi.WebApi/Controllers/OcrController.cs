using Microsoft.AspNetCore.Mvc;
using OcrApi.WebApi.Interfaces;
using OcrApi.WebApi.Models;
using OcrApi.WebApi.Models.OcrController;
using System;
using System.Linq;

namespace OcrApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class OcrController : Controller
    {
        private readonly IRequestsHandler _requestHandler;
        private readonly IOcrReader _ocrReader;

        public OcrController(IRequestsHandler requestHandler, IOcrReader ocrReader)
        {
            _requestHandler = requestHandler;
            _ocrReader = ocrReader;
        }

        // POST api/ocr
        [HttpPost]
        public JsonResult Post([FromBody]PostModel model)
        {
            return _requestHandler.JsonHandle(Request, Response, () =>
            {
                var resultLines = _ocrReader.ReadImageBase64(model.ImageBase64).Split(new string[] { "\r\n" }, System.StringSplitOptions.None).ToList<string>();
                return new JsonReturn { Result = resultLines };
            });
        }
    }
}
