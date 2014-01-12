using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Hibu.Sam.Concordance.Models;
using Hibu.Sam.Concordance.Parsers;
using Hibu.Sam.Concordance.Utilities;

namespace Hibu.Sam.Concordance.Web.Controllers
{
    public class ConcordanceController : ApiController
    {
        public Task<HttpResponseMessage> PostFormData()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string rootPath = HttpContext.Current.Server.MapPath("~/App_Data");

            MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(rootPath);

            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsFaulted || t.IsCanceled)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }

                    MultipartFileData file = provider.FileData[0];  // interface only provide servcie to single file, otherwise we need to iterate colleciton and return file based colletion
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                    System.IO.FileStream fileStream = System.IO.File.Open(file.LocalFileName, System.IO.FileMode.Open);

                    #region Parse file and return JSON string

                    EnglishParser parser = new EnglishParser(fileStream);
                    Dict dict = new Dict();

                    ParserWord word = null;

                    while ((word = parser.GetNextWord()) != null)
                    {
                        dict.AddWord(word.Letters, word.SentenceNumber);
                    }

                    List<Word> words = dict.GetWords();

                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(words);

                    #endregion

                    fileStream.Close();

                    try
                    {
                        System.IO.File.Delete(file.LocalFileName);
                    }
                    catch (System.Exception error)
                    {
                        //Do log, don't throw;
                        Trace.WriteLine(error.Message);                        
                    }

                    HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    return response;
                });

            return task;
        }
    }
}
