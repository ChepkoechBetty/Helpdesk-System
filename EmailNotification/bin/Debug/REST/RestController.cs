using AutoMapper;
using Helpdesk.Data;
using Helpdesk.DataSet;
using Helpdesk.Models;
using Helpdesk.Models.ReportModels;
using Helpdesk.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Helpdesk.REST
{
    [RoutePrefix("api/rest")]
    public class RestController : ApiController
    {
        //Import the dataset that will be used to render the reports
        private readonly IDatabase _database;
        private readonly HelpdeskContext _db;
        protected CurrentUserViewModel CurrentUser;

        public RestController(IDatabase database, HelpdeskContext db)
        {
            _database = database;
            _db = db;
        }


        // GET: Rest
        [Route("getReportbyClient")]
        public List<ReportByClient> GetReportByClient(string fromDate, string toDate) {

            DateTime? dateFrom = null;
            DateTime? dateTo = null;

            //Process the date values
            if (!string.IsNullOrWhiteSpace(fromDate))
            {
                dateFrom = DateTime.ParseExact(fromDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(toDate))
            {
                dateTo = DateTime.ParseExact(toDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            return _database.Fetch<ReportByClient>("EXEC [dbo].[ReportPerClients]@@FromDate=@0, @@ToDate=@1;",dateFrom, dateTo);
        }
        [Route("getReportPerClient")]
        public List<ReportPerClient> GetReportPerClient(string fromDate, string toDate)
        {

            DateTime? dateFrom = null;
            DateTime? dateTo = null;

            //Process the date values
            if (!string.IsNullOrWhiteSpace(fromDate))
            {
                dateFrom = DateTime.ParseExact(fromDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(toDate))
            {
                dateTo = DateTime.ParseExact(toDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            return _database.Fetch<ReportPerClient>("EXEC [dbo].[ReportPerClients]@@FromDate=@0, @@ToDate=@1;", dateFrom, dateTo);
        }

        [Route("getReportbyProblemCause")]
        public List<ReportByProblemCause> GetReportByProblemCause(string fromDate, string toDate)
        {

            DateTime? dateFrom = null;
            DateTime? dateTo = null;

            //Process the date values
            if (!string.IsNullOrWhiteSpace(fromDate))
            {
                dateFrom = DateTime.ParseExact(fromDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(toDate))
            {
                dateTo = DateTime.ParseExact(toDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            return _database.Fetch<ReportByProblemCause>("EXEC [dbo].[ReportByProblemCause]@@FromDate=@0, @@ToDate=@1;", dateFrom, dateTo);
        }

        [Route("getReportbyProduct")]
        public List<ReportByProduct> GetReportByProduct(string fromDate, string toDate)
        {

            DateTime? dateFrom = null;
            DateTime? dateTo = null;

            //Process the date values
            if (!string.IsNullOrWhiteSpace(fromDate))
            {
                dateFrom = DateTime.ParseExact(fromDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(toDate))
            {
                dateTo = DateTime.ParseExact(toDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            return _database.Fetch<ReportByProduct>("EXEC [dbo].[ReportByProducts]@@FromDate=@0, @@ToDate=@1;", dateFrom, dateTo);
        }

        [Route("getReportbyTicketPriority")]
        public List<ReportByTicketPriority> GetReportByTicketPriority(string fromDate, string toDate)
        {

            DateTime? dateFrom = null;
            DateTime? dateTo = null;

            //Process the date values
            if (!string.IsNullOrWhiteSpace(fromDate))
            {
                dateFrom = DateTime.ParseExact(fromDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(toDate))
            {
                dateTo = DateTime.ParseExact(toDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            return _database.Fetch<ReportByTicketPriority>("EXEC [dbo].[ReportByTicketPriority]@@FromDate=@0, @@ToDate=@1;", dateFrom, dateTo);
        }

        [Route("getReportbyTicketType")]
        public List<ReportByTicketType> GetReportByTicketType(string fromDate, string toDate)
        {

            DateTime? dateFrom = null;
            DateTime? dateTo = null;

            //Process the date values
            if (!string.IsNullOrWhiteSpace(fromDate))
            {
                dateFrom = DateTime.ParseExact(fromDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(toDate))
            {
                dateTo = DateTime.ParseExact(toDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            return _database.Fetch<ReportByTicketType>("EXEC [dbo].[ReportByTicketTypes]@@FromDate=@0, @@ToDate=@1;", dateFrom, dateTo);
        }
        [Route("getCientsReport")]
        public List<ClientsReport> getCientsReport(string fromDate, string toDate)
        {

            DateTime? dateFrom = null;
            DateTime? dateTo = null;
            int? clientId;
            //int ProdId = 0;
            //if (!string.IsNullOrWhiteSpace(ProductId))
            //{
            //    ProdId = _db.Products.Where(x => x.Name == ProductId).Select(x => x.Id).FirstOrDefault();
            //}
            CurrentUser = Mapper.Map<CurrentUserViewModel>(new ApplicationUserManager(new UserStore<User>(new HelpdeskContext())).FindById(User.Identity.GetUserId()));
            clientId = CurrentUser.ClientId;
            //int?BranchId = CurrentUser.BranchId;

            //Process the date values
            if (!string.IsNullOrWhiteSpace(fromDate))
            {
                dateFrom = DateTime.ParseExact(fromDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(toDate))
            {
                dateTo = DateTime.ParseExact(toDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            return _database.Fetch<ClientsReport>("EXEC [dbo].[ClientsReport]@@FromDate=@0, @@ToDate=@1,@@ClientId=@2;", dateFrom, dateTo, clientId);
        }
        [Route("getTicketsReport")]
        public List<TicketsReport> getTicketsReport(string fromDate, string toDate)
        {

            DateTime? dateFrom = null;
            DateTime? dateTo = null;

                //Process the date values
                if (!string.IsNullOrWhiteSpace(fromDate))
            {
                dateFrom = DateTime.ParseExact(fromDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(toDate))
            {
                dateTo = DateTime.ParseExact(toDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            return _database.Fetch<TicketsReport>("EXEC [dbo].[TicketsReport]@@FromDate=@0, @@ToDate=@1;", dateFrom, dateTo);
        }
        [Route("getAllocationsReport")]
        public List<AllocationsReport> getAllocationsReport(string fromDate, string toDate)
        {

            DateTime? dateFrom = null;
            DateTime? dateTo = null;

            //Process the date values
            if (!string.IsNullOrWhiteSpace(fromDate))
            {
                dateFrom = DateTime.ParseExact(fromDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(toDate))
            {
                dateTo = DateTime.ParseExact(toDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            return _database.Fetch<AllocationsReport>("EXEC [dbo].[AllocationsReport]@@FromDate=@0, @@ToDate=@1;", dateFrom, dateTo);
        }
    }
}