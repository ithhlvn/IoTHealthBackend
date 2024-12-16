using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.Loggings
{
    public class GlobalExeption : IExceptionFilter
    {
        public void WriteLogFile(string Log)
        {
            try
            {
                FileStream _stream = null;
                StreamWriter sw = null;
                string PathFolder = Environment.CurrentDirectory;//thu mục của ứng dụng cài đặt
                PathFolder = PathFolder + "\\Logs\\" + string.Format("{0:yyyyMMdd}", DateTime.Now);
                try
                {
                    if (!Directory.Exists(PathFolder))
                        Directory.CreateDirectory(PathFolder);
                    string Path = PathFolder + "/IOTBE.log";
                    if (!File.Exists(Path))
                        _stream = File.Create(Path);
                    else
                    {
                        FileInfo file = new FileInfo(Path);
                        if (file.Length > 20000000)//max 20M
                        {
                            file.Delete();
                            _stream = File.Create(Path);
                        }
                        else
                            _stream = new FileStream(Path, FileMode.Append);
                    }
                    sw = new StreamWriter(_stream);
                    sw.Write("==========>>>" + string.Format("{0:dd/MM/yyyy HH:mm:ss.ff}", DateTime.Now) + ":" + Environment.NewLine + Log + Environment.NewLine);
                    sw.Flush();
                }
                catch
                {
                }
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
                if (_stream != null)
                {
                    _stream.Close();
                    _stream.Dispose();
                }
            }
            catch
            {
            }
        }
        public void OnException(ExceptionContext context)
        {
            WriteLogFile($"Message: {context.Exception.Message}\nStackTrace: {context.Exception.StackTrace}");
            //ImEmr.Libs.Base.Sys.Log(new Exception($"Message: {context.Exception.Message}\n StackTrace: {context.Exception.StackTrace}"));
        }
    }
}
