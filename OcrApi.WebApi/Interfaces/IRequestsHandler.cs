using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OcrApi.WebApi.Models;
using System;

namespace OcrApi.WebApi.Interfaces
{
    public interface IRequestsHandler
    {
        JsonResult JsonHandle(HttpRequest request, HttpResponse response, Func<JsonReturn> action);
    }
}
