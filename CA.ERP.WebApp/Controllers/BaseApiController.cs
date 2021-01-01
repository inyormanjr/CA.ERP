using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController:ControllerBase
    {
        public BaseApiController()
        {
            SetupMimeTypes();
        }

        private void SetupMimeTypes()
        {
            MimeTypes.Add("pdf", "application/pdf");
        }

        protected Dictionary<string, string> MimeTypes = new Dictionary<string, string>();
        protected ReportResult GenerateReport(string path, RenderType renderType = RenderType.Pdf, Dictionary<string,string> parameters = null, Dictionary<string, object> dataSources = null)
        {
            
            LocalReport localReport = new LocalReport(path);
            if (dataSources != null)
            {
                foreach (var dataSource in dataSources)
                {
                    localReport.AddDataSource(dataSource.Key, dataSource.Value);
                }
            }
            
            return localReport.Execute(renderType, 1, parameters);

        }
    }
}
