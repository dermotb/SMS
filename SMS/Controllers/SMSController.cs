using SMS.Models;
using SMSGateway.Models;
using System;
using System.IO;
using System.Web.Http;

namespace SMS.Controllers
{
    public class SMSController : ApiController
    {
        private const int MaxSize = 140;
        private const String LOGFILENAME = @"C:\temp\log.txt";

        SMSContext ctx = new SMSContext();

        // POST /api/SMSGateway/
        public IHttpActionResult PostSendSMS(TextMessage message)                         // message serialised in request body
        {
            if (ModelState.IsValid)
            {
                // log to file
                Log("Sent: " + message.Content + " from " + message.FromNumber + " to " + message.ToNumber);
                LogToDataBase(message);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // log to file, may need to run VS as adminstrator in order to have write access to the file system
        [NonAction]                                                             // not a controller action
        private void Log(String logInfo)
        {
            using (StreamWriter stream = File.AppendText(LOGFILENAME))
            {
                stream.Write(logInfo);
                stream.WriteLine(" " + DateTime.Now);
                stream.Close();
            }
        }

        // log to DB
        [NonAction] 
        private void LogToDataBase(TextMessage msg)
        {
            ctx.texts.Add(msg);
            ctx.SaveChanges();
        }
    }
}
