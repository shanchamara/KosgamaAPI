using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using CommonStockManagementDatabase.Model;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using WkHtmlToPdfDotNet;
using CommonStockManagementServices.Services;
using WkHtmlToPdfDotNet.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.Design;

namespace CommonStockManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReportController(IConverter converter, GRNService gRNService, GINService gINServices, IPOSService iPOSService, IReportService reportService, SRNService sRNService, POSReturnService pOSReturnService, IMemoryCache cache) : ControllerBase
    {
        private const string API_ROUTE_NAME = "/api/Report";
        private readonly GRNService _GrnServices = gRNService;
        private readonly GINService _GINServices = gINServices;
        private readonly IPOSService _iPOSService = iPOSService;
        private readonly IReportService _reportService = reportService;
        private readonly SRNService _SRNService = sRNService;
        private readonly POSReturnService _pOSReturnService = pOSReturnService;
        private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        public IConverter _converter = converter;

        #region Stock Managemnet 

        [HttpGet]
        [Route(API_ROUTE_NAME + "/PrintGRN/{id}")]
        public IActionResult PrintreportGRN(int ID)
        {
            byte[] reurnarray = GRNPDfAsync(ID);
            return File(reurnarray, "application/pdf");
        }
        public byte[] GRNPDfAsync(int id)
        {
            var modelHead = _GrnServices.GetDetailsByGrnHeadIdForPrint(id);
            var getcompany = _reportService.GetCompanyDetailsForPrint(modelHead.LocationId ?? 0);

            var modelBody = _GrnServices.GetByGRNBodiesId(id);
            //var result = await modelBody.ToList();

            StringBuilder TableText = new();
            decimal tot = 0m;
            decimal Discount = 0m;

            decimal Gross = 0m;
            int countRows = 0;
            foreach (var obj in modelBody)
            {
                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.Item_name + "</td>" +
                    "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' > " + obj.Code + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.UnitCost, 2).ToString("#,0.00") + "  </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Qty, 2).ToString("#,0.00") + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Discount, 2).ToString("#,0.00") + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Cost, 2).ToString("#,0.00") + " </td></tr> ";
                TableText.Append(rownew);
                tot = (decimal)modelHead.Total;
                Discount = (decimal)modelHead.Discount;
                Gross = (decimal)modelHead.Gross;

                countRows++;
            }
            var Total = tot.ToString("#,0.00");
            var TotalDiscount = Discount.ToString("#,0.00");
            var TotalGross = Gross.ToString("#,0.00");

            string tablestyle = "  html {     margin-top:-30px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

            var html2 = $@"<html lang='en''>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <title> Good Received Note</title>
    <style type='text/css'>
       {tablestyle}
    </style>
</head>
<body>

    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        <div style='padding: 20px 40px;'>
            <div style='display: flex;flex-wrap: wrap;'>
                 <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
             <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName} </b></h2></td>
            
             </tr>
            <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1} / {getcompany.TelPhone2}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'><h4>Good Received Note</h4></td>
            
             </tr>
          
          
           </tbody>
           </table>
                
            </div>
               
        </div>

        <div style='padding: 16px;'>
            <div style='display: flex;flex-wrap: wrap;'>
              <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
           
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>GRN NO : {modelHead.Id}</td>
            
              <td style='text-align:right;font-size:15px;'>Supplier Name </td>
             <td colspan='2' style='font-size:12px;text-align:left;'>: {modelHead.Supplier}</td>
            
            
             </tr>
           <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Date : {modelHead.Date:yyyy/MMM/dd}</td>
            
              
             
             </tr>
             <tr>
                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime()}</td>
            
             </tr>

           </tbody>
           </table>
            
             
            </div>
        </div>

        <div style='padding: 16px;'>
            <h3 style='font-size: 20px; margin-bottom: 08px; color: black;' >Ordered Items</h3>
            <br>
            <table style='background-color: #fff;width: 100%;border-collapse: collapse;box-shadow: 0px 0px 5px 0.5px gray;'>
                <thead>
                    <tr style='border: 1px solid #111;background-color: #f2f2f2;'>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 40%;'>Product</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Code</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Cost</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Quantity</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Discount</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >GrandTotal</th>
                    </tr>
                </thead>
                <tbody>
                    
                      {TableText}
                    
                    <tr>
                        <td style='vertical-align: middle !important;text-align: center;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' colspan='5'>Sub Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {Total}</td>
                    </tr>
                     <tr>
                        <td colspan='5' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Discount</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalDiscount}</td>
                    </tr>
                    <tr>
                        <td colspan='5' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Grand Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalGross}</td>
                    </tr>
                </tbody>
            </table>
            <br>
            <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Prepared By : {modelHead.Created}</td>
            
           
             <td colspan='3' style='font-size:18px;text-align:left;'>Authorized Person : ......................</td>
            
             </tr>
           <tr>
            <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Count  : {countRows}</td>
            
             
            <td colspan='3' style='font-size:18px;text-align:left;'> Authorized Date  : ......................</td>
            
             </tr>
           </tbody>
           </table>
            
        </div>

          
    </div>      

</body>
</html>";
            GlobalSettings globalSettings = new()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Right = 0, Left = 0 }
            };
            ObjectSettings objectSettings = new()
            {
                PagesCount = true,
                HtmlContent = html2
            };
            WebSettings webSettings = new()
            {
                DefaultEncoding = "utf-8"
            };
            HeaderSettings headerSettings = new()
            {
                FontSize = 15,
                FontName = "times new roman"
            };
            //headerSettings.Right = "Page [page] of [toPage]";
            //headerSettings.Line = true;
            FooterSettings footerSettings1 = new();
            FooterSettings footerSettings = footerSettings1;
            footerSettings.FontSize = 8;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788";

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
            return pdfData;
        }


        [HttpGet]
        [Route(API_ROUTE_NAME + "/PrintSRN/{id}")]
        public IActionResult PrintreportSRN(int ID)
        {
            byte[] reurnarray = SRNPDfAsync(ID);
            return File(reurnarray, "application/pdf");
        }
        public byte[] SRNPDfAsync(int id)
        {
            var CompanyID = Convert.ToInt32(User.Identities.First().Claims.Single(s => s.Type == "CompanyId").Value);
            var getcompany = _reportService.GetCompanyDetailsForPrint(CompanyID);
            var modelHead = _SRNService.GetDetailsBySRNHeadIdForPrint(id);
            var modelBody = _SRNService.GetBySRNBodiesId(id);
            //var result = await modelBody.ToList();

            StringBuilder TableText = new();
            decimal tot = 0m;
            decimal Discount = 0m;

            decimal Gross = 0m;
            int countRows = 0;
            foreach (var obj in modelBody)
            {
                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.ItemName + "</td>" +
                    "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' > " + obj.Code + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.UnitCost, 2).ToString("#,0.00") + "  </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Qty, 2).ToString("#,0.00") + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Discount, 2).ToString("#,0.00") + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Cost, 2).ToString("#,0.00") + " </td></tr> ";
                TableText.Append(rownew);
                tot = (decimal)modelHead.Total;
                Discount = (decimal)modelHead.Discount;
                Gross = (decimal)modelHead.Gross;

                countRows++;
            }
            var Total = tot.ToString("#,0.00");
            var TotalDiscount = Discount.ToString("#,0.00");
            var TotalGross = Gross.ToString("#,0.00");

            string tablestyle = "  html {     margin-top:-30px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

            var html2 = $@"<html lang='en''>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <title> Supplier Return Note</title>
    <style type='text/css'>
       {tablestyle}
    </style>
</head>
<body>

    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        <div style='padding: 20px 40px;'>
            <div style='display: flex;flex-wrap: wrap;'>
                 <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
             <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName} </b></h2></td>
            
             </tr>
            <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1} / {getcompany.TelPhone2}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'><h4>Supplier Return Note</h4></td>
            
             </tr>
          
          
           </tbody>
           </table>
                
            </div>
               
        </div>

        <div style='padding: 16px;'>
            <div style='display: flex;flex-wrap: wrap;'>
              <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
           
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>SRN NO : {modelHead.ID}</td>
            
              <td style='text-align:right;font-size:15px;'>Supplier Name </td>
             <td colspan='2' style='font-size:12px;text-align:left;'>: {modelHead.Supplier}</td>
            
            
             </tr>
           <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Date : {modelHead.Date:yyyy/MMM/dd}</td>
            
              
             
             </tr>
             <tr>
                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime()}</td>
            
             </tr>

           </tbody>
           </table>
            
             
            </div>
        </div>

        <div style='padding: 16px;'>
            <h3 style='font-size: 20px; margin-bottom: 08px; color: black;' >Ordered Items</h3>
            <br>
            <table style='background-color: #fff;width: 100%;border-collapse: collapse;box-shadow: 0px 0px 5px 0.5px gray;'>
                <thead>
                    <tr style='border: 1px solid #111;background-color: #f2f2f2;'>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 40%;'>Product</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Code</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Price</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Quantity</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Discount</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >GrandTotal</th>
                    </tr>
                </thead>
                <tbody>
                    
                      {TableText}
                    
                    <tr>
                        <td style='vertical-align: middle !important;text-align: center;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' colspan='5'>Sub Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {Total}</td>
                    </tr>
                     <tr>
                        <td colspan='5' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Discount</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalDiscount}</td>
                    </tr>
                    <tr>
                        <td colspan='5' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Grand Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalGross}</td>
                    </tr>
                </tbody>
            </table>
            <br>
            <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Prepared By : {modelHead.Created}</td>
            
           
             <td colspan='3' style='font-size:18px;text-align:left;'>Authorized Person : ......................</td>
            
             </tr>
           <tr>
            <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Count  : {countRows}</td>
            
             
            <td colspan='3' style='font-size:18px;text-align:left;'> Authorized Date  : ......................</td>
            
             </tr>
           </tbody>
           </table>
            
        </div>

          
    </div>      

</body>
</html>";
            GlobalSettings globalSettings = new()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Right = 0, Left = 0 }
            };
            ObjectSettings objectSettings = new()
            {
                PagesCount = true,
                HtmlContent = html2
            };
            WebSettings webSettings = new()
            {
                DefaultEncoding = "utf-8"
            };
            HeaderSettings headerSettings = new()
            {
                FontSize = 15,
                FontName = "times new roman"
            };
            //headerSettings.Right = "Page [page] of [toPage]";
            //headerSettings.Line = true;
            FooterSettings footerSettings1 = new();
            FooterSettings footerSettings = footerSettings1;
            footerSettings.FontSize = 8;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788";

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
            return pdfData;
        }


        [HttpGet]
        [Route(API_ROUTE_NAME + "/PrintGIN/{id}")]
        public IActionResult PrintreportGIN(int ID)
        {
            byte[] reurnarray = GINPDfAsync(ID);
            return File(reurnarray, "application/pdf");
        }
        public byte[] GINPDfAsync(int id)
        {
            var modelHead = _GINServices.GetDetailsByGINHeadIdForPrint(id);

            var getcompany = _reportService.GetCompanyDetailsForPrint(modelHead.FKLocationId ?? 0);
            var modelBody = _GINServices.GetByGINBodiesId(id);
            //var result = await modelBody.ToList();

            StringBuilder TableText = new();
            decimal tot = 0m;
            decimal Discount = 0m;

            decimal Gross = 0m;
            int countRows = 0;
            foreach (var obj in modelBody)
            {
                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.ItemName + "</td>" +
                    "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' > " + obj.Code + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Cost, 2).ToString("#,0.00") + "  </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Qty, 2).ToString("#,0.00") + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.UnitCost, 2).ToString("#,0.00") + " </td></tr> ";
                TableText.Append(rownew);
                tot = (decimal)modelHead.Total;
                Discount = (decimal)modelHead.Discount;
                Gross = (decimal)modelHead.Gross;

                countRows++;
            }
            var Total = tot.ToString("#,0.00");
            var TotalDiscount = Discount.ToString("#,0.00");
            var TotalGross = Gross.ToString("#,0.00");

            string tablestyle = "  html {     margin-top:-30px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

            var html2 = $@"<html lang='en''>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <title> Good Issue Note</title>
    <style type='text/css'>
       {tablestyle}
    </style>
</head>
<body>

    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        <div style='padding: 20px 40px;'>
            <div style='display: flex;flex-wrap: wrap;'>
                 <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
             <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName} </b></h2></td>
            
             </tr>
            <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1} / {getcompany.TelPhone2}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'><h4>Good Issue Note</h4></td>
            
             </tr>
          
          
           </tbody>
           </table>
                
            </div>
               
        </div>

        <div style='padding: 16px;'>
            <div style='display: flex;flex-wrap: wrap;'>
              <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
           
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>GRN NO : {modelHead.GINId}</td>
            
              
            
            
             </tr>
           <tr>
            
             <td colspan='2' style='width:40%;font-size:18px;text-align:left;'>Date : {modelHead.Date:yyyy/MMM/dd}</td>
            
              
             
             </tr>
             <tr>
                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime()}</td>
            
             </tr>

           </tbody>
           </table>
            
             
            </div>
        </div>

        <div style='padding: 16px;'>
            <h3 style='font-size: 20px; margin-bottom: 08px; color: black;' >Ordered Items</h3>
            <br>
            <table style='background-color: #fff;width: 100%;border-collapse: collapse;box-shadow: 0px 0px 5px 0.5px gray;'>
                <thead>
                    <tr style='border: 1px solid #111;background-color: #f2f2f2;'>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 40%;'>Product</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Code</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Price</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Quantity</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >GrandTotal</th>
                    </tr>
                </thead>
                <tbody>
                    
                      {TableText}
                    
                    <tr>
                        <td style='vertical-align: middle !important;text-align: center;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' colspan='3'>Sub Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {Total}</td>
                    </tr>
                   
                    <tr>
                        <td colspan='3' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Grand Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalGross}</td>
                    </tr>
                </tbody>
            </table>
            <br>
            <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Prepared By : {modelHead.Created}</td>
            
           
             <td colspan='3' style='font-size:18px;text-align:left;'>Authorized Person : ......................</td>
            
             </tr>
           <tr>
            <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Count  : {countRows}</td>
            
             
            <td colspan='3' style='font-size:18px;text-align:left;'> Authorized Date  : ......................</td>
            
             </tr>
           </tbody>
           </table>
            
        </div>

          
    </div>      

</body>
</html>";
            GlobalSettings globalSettings = new()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Right = 0, Left = 0 }
            };
            ObjectSettings objectSettings = new()
            {
                PagesCount = true,
                HtmlContent = html2
            };
            WebSettings webSettings = new()
            {
                DefaultEncoding = "utf-8"
            };
            HeaderSettings headerSettings = new()
            {
                FontSize = 15,
                FontName = "times new roman"
            };
            //headerSettings.Right = "Page [page] of [toPage]";
            //headerSettings.Line = true;
            FooterSettings footerSettings1 = new();
            FooterSettings footerSettings = footerSettings1;
            footerSettings.FontSize = 8;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788";

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
            return pdfData;
        }


        [HttpGet]
        [Route(API_ROUTE_NAME + "/PrintPOS/{id}")]
        public IActionResult PrintreportPOS(int ID)
        {
            byte[] reurnarray = POSPDfAsync(ID);

            return File(reurnarray, "application/pdf");
        }
        //        public byte[] POSPDfAsync(int id)
        //        {
        //            var getcompany = _reportService.GetCompanyDetailsForPrint();
        //            var modelHead = _iPOSService.GetDetailsByPOSHeadIdForPrint(id);
        //            var modelBody = _iPOSService.GetByPOSBodiesId(id);
        //            //var result = await modelBody.ToList();

        //            StringBuilder TableText = new();
        //            decimal tot = 0m;
        //            decimal Discount = 0m;

        //            decimal Gross = 0m;
        //            int countRows = 0;
        //            foreach (var obj in modelBody)
        //            {


        //                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.ItemName + "</td>" +
        //                    "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' > " + obj.Code + " </td>" +
        //                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.UnitCost, 2).ToString("#,0.00") + "  </td>" +
        //                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Qty, 2).ToString("#,0.00") + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
        //                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Cost, 2).ToString("#,0.00") + " </td></tr> ";
        //                TableText.Append(rownew);
        //                tot = (decimal)modelHead.Total;
        //                Discount = (decimal)modelHead.Discount;
        //                Gross = (decimal)modelHead.Gross;

        //                countRows++;
        //            }
        //            var Total = tot.ToString("#,0.00");
        //            var TotalDiscount = (Discount).ToString("#,0.00");
        //            var TotalGross = Gross.ToString("#,0.00");

        //            string tablestyle = "  html {     margin-top:-30px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

        //            var html2 = $@"<html lang='en''>
        //<head>
        //    <meta charset = 'UTF-8'>
        //    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
        //    <title> Invoice</title>
        //    <style type='text/css'>
        //       {tablestyle}
        //    </style>
        //</head>
        //<body>

        //    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        //        <div style='padding: 20px 40px;'>
        //            <div style='display: flex;flex-wrap: wrap;'>
        //                 <table id='tbmain' width='100%' style='table-layout: fixed;
        //        word-wrap: break-word;
        //        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
        //          <thead>  
        //           <tr>
        //          <th style='width:70%;'></th>
        //          <th style='width:50%;'></th>
        //           <th style='width:50%;'></th>
        //            <th style='width:50%;'></th>
        //            <th style='width:50%;'></th>
        //            <th style='width:50%;'></th>
        //         </thead>
        //           <tbody>



        //             <tr>
        //             <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName} </b></h2></td>

        //             </tr>
        //            <tr>
        //             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>

        //             </tr>
        //             <tr>
        //             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1} / {getcompany.TelPhone2}</td>

        //             </tr>
        //             <tr>
        //             <td style='text-align:center;font-size:18px;' colspan='6'><h3>Invoice</h3></td>

        //             </tr>


        //           </tbody>
        //           </table>

        //            </div>

        //        </div>

        //        <div style='padding: 16px;'>
        //            <div style='display: flex;flex-wrap: wrap;'>
        //              <table id='tbmain' width='100%' style='table-layout: fixed;
        //        word-wrap: break-word;
        //        word-break: break-all;margin-top:20px;color:black;font-family:times'>
        //          <thead>  
        //           <tr>
        //          <th style='width:70%;'></th>
        //          <th style='width:50%;'></th>
        //           <th style='width:50%;'></th>
        //            <th style='width:50%;'></th>
        //            <th style='width:50%;'></th>
        //         </thead>
        //           <tbody>



        //             <tr>

        //             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Invoice NO :{"INV000"}{modelHead.Id}</td>




        //             </tr>
        //           <tr>

        //             <td colspan='2' style='width:40%;font-size:18px;text-align:left;'>Date : {modelHead.Date:yyyy/MMM/dd}</td>



        //             </tr>
        //             <tr>
        //                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime()}</td>

        //             </tr>

        //           </tbody>
        //           </table>


        //            </div>
        //        </div>

        //        <div style='padding: 16px;'>
        //            <h3 style='font-size: 20px; margin-bottom: 08px; color: black;' >Ordered Items</h3>
        //            <br>
        //            <table style='background-color: #fff;width: 100%;border-collapse: collapse;box-shadow: 0px 0px 5px 0.5px gray;'>
        //                <thead>
        //                    <tr style='border: 1px solid #111;background-color: #f2f2f2;'>
        //                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 40%;'>Product</th>
        //                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Code</th>
        //                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Price</th>
        //                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Quantity</th>
        //                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >GrandTotal</th>
        //                    </tr>
        //                </thead>
        //                <tbody>

        //                      {TableText}

        //                    <tr>
        //                        <td style='vertical-align: middle !important;text-align: center;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' colspan='4'>Sub Total</td>
        //                        <td colspan='1' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {Total}</td>
        //                    </tr>

        //                    <tr>
        //                        <td colspan='4' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Grand Total</td>
        //                        <td colspan='1' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalGross}</td>
        //                    </tr>
        //                </tbody>
        //            </table>
        //            <br>
        //            <table id='tbmain' width='100%' style='table-layout: fixed;
        //        word-wrap: break-word;
        //        word-break: break-all;margin-top:20px;color:black;font-family:times'>
        //          <thead>  
        //           <tr>
        //          <th style='width:70%;'></th>
        //          <th style='width:50%;'></th>
        //           <th style='width:50%;'></th>
        //            <th style='width:50%;'></th>
        //            <th style='width:50%;'></th>
        //            <th style='width:50%;'></th>
        //         </thead>
        //           <tbody>



        //             <tr>

        //             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Prepared By : {modelHead.Created}</td>


        //             <td colspan='3' style='font-size:18px;text-align:left;'>Authorized Person : ......................</td>

        //             </tr>
        //           <tr>
        //            <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Count  : {countRows}</td>


        //            <td colspan='3' style='font-size:18px;text-align:left;'> Authorized Date  : ......................</td>

        //             </tr>
        //           </tbody>
        //           </table>

        //        </div>


        //    </div>      

        //</body>
        //</html>";
        //            GlobalSettings globalSettings = new()
        //            {
        //                ColorMode = ColorMode.Color,
        //                Orientation = Orientation.Portrait,
        //                PaperSize = PaperKind.A4,
        //                Margins = new MarginSettings { Top = 10, Bottom = 10, Right = 0, Left = 0 }
        //            };
        //            ObjectSettings objectSettings = new()
        //            {
        //                PagesCount = true,
        //                HtmlContent = html2
        //            };
        //            WebSettings webSettings = new()
        //            {
        //                DefaultEncoding = "utf-8"
        //            };
        //            HeaderSettings headerSettings = new()
        //            {
        //                FontSize = 15,
        //                FontName = "times new roman"
        //            };
        //            //headerSettings.Right = "Page [page] of [toPage]";
        //            //headerSettings.Line = true;
        //            FooterSettings footerSettings1 = new();
        //            FooterSettings footerSettings = footerSettings1;
        //            footerSettings.FontSize = 8;
        //            footerSettings.FontName = "Ariel";
        //            footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788";

        //            objectSettings.HeaderSettings = headerSettings;
        //            objectSettings.FooterSettings = footerSettings;
        //            objectSettings.WebSettings = webSettings;
        //            HtmlToPdfDocument htmlToPdfDocument = new()
        //            {
        //                GlobalSettings = globalSettings,
        //                Objects = { objectSettings },
        //            };
        //            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
        //            return pdfData;
        //        }

        public byte[] POSPDfAsync(int id)
        {
            var modelHead = _iPOSService.GetDetailsByPOSHeadIdForPrint(id);
            var getcompany = _reportService.GetCompanyDetailsForPrint(modelHead.LocationId ?? 0);

            var modelBody = _iPOSService.GetByPOSBodiesId(id);
            //var result = await modelBody.ToList();

            StringBuilder TableText = new();
            decimal tot = 0m;
            decimal Discount = 0m;

            decimal Gross = 0m;
            int countRows = 0;
            foreach (var obj in modelBody)
            {


                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;color: black;font-size:18px;'>" + obj.ItemName + "</td>" +
                 "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;color: black;font-size:18px;' > " + obj.Code + " </td>" +
                     "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;color: black;font-size:18px;'>" + Math.Round((decimal)obj.Sellingprice, 2).ToString("#,#.##") + "  </td>" +
                     "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;color: black;font-size:18px;'>" + Math.Round((decimal)obj.Qty, 2).ToString("#,#.##") + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
                     "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;color: black;font-size:18px;'>" + Math.Round((decimal)obj.Discount, 2).ToString("#,#.##") + " </td>" +
                     "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;color: black;font-size:18px;'>" + Math.Round((decimal)obj.Cost, 2).ToString("#,#.##") + " </td></tr> ";
                TableText.Append(rownew);
                tot = (decimal)modelHead.Total;
                Discount = (decimal)modelHead.Discount;
                Gross = (decimal)modelHead.Gross;

                countRows++;
            }
            var Total = tot.ToString("#,0.00");
            var TotalDiscount = Discount.ToString("#,0.00");
            var TotalGross = Gross.ToString("#,0.00");


            string tablestyle = "  html {     margin-top:0px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

            var html = $@"<html lang='en''>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <title> Invoice</title>
    <style type='text/css'>
       {tablestyle}
    </style>
</head>
<body>

    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        <div style='padding: 20px 40px;'>
            <div style='display: flex;flex-wrap: wrap;'>
                 <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
             <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName}</b></h2></td>
            
             </tr>
            <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1 + " " + getcompany.TelPhone2}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;color: black;' colspan='6'><h4>Invoice</h4></td>
            
             </tr>
          
          
           </tbody>
           </table>
                
            </div>
               
        </div>

        <div style='padding: 16px;'>
            <div style='display: flex;flex-wrap: wrap;margin-top: -70;'>
              <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:10px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
           
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Invoice NO :{"INV000"}{modelHead.Id}</td>
            
              <td style='text-align:right;font-size:18px;'>Customer  </td>
             <td colspan='2' style='font-size:15px;text-align:left;'>: {modelHead.Customer} </td>
            
            
             </tr>
           <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Date : {modelHead.Date:yyyy/MMM/dd}</td>
            
              
             
             </tr>
             <tr>
                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime():yyyy/MMM/dd hh:mm:tt}</td>
            
             </tr>

           </tbody>
           </table>
            
             
            </div>
        </div>

        <div style='padding: 16px;margin-top:-40px;'>
            <h3 style='font-size: 20px; margin-bottom: 08px;color: black;' >Ordered Items</h3>
            <br>
            <table style='background-color: #fff;width: 100%;border-collapse: collapse;'>
                <thead>
                    <tr style='border: 1px solid #111;'>
                        <th style='padding-top: 08px;color: black; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 40%;'> <h3>Product</h3></th>
                        <th style='padding-top: 08px;color: black; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'><h3>Code</h3></th>
                        <th style='padding-top: 08px;color: black; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' ><h3>Price</h3></th>
                        <th style='padding-top: 08px;color: black; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' ><h3>Quantity</h3></th>
                        <th style='padding-top: 08px;color: black; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' ><h3>Discount</h3></th>
                        <th style='padding-top: 08px;color: black; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' ><h3>GrandTotal</h3></th>
                    </tr>
                </thead>
                <tbody>
                    
                      {TableText}
                    
                    <tr>
                        <td style='vertical-align: middle !important;text-align: center;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;color: black;font-size:18px;' colspan='5'>Sub Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px;color: black; padding-bottom: 08px;border: 1px solid #dee2e6;font-size:18px;'> {Total}</td>
                    </tr>
                     <tr>
                        <td colspan='5' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;color: black;font-size:18px;'>Discount</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px;color: black; padding-bottom: 08px;border: 1px solid #dee2e6;font-size:18px;'> {TotalDiscount}</td>
                    </tr>
                    <tr>
                        <td colspan='5' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;color: black;font-size:18px;'>Grand Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px;color: black; padding-bottom: 08px;border: 1px solid #dee2e6;font-size:18px;'> {TotalGross}</td>
                    </tr>
                </tbody>
            </table>
            
            <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;font-size:16px;'>Prepared By : {modelHead.Created}</td>
            
           
             <td colspan='3' style='font-size:18px;text-align:left;font-size:16px;'>Authorized Person : ......................</td>
            
             </tr>
           <tr>
            <td colspan='3' style='width:40%;font-size:18px;text-align:left;font-size:16px;'>Count  : {countRows}</td>
            
             
            <td colspan='3' style='font-size:18px;text-align:left;font-size:16px;'> Authorized Date  : ......................</td>
            
             </tr>

            

           
           </tbody>
           </table>
            
        </div>
            <p style='text-align:center;color: black;font-size:16px;'>**Thank you come again**</p>
            <p style='text-align:center;color: black;font-size:16px;'>Solution provided by M System Solutions 077 55 49 788</p>

    </div>      

</body>
</html>";

            GlobalSettings globalSettings = new()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A5,
                Margins = new MarginSettings { Top = 0, Bottom = 2, Right = 0, Left = 0 }
            };
            ObjectSettings objectSettings = new()
            {
                PagesCount = true,
                HtmlContent = html
            };
            WebSettings webSettings = new()
            {
                DefaultEncoding = "utf-8"
            };
            HeaderSettings headerSettings = new()
            {
                FontSize = 12,
                FontName = "times new roman"
            };
            //headerSettings.Right = "Page [page] of [toPage]";
            //headerSettings.Line = true;
            FooterSettings footerSettings1 = new();
            FooterSettings footerSettings = footerSettings1;
            footerSettings.FontSize = 12;
            footerSettings.FontName = "Ariel";
            //footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788";

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
            return pdfData;
        }


        [HttpGet]
        [Route(API_ROUTE_NAME + "/PrintPOSReturn/{id}")]
        public IActionResult PrintreportPOSReturn(int ID)
        {
            byte[] reurnarray = POSReturnPDfAsync(ID);
            return File(reurnarray, "application/pdf");
        }
        public byte[] POSReturnPDfAsync(int id)
        {
            var modelHead = _pOSReturnService.GetDetailsByPRNHeadIdForPrint(id);

            var getcompany = _reportService.GetCompanyDetailsForPrint(modelHead.LocationId);
            var modelBody = _pOSReturnService.GetByPOSReturnBodiesId(id);
            //var result = await modelBody.ToList();

            StringBuilder TableText = new();
            decimal tot = 0m;
            decimal Discount = 0m;

            decimal Gross = 0m;
            int countRows = 0;
            foreach (var obj in modelBody)
            {
                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.ItemName + "</td>" +
                    "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' > " + obj.Code + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Sellingprice, 2).ToString("#,0.00") + "  </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Qty, 2).ToString("#,0.00") + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Cost, 2).ToString("#,0.00") + " </td></tr> ";
                TableText.Append(rownew);
                tot = (decimal)modelHead.Total;
                Discount = (decimal)modelHead.Discount;
                Gross = (decimal)modelHead.Gross;

                countRows++;
            }
            var Total = tot.ToString("#,0.00");
            var TotalDiscount = Discount.ToString("#,0.00");
            var TotalGross = Gross.ToString("#,0.00");

            string tablestyle = "  html {     margin-top:-30px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

            var html2 = $@"<html lang='en''>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <title> Invoice</title>
    <style type='text/css'>
       {tablestyle}
    </style>
</head>
<body>

    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        <div style='padding: 20px 40px;'>
            <div style='display: flex;flex-wrap: wrap;'>
                 <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
             <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName} </b></h2></td>
            
             </tr>
            <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1} / {getcompany.TelPhone2}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'><h3>Customer Return Note</h3></td>
            
             </tr>
          
          
           </tbody>
           </table>
                
            </div>
               
        </div>

        <div style='padding: 16px;'>
            <div style='display: flex;flex-wrap: wrap;'>
              <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
           
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Invoice NO :{modelHead.RefInv}</td>
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Return NO :{"INVR000"}{modelHead.Id}</td>
              
            
            
             </tr>
           <tr>
            
             <td colspan='2' style='width:40%;font-size:18px;text-align:left;'>Date : {modelHead.Date:yyyy/MMM/dd}</td>
            
              
             
             </tr>
             <tr>
                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime()}</td>
            
             </tr>

           </tbody>
           </table>
            
             
            </div>
        </div>

        <div style='padding: 16px;'>
            <h3 style='font-size: 20px; margin-bottom: 08px; color: black;' >Ordered Items</h3>
            <br>
            <table style='background-color: #fff;width: 100%;border-collapse: collapse;box-shadow: 0px 0px 5px 0.5px gray;'>
                <thead>
                    <tr style='border: 1px solid #111;background-color: #f2f2f2;'>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 40%;'>Product</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Code</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Price</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Quantity</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >GrandTotal</th>
                    </tr>
                </thead>
                <tbody>
                    
                      {TableText}
                    
                    <tr>
                        <td style='vertical-align: middle !important;text-align: center;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' colspan='4'>Sub Total</td>
                        <td colspan='1' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {Total}</td>
                    </tr>
                   
                    <tr>
                        <td colspan='4' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Grand Total</td>
                        <td colspan='1' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalGross}</td>
                    </tr>
                </tbody>
            </table>
            <br>
            <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Prepared By : {modelHead.Created}</td>
            
           
             <td colspan='3' style='font-size:18px;text-align:left;'>Authorized Person : ......................</td>
            
             </tr>
           <tr>
            <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Count  : {countRows}</td>
            
             
            <td colspan='3' style='font-size:18px;text-align:left;'> Authorized Date  : ......................</td>
            
             </tr>
           </tbody>
           </table>
            
        </div>

          
    </div>      

</body>
</html>";
            GlobalSettings globalSettings = new()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Right = 0, Left = 0 }
            };
            ObjectSettings objectSettings = new()
            {
                PagesCount = true,
                HtmlContent = html2
            };
            WebSettings webSettings = new()
            {
                DefaultEncoding = "utf-8"
            };
            HeaderSettings headerSettings = new()
            {
                FontSize = 15,
                FontName = "times new roman"
            };
            //headerSettings.Right = "Page [page] of [toPage]";
            //headerSettings.Line = true;
            FooterSettings footerSettings1 = new();
            FooterSettings footerSettings = footerSettings1;
            footerSettings.FontSize = 8;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788 \n 077 55 49 788 / 070 65 49 788";

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
            return pdfData;
        }








        #endregion


        #region ReorderLevel  Report

        [HttpGet]
        [Route(API_ROUTE_NAME + "/ReorderLevelReport/{CompanyId}")]
        public IActionResult PrintReorderLevelReport(int CompanyId)
        {
            byte[] reurnarray = PrintReorderLevelReportData(CompanyId);
            return File(reurnarray, "application/pdf");
        }
        public byte[] PrintReorderLevelReportData(int CompanyId)
        {
            var getcompany = _reportService.GetCompanyDetailsForPrint(CompanyId);
            var modelBody = _reportService.ListReportReorderLevelItemAsync(CompanyId);

            StringBuilder TableText = new();

            int countRows = 0;
            foreach (var obj in modelBody)
            {
                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.ItemCode + "</td>" +
                    "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' > " + obj.ItemName + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.CategoryName + "  </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.BrandName + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.ReorderLevel, 2).ToString("#,0.00") + " </td> " +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Convert.ToString(obj.BalanceQty == 0 ? 0 : Math.Round((decimal)obj.BalanceQty, 2).ToString("#,0.00")) + " </td> </tr>";
                TableText.Append(rownew);
                countRows++;
            }

            string tablestyle = "  html {     margin-top:-30px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

            var html2 = $@"<html lang='en''>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <title> Item Reorder Level Report</title>
    <style type='text/css'>
       {tablestyle}
    </style>
</head>
<body>

    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        <div style='padding: 20px 40px;'>
            <div style='display: flex;flex-wrap: wrap;'>
                 <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
                <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName} </b></h2></td>
            
             </tr>
            <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1} / {getcompany.TelPhone2}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'><h4>Item Reorder Level Report</h4></td>
            
             </tr>
          
          
           </tbody>
           </table>
                
            </div>
               
        </div>

        <div style='padding: 16px;'>
            <div style='display: flex;flex-wrap: wrap;'>
              <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
           
           
              
            
            
             </tr>
         
             <tr>
                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime():yyyy/MMM/dd hh:mm:tt}</td>
            
             </tr>

           </tbody>
           </table>
            
             
            </div>
        </div>

        <div style='padding: 16px;margin-top:-40px;'>
            <h3 style='font-size: 20px; margin-bottom: 08px;' >Details</h3>
           
            <table style='background-color: #fff;width: 100%;border-collapse: collapse;box-shadow: 0px 0px 5px 0.5px gray;'>
                <thead>
                    <tr style='border: 1px solid #111;background-color: #f2f2f2;'>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Item Code</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 25%;'>Item Name</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Category</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 20%;text-align: center;' >Brand Name</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Reorder Level</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >BalanceQty</th>
                    </tr>
                </thead>
                <tbody>
                    
                      {TableText}
                    
                   
                </tbody>
            </table>
            <br>
            <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Prepared By : {User.FindFirstValue("FullName")}</td>
            
           
             <td colspan='3' style='font-size:18px;text-align:left;'>Authorized Person : ......................</td>
            
             </tr>
           <tr>
            <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Count  : {countRows}</td>
            
             
            <td colspan='3' style='font-size:18px;text-align:left;'> Authorized Date  : ......................</td>
            
             </tr>
           </tbody>
           </table>
            
        </div>

          
          
    </div>      

</body>
</html>";
            GlobalSettings globalSettings = new()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Right = 0, Left = 0 }
            };
            ObjectSettings objectSettings = new()
            {
                PagesCount = true,
                HtmlContent = html2
            };
            WebSettings webSettings = new()
            {
                DefaultEncoding = "utf-8"
            };
            HeaderSettings headerSettings = new()
            {
                FontSize = 15,
                FontName = "times new roman",
                Right = "Page [page] of [toPage]",
                Line = true
            };
            FooterSettings footerSettings1 = new();
            FooterSettings footerSettings = footerSettings1;
            footerSettings.FontSize = 10;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788";

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
            return pdfData;
        }



        #endregion


        #region Available ItemDetails Report  

        [HttpGet]
        [Route(API_ROUTE_NAME + "/PrintAvailableItemDetailsReport/{CompanyId}")]
        public IActionResult PrintAvailableItemDetailsReport(int CompanyId)
        {

            byte[] reurnarray = PrintAvailableItemDetailsReportData(CompanyId);
            return File(reurnarray, "application/pdf");
        }
        public byte[] PrintAvailableItemDetailsReportData(int CompanyId)
        {
            var modelBody = _reportService.ListAvailableItemDetailsItemList();
            var getcompany = _reportService.GetCompanyDetailsForPrint(CompanyId);
            StringBuilder TableText = new();

            int countRows = 0;

            foreach (var obj in modelBody)
            {
                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.ItemCode + "</td>" +
                    "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' > " + obj.ItemName + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.CategoryName + "  </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.BrandName + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.ReorderLevel, 2).ToString("#,0.00") + " </td> " +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.MinLevel + " </td> " +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.MinLevel + " </td> " +
                "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.BalanceQty + " </td> </tr>";
                TableText.Append(rownew);

                countRows++;
            }

            string tablestyle = "  html {     margin-top:-30px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

            var html2 = $@"<html lang='en''>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <title> Item Available ItemDetails Report</title>
    <style type='text/css'>
       {tablestyle}
    </style>
</head>
<body>

    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        <div style='padding: 20px 40px;'>
            <div style='display: flex;flex-wrap: wrap;'>
                 <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
             <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName} </b></h2></td>
            
             </tr>
            <tr>
            <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>
            
             </tr>
             <tr>
            <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1} / {getcompany.TelPhone2}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'><h4> Item Available ItemDetails Report</h4></td>
            
             </tr>
          
          
           </tbody>
           </table>
                
            </div>
               
        </div>

        <div style='padding: 16px;'>
            <div style='display: flex;flex-wrap: wrap;'>
              <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
         
             <tr>
                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime():yyyy/MMM/dd hh:mm:tt}</td>
            
             </tr>

           </tbody>
           </table>
            
             
            </div>
        </div>

        <div style='padding: 16px;margin-top:-40px;'>
            <h3 style='font-size: 20px; margin-bottom: 08px;' >Details</h3>
           
            <table style='background-color: #fff;width: 100%;border-collapse: collapse;box-shadow: 0px 0px 5px 0.5px gray;'>
                <thead>
                    <tr style='border: 1px solid #111;background-color: #f2f2f2;'>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Item Code</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 25%;'>Item Name</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Category</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 20%;text-align: center;' >Brand Name</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Reorder Level</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Min Qty</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Max Qty</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Remark</th>
                    </tr>
                </thead>
                <tbody>
                    
                      {TableText}
                    
                   
                </tbody>
            </table>
            <br>
            <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Prepared By : {User.FindFirstValue("FullName")}</td>
            
           
             <td colspan='3' style='font-size:18px;text-align:left;'>Authorized Person : ......................</td>
            
             </tr>
           <tr>
            <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Count  : {countRows}</td>
            
             
            <td colspan='3' style='font-size:18px;text-align:left;'> Authorized Date  : ......................</td>
            
             </tr>
           </tbody>
           </table>
            
        </div>

          
          
    </div>      

</body>
</html>";
            GlobalSettings globalSettings = new()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Right = 0, Left = 0 }
            };
            ObjectSettings objectSettings = new()
            {
                PagesCount = true,
                HtmlContent = html2
            };
            WebSettings webSettings = new()
            {
                DefaultEncoding = "utf-8"
            };
            HeaderSettings headerSettings = new()
            {
                FontSize = 15,
                FontName = "times new roman",
                Right = "Page [page] of [toPage]",
                Line = true
            };
            FooterSettings footerSettings1 = new();
            FooterSettings footerSettings = footerSettings1;
            footerSettings.FontSize = 10;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788";

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
            return pdfData;
        }



        #endregion

        #region QTy Available ItemDetails Report Location Wise 

        [HttpGet]
        [Route(API_ROUTE_NAME + "/ItemByAvailableQuantity/{CompanyId}")]

        public IActionResult PrintAvailableQtyItemDetailsLocationWiseReport(int CompanyId)
        {

            byte[] reurnarray = PrintAvailableQtyItemDetailsLocationWiseReportData(CompanyId);
            return File(reurnarray, "application/pdf");
        }
        public byte[] PrintAvailableQtyItemDetailsLocationWiseReportData(int CompanyId)
        {
            var getcompany = _reportService.GetCompanyDetailsForPrint(CompanyId);
            var modelBody = _reportService.VWAllActiveANDAvailableItemList(CompanyId);

            StringBuilder TableText = new();

            int countRows = 0;
            foreach (var obj in modelBody)
            {
                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.ItemCode + "</td>" +
                    "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' > " + obj.ItemName + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.CategoryName + "  </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.BrandName + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.ReorderLevel, 2).ToString("#,0.00") + " </td> " +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.MinLevel + " </td> " +
                "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.MaxLevel + " </td> " +
                "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.BalanceQty, 2).ToString("#,0.00") + " </td> </tr>";
                TableText.Append(rownew);
                countRows++;
            }

            string tablestyle = "  html {     margin-top:-30px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

            var html2 = $@"<html lang='en''>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <title> Item Available Quantity ItemDetails Report</title>
    <style type='text/css'>
       {tablestyle}
    </style>
</head>
<body>

    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        <div style='padding: 20px 40px;'>
            <div style='display: flex;flex-wrap: wrap;'>
                 <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
             <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName} </b></h2></td>
            
             </tr>
            <tr>
            <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>
            
             </tr>
             <tr>
              <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1} / {getcompany.TelPhone2}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'><h4> Item Available Quantity ItemDetails Report</h4></td>
            
             </tr>
          
          
           </tbody>
           </table>
                
            </div>
               
        </div>

        <div style='padding: 16px;'>
            <div style='display: flex;flex-wrap: wrap;'>
              <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
           <tr>
           
           
            
            
             </tr>
             <tr>
                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime():yyyy/MMM/dd hh:mm:tt}</td>
            
             </tr>

           </tbody>
           </table>
            
             
            </div>
        </div>

        <div style='padding: 16px;margin-top:-40px;'>
            <h3 style='font-size: 20px; margin-bottom: 08px;' >Details</h3>
           
            <table style='background-color: #fff;width: 100%;border-collapse: collapse;box-shadow: 0px 0px 5px 0.5px gray;'>
                <thead>
                    <tr style='border: 1px solid #111;background-color: #f2f2f2;'>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Item Code</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 25%;'>Item Name</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Category</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 20%;text-align: center;' >Brand Name</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Reorder Level</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Min Qty</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Max Qty</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 15%;text-align: center;' >Balance Qty</th>
                    </tr>
                </thead>
                <tbody>
                    
                      {TableText}
                    
                   
                </tbody>
            </table>
            <br>
            <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Prepared By : {User.FindFirstValue("FullName")}</td>
            
           
             <td colspan='3' style='font-size:18px;text-align:left;'>Authorized Person : ......................</td>
            
             </tr>
           <tr>
            <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Count  : {countRows}</td>
            
             
            <td colspan='3' style='font-size:18px;text-align:left;'> Authorized Date  : ......................</td>
            
             </tr>
           </tbody>
           </table>
            
        </div>

          
          
    </div>      

</body>
</html>";
            GlobalSettings globalSettings = new()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Right = 0, Left = 0 }
            };
            ObjectSettings objectSettings = new()
            {
                PagesCount = true,
                HtmlContent = html2
            };
            WebSettings webSettings = new()
            {
                DefaultEncoding = "utf-8"
            };
            HeaderSettings headerSettings = new()
            {
                FontSize = 15,
                FontName = "times new roman",
                Right = "Page [page] of [toPage]",
                Line = true
            };
            FooterSettings footerSettings1 = new();
            FooterSettings footerSettings = footerSettings1;
            footerSettings.FontSize = 10;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788";

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
            return pdfData;
        }



        #endregion


        #region Monthly Sales Report

        [HttpGet]
        [Route(API_ROUTE_NAME + "/PrintMonthlySalesReport/{Month}/{CompanyId}")]
        public IActionResult PrintMonthlySalesReport(int Month, int CompanyId)
        {
            byte[] reurnarray = PrintMonthlySalesReportData(Month, CompanyId);
            return File(reurnarray, "application/pdf");
        }
        public byte[] PrintMonthlySalesReportData(int id, int CompanyId)
        {
            var getcompany = _reportService.GetCompanyDetailsForPrint(CompanyId);
            var modelBody = _reportService.VWAllActiveInvoiceHeadForReportMonthWise(id);

            StringBuilder TableText = new();
            decimal tot = 0m;
            decimal Discount = 0m;

            decimal Gross = 0m;
            int countRows = 0;
            foreach (var obj in modelBody)
            {
                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.Id + "</td>" +
                    "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' > " + obj.InvoiceDate.ToString("yyyy-MMM-dd") + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.Type + "  </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.UserName + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.Customer + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Total, 2).ToString("#,0.00") + " </td> " +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Discount, 2).ToString("#,0.00") + " </td> " +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Gross, 2).ToString("#,0.00") + " </td></tr> ";
                TableText.Append(rownew);
                tot += (decimal)obj.Total;
                Discount += (decimal)obj.Discount;
                Gross += (decimal)obj.Gross;
                countRows++;
            }
            var Total = tot.ToString("#,0.00");
            var TotalDiscount = Discount.ToString("#,0.00");
            var TotalGross = Gross.ToString("#,0.00");

            string tablestyle = "  html {     margin-top:-30px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

            var html2 = $@"<html lang='en''>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <title> Monthly Sales Report</title>
    <style type='text/css'>
       {tablestyle}
    </style>
</head>
<body>

    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        <div style='padding: 20px 40px;'>
            <div style='display: flex;flex-wrap: wrap;'>
                 <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
               <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName} </b></h2></td>
            
             </tr>
            <tr>
            <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>
            
             </tr>
             <tr>
            <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1} / {getcompany.TelPhone2}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'><h4>Monthly Sales Report</h4></td>
            
             </tr>
          
          
           </tbody>
           </table>
                
            </div>
               
        </div>

        <div style='padding: 16px;'>
            <div style='display: flex;flex-wrap: wrap;'>
              <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
           
           
             
            
            
             </tr>
         
             <tr>
                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime():yyyy/MMM/dd hh:mm:tt}</td>
            
             </tr>

           </tbody>
           </table>
            
             
            </div>
        </div>

        <div style='padding: 16px;margin-top:-40px;'>
            <h3 style='font-size: 20px; margin-bottom: 08px;' >Details</h3>
           
            <table style='background-color: #fff;width: 100%;border-collapse: collapse;box-shadow: 0px 0px 5px 0.5px gray;'>
                <thead>
                    <tr style='border: 1px solid #111;background-color: #f2f2f2;'>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Invoice No</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Date</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Type</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 20%;text-align: center;' >Created By</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 20%;text-align: center;' >Customer Name</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Total</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Discount</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >GrandTotal</th>
                    </tr>
                </thead>
                <tbody>
                    
                      {TableText}
                    
                    <tr>
                        <td style='vertical-align: middle !important;text-align: center;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' colspan='6'>Monthly Sub Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {Total}</td>
                    </tr>
                     <tr>
                        <td colspan='6' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Monthly Discount</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalDiscount}</td>
                    </tr>
                    <tr>
                        <td colspan='6' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Monthly Grand Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalGross}</td>
                    </tr>
                </tbody>
            </table>
            <br>
            <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Prepared By : {User.FindFirstValue("FullName")}</td>
            
           
             <td colspan='3' style='font-size:18px;text-align:left;'>Authorized Person : ......................</td>
            
             </tr>
           <tr>
            <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Count  : {countRows}</td>
            
             
            <td colspan='3' style='font-size:18px;text-align:left;'> Authorized Date  : ......................</td>
            
             </tr>
           </tbody>
           </table>
            
        </div>

          
          
    </div>      

</body>
</html>";
            GlobalSettings globalSettings = new()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Right = 0, Left = 0 }
            };
            ObjectSettings objectSettings = new()
            {
                PagesCount = true,
                HtmlContent = html2
            };
            WebSettings webSettings = new()
            {
                DefaultEncoding = "utf-8"
            };
            HeaderSettings headerSettings = new()
            {
                FontSize = 15,
                FontName = "times new roman",
                Right = "Page [page] of [toPage]",
                Line = true
            };
            FooterSettings footerSettings1 = new();
            FooterSettings footerSettings = footerSettings1;
            footerSettings.FontSize = 10;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788";

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
            return pdfData;
        }



        #endregion


        #region Daily Sales Report

        [HttpGet]
        [Route(API_ROUTE_NAME + "/PrintDailySalesReport/{date}/{CompanyId}")]
        public IActionResult PrintDailySalesReport(string date, int CompanyId)
        {

            string dateString = date;

            if (DateTime.TryParseExact(dateString, "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(India Standard Time)'", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateObject))
            {
                dateString = dateObject.Date.Day.ToString();
            }
            else
            {
                Console.WriteLine("Invalid date string.");
            }

            byte[] reurnarray = PrintDailySalesReportData(dateString, CompanyId);
            return File(reurnarray, "application/pdf");
        }
        public byte[] PrintDailySalesReportData(string date, int CompanyId)
        {
            var modelBody = _reportService.ListInvoiceHeadSForReportDaily(date);
            var getcompany = _reportService.GetCompanyDetailsForPrint(CompanyId);
            StringBuilder TableText = new();
            decimal tot = 0m;
            decimal Discount = 0m;

            decimal Gross = 0m;
            int countRows = 0;
            foreach (var obj in modelBody)
            {
                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.Id + "</td>" +
                    "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' > " + obj.InvoiceDate.ToString("yyyy-MMM-dd") + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.Type + "  </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.UserName + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.Customer + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Total, 2).ToString("#,0.00") + " </td> " +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Discount, 2).ToString("#,0.00") + " </td> " +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Gross, 2).ToString("#,0.00") + " </td></tr> ";
                TableText.Append(rownew);
                tot += (decimal)obj.Total;
                Discount += (decimal)obj.Discount;
                Gross += (decimal)obj.Gross;
                countRows++;
            }
            var Total = tot.ToString("#,0.00");
            var TotalDiscount = Discount.ToString("#,0.00");
            var TotalGross = Gross.ToString("#,0.00");

            string tablestyle = "  html {     margin-top:-30px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

            var html2 = $@"<html lang='en''>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <title> Monthly Sales Report</title>
    <style type='text/css'>
       {tablestyle}
    </style>
</head>
<body>

    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        <div style='padding: 20px 40px;'>
            <div style='display: flex;flex-wrap: wrap;'>
                 <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
               <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName} </b></h2></td>
            
             </tr>
            <tr>
            <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>
            
             </tr>
             <tr>
            <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1} / {getcompany.TelPhone2}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'><h4>Daily Sales Report</h4></td>
            
             </tr>
          
          
           </tbody>
           </table>
                
            </div>
               
        </div>

        <div style='padding: 16px;'>
            <div style='display: flex;flex-wrap: wrap;'>
              <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
           
           
             
            
            
             </tr>
         
             <tr>
                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime():yyyy/MMM/dd hh:mm:tt}</td>
            
             </tr>

           </tbody>
           </table>
            
             
            </div>
        </div>

        <div style='padding: 16px;margin-top:-40px;'>
            <h3 style='font-size: 20px; margin-bottom: 08px;' >Details</h3>
           
            <table style='background-color: #fff;width: 100%;border-collapse: collapse;box-shadow: 0px 0px 5px 0.5px gray;'>
                <thead>
                    <tr style='border: 1px solid #111;background-color: #f2f2f2;'>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Invoice No</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Date</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Type</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 20%;text-align: center;' >Created By</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 20%;text-align: center;' >Customer Name</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Total</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Discount</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >GrandTotal</th>
                    </tr>
                </thead>
                <tbody>
                    
                      {TableText}
                    
                    <tr>
                        <td style='vertical-align: middle !important;text-align: center;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' colspan='6'>Monthly Sub Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {Total}</td>
                    </tr>
                     <tr>
                        <td colspan='6' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Monthly Discount</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalDiscount}</td>
                    </tr>
                    <tr>
                        <td colspan='6' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Monthly Grand Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalGross}</td>
                    </tr>
                </tbody>
            </table>
            <br>
            <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Prepared By : {User.FindFirstValue("FullName")}</td>
            
           
             <td colspan='3' style='font-size:18px;text-align:left;'>Authorized Person : ......................</td>
            
             </tr>
           <tr>
            <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Count  : {countRows}</td>
            
             
            <td colspan='3' style='font-size:18px;text-align:left;'> Authorized Date  : ......................</td>
            
             </tr>
           </tbody>
           </table>
            
        </div>

          
          
    </div>      

</body>
</html>";
            GlobalSettings globalSettings = new()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Right = 0, Left = 0 }
            };
            ObjectSettings objectSettings = new()
            {
                PagesCount = true,
                HtmlContent = html2
            };
            WebSettings webSettings = new()
            {
                DefaultEncoding = "utf-8"
            };
            HeaderSettings headerSettings = new()
            {
                FontSize = 15,
                FontName = "times new roman",
                Right = "Page [page] of [toPage]",
                Line = true
            };
            FooterSettings footerSettings1 = new();
            FooterSettings footerSettings = footerSettings1;
            footerSettings.FontSize = 10;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788";

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
            return pdfData;
        }



        #endregion



        #region Yearly Sales Report

        [HttpGet]
        [Route(API_ROUTE_NAME + "/PrintYearlySalesReport/{year}/{CompanyId}")]
        public IActionResult PrintYearlySalesReport(int year, int CompanyId)
        {
            //int id = Convert.ToInt32(Month.Split("-").Last());
            byte[] reurnarray = PrintYearlySalesReportData(year, CompanyId);
            return File(reurnarray, "application/pdf");
        }
        public byte[] PrintYearlySalesReportData(int id, int CompanyId)
        {
            var modelBody = _reportService.ListInvoiceHeadSForReportYear(id);
            var getcompany = _reportService.GetCompanyDetailsForPrint(CompanyId);
            StringBuilder TableText = new();
            decimal tot = 0m;
            decimal Discount = 0m;

            decimal Gross = 0m;
            int countRows = 0;
            foreach (var obj in modelBody)
            {
                string rownew = "<tr> <td style='vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.Id + "</td>" +
                    "<td style = 'vertical-align: middle !important;text-align: left;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' > " + obj.InvoiceDate.ToString("yyyy-MMM-dd") + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.Type + "  </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.UserName + " </td>" +//model.InvType == "Processing" ? "Due" : "Complete",
                        "<td style = 'vertical-align: middle !important;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + obj.Customer + " </td>" +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Total, 2).ToString("#,0.00") + " </td> " +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Discount, 2).ToString("#,0.00") + " </td> " +
                        "<td style = 'vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>" + Math.Round((decimal)obj.Gross, 2).ToString("#,0.00") + " </td></tr> ";
                TableText.Append(rownew);
                tot += (decimal)obj.Total;
                Discount += (decimal)obj.Discount;
                Gross += (decimal)obj.Gross;
                countRows++;
            }
            var Total = tot.ToString("#,0.00");
            var TotalDiscount = Discount.ToString("#,0.00");
            var TotalGross = Gross.ToString("#,0.00");

            string tablestyle = "  html {     margin-top:-30px;        line-height: 1;         }          ol, ul {             list-style: none;         }          table {             border-collapse: collapse;             border-spacing: 0;         }          caption, th, td {             text-align: left;             font-weight: normal;             vertical-align: middle;         }          q, blockquote {             quotes: none;         }              q:before, q:after, blockquote:before, blockquote:after {                 content: '';                 content: none;             }          a img {             border: none;         }          article, aside, details, figcaption, figure, footer, header, hgroup, main, menu, nav, section, summary {             display: block;         }          body {             font-family: 'Source Sans Pro', sans-serif;             font-weight: 300;             font-size: 12px;             /*margin: 0;                     padding: 0;*/             color: #777777;         }              /* body a {                         text-decoration: none;                         color: inherit;                     }                          body a:hover {                             color: inherit;                             opacity: 0.7;                         }*/              body .container {                 /* min-width: 500px;*/                 /* margin: 0 auto;*/                 /* padding: 0 30px;*/                 /* margin-top:-50px;*/             }              body .clearfix:after {                 content: '';                 display: table;                 clear: both;                 margin-top: -50px;             }              body .left {                 float: left;             }              body .right {                 float: right;             }              body .helper {                 height: 100%;             }                 /*  header {                     height: 40px;                     margin-top: 20px;                     margin-bottom: 40px;                     padding: 0px 5px 0;                 }          */         section .details {             min-width: 500px;             margin-bottom: 40px;             padding: 10px 35px;             background-color: white;             color: #ffffff;         }              section .details .client {                 width: 50%;                 line-height: 16px;             }                  section .details .client .name {                     font-weight: 600;                 }              section .details .data {                 width: 50%;                 text-align: right;             }              section .details .title {                 margin-bottom: 15px;                 font-size: 3em;                 font-weight: 400;                 text-transform: uppercase;             }          /*  section .table-wrapper {                     position: relative;                     overflow: hidden;                 }                      section .table-wrapper:before {                         content: '';                         display: block;                         position: absolute;                         top: 33px;                         left: 30px;                         width: 90%;                         height: 100%;                         border-top: 2px solid #BDB9B9;                         border-left: 2px solid #BDB9B9;                         z-index: -1;                     }*/          /*section .no-break {                     page-break-inside: avoid;                 }*/         footer {             margin-bottom: 25px;         }              footer .end {                 padding-top: 5px;                 border-top: 2px solid #2A8EAC;                 text-align: center;             } ";

            var html2 = $@"<html lang='en''>
<head>
    <meta charset = 'UTF-8'>
    <meta name = 'viewport' content = 'width=device-width, initial-scale=1.0'>
    <title> Monthly Sales Report</title>
    <style type='text/css'>
       {tablestyle}
    </style>
</head>
<body>

    <div style='width: 100%; margin-right: auto;margin-left: auto;' >
        <div style='padding: 20px 40px;'>
            <div style='display: flex;flex-wrap: wrap;'>
                 <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:Calibri'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
               <td style='text-align:center;' colspan='6'><h2><b>{getcompany.CompanyName} </b></h2></td>
            
             </tr>
            <tr>
            <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.Address}</td>
            
             </tr>
             <tr>
            <td style='text-align:center;font-size:18px;' colspan='6'>{getcompany.TelPhone1} / {getcompany.TelPhone2}</td>
            
             </tr>
             <tr>
             <td style='text-align:center;font-size:18px;' colspan='6'><h4>Monthly Sales Report</h4></td>
            
             </tr>
          
          
           </tbody>
           </table>
                
            </div>
               
        </div>

        <div style='padding: 16px;'>
            <div style='display: flex;flex-wrap: wrap;'>
              <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
           
           
             
            
            
             </tr>
         
             <tr>
                <td style='text-align:left;font-size:18px;' colspan='6'>Print Date :{CommonResources.LocalDatetime():yyyy/MMM/dd hh:mm:tt}</td>
            
             </tr>

           </tbody>
           </table>
            
             
            </div>
        </div>

        <div style='padding: 16px;margin-top:-40px;'>
            <h3 style='font-size: 20px; margin-bottom: 08px;' >Details</h3>
           
            <table style='background-color: #fff;width: 100%;border-collapse: collapse;box-shadow: 0px 0px 5px 0.5px gray;'>
                <thead>
                    <tr style='border: 1px solid #111;background-color: #f2f2f2;'>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Invoice No</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;text-align: center;width: 10%;'>Date</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Type</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 20%;text-align: center;' >Created By</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 20%;text-align: center;' >Customer Name</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Total</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >Discount</th>
                        <th style='padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6; width: 10%;text-align: center;' >GrandTotal</th>
                    </tr>
                </thead>
                <tbody>
                    
                      {TableText}
                    
                    <tr>
                        <td style='vertical-align: middle !important;text-align: center;text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;' colspan='6'>Monthly Sub Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {Total}</td>
                    </tr>
                     <tr>
                        <td colspan='6' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Monthly Discount</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalDiscount}</td>
                    </tr>
                    <tr>
                        <td colspan='6' style='text-align: center;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'>Monthly Grand Total</td>
                        <td colspan='2' style='vertical-align: middle !important;text-align: right;padding-top: 08px; padding-bottom: 08px;border: 1px solid #dee2e6;'> {TotalGross}</td>
                    </tr>
                </tbody>
            </table>
            <br>
            <table id='tbmain' width='100%' style='table-layout: fixed;
        word-wrap: break-word;
        word-break: break-all;margin-top:20px;color:black;font-family:times'>
          <thead>  
           <tr>
          <th style='width:70%;'></th>
          <th style='width:50%;'></th>
           <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
            <th style='width:50%;'></th>
         </thead>
           <tbody>
            
           
          
             <tr>
            
             <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Prepared By : {User.FindFirstValue("FullName")}</td>
            
           
             <td colspan='3' style='font-size:18px;text-align:left;'>Authorized Person : ......................</td>
            
             </tr>
           <tr>
            <td colspan='3' style='width:40%;font-size:18px;text-align:left;'>Count  : {countRows}</td>
            
             
            <td colspan='3' style='font-size:18px;text-align:left;'> Authorized Date  : ......................</td>
            
             </tr>
           </tbody>
           </table>
            
        </div>

          
          
    </div>      

</body>
</html>";
            GlobalSettings globalSettings = new()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Right = 0, Left = 0 }
            };
            ObjectSettings objectSettings = new()
            {
                PagesCount = true,
                HtmlContent = html2
            };
            WebSettings webSettings = new()
            {
                DefaultEncoding = "utf-8"
            };
            HeaderSettings headerSettings = new()
            {
                FontSize = 15,
                FontName = "times new roman",
                Right = "Page [page] of [toPage]",
                Line = true
            };
            FooterSettings footerSettings1 = new();
            FooterSettings footerSettings = footerSettings1;
            footerSettings.FontSize = 10;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "Solution provided by M System Solutions \n 077 55 49 788 / 070 65 49 788";

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            byte[] pdfData = _converter.Convert(htmlToPdfDocument);
            return pdfData;
        }



        #endregion



        #region DashBoard 
        [AllowAnonymous]
        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllBestSalerItemM")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllBestSalerItemM()
        {
            try
            {
                var Data = await _reportService.ViewAllPOsInvoiceItems();

                var response = new DataResponse<List<ViewAllPOsInvoiceItem>>
                {
                    Data = Data,
                    //Payload = payload
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }



        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetAllBestSalerItem")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllBestSalerItem()
        {
            try
            {
                var CompanyID = Convert.ToInt32(User.Identities.First().Claims.Single(s => s.Type == "CompanyId").Value);
                var Data = await _reportService.ViewAllPOsInvoiceItemForReportAsync(CompanyID);

                var response = new DataResponse<List<ViewAllPOsInvoiceItemForReport>>
                {
                    Data = Data,
                    //Payload = payload
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }


        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetReorderLevelTop10")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetReorderLevelTop10()
        {
            try
            {
                var CompanyID = Convert.ToInt32(User.Identities.First().Claims.Single(s => s.Type == "CompanyId").Value);
                var Data = await _reportService.ListReportReorderLevelItemAsyncTop10(CompanyID);

                var response = new DataResponse<List<VWAllActiveItemReorderLevelList>>
                {
                    Data = Data,
                    //Payload = payload
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }



        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetBestCustomers")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetBestCustomers()
        {
            try
            {
                var CompanyID = Convert.ToInt32(User.Identities.First().Claims.Single(s => s.Type == "CompanyId").Value);
                var Data = await _reportService.ViewAllBestCustomersForReportAsync(CompanyID);

                var response = new DataResponse<List<VwBestCustomers>>
                {
                    Data = Data,
                    //Payload = payload
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }



        [HttpGet]
        [Route(API_ROUTE_NAME + "/GETViewAllPurcheseAndRevenue")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GETViewAllPurcheseAndRevenue()
        {
            try
            {
                var cacheKey = "ViewAllPurcheseAndRevenueData";

                // Attempt to retrieve data from cache
                if (_cache.TryGetValue(cacheKey, out List<ViewAllPurcheseAndRevenue> cachedData))
                {
                    // If data is found in cache, return it
                    return Ok(new DataResponse<List<ViewAllPurcheseAndRevenue>> { Data = cachedData });
                }
                var CompanyID = Convert.ToInt32(User.Identities.First().Claims.Single(s => s.Type == "CompanyId").Value);
                // If data is not found in cache, fetch it from the service
                var data = await _reportService.ViewAllPurcheseAndRevenueForReportAsync(CompanyID);

                // Cache the fetched data
                _cache.Set(cacheKey, data, TimeSpan.FromMinutes(10)); // Cache for 10 minutes, adjust the duration as needed

                return Ok(new DataResponse<List<ViewAllPurcheseAndRevenue>> { Data = data });


            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }


        [HttpGet]
        [Route(API_ROUTE_NAME + "/GETViewAllPurcheseAndRevenueTotalWise")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GETViewAllPurcheseAndRevenueTotalWise()
        {
            try
            {
                var cacheKey = "ViewAllPurcheseAndRevenueDataTotalWIse";

                // Attempt to retrieve data from cache
                if (_cache.TryGetValue(cacheKey, out ViewAllPurcheseAndRevenue cachedData))
                {
                    // If data is found in cache, return it
                    var responsea = new DataResponse<ViewAllPurcheseAndRevenue>
                    {
                        Data = cachedData,
                        //Payload = payload
                    };
                    return Ok(responsea);
                }
                var CompanyID = Convert.ToInt32(User.Identities.First().Claims.Single(s => s.Type == "CompanyId").Value);

                // If data is not found in cache, fetch it from the service
                var data = await _reportService.ViewAllPurcheseAndRevenueTotalWiseForReportAsync(CompanyID);

                // Cache the fetched data
                _cache.Set(cacheKey, data, TimeSpan.FromMinutes(10)); // Cache for 10 minutes, adjust the duration as needed

                var response = new DataResponse<ViewAllPurcheseAndRevenue>
                {
                    Data = data,
                    //Payload = payload
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }



        [HttpGet]
        [Route(API_ROUTE_NAME + "/GetDailyMonthlyYearlySales")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetDailyMonthlyYearlySales()
        {
            try
            {
                var CompanyID = Convert.ToInt32(User.Identities.First().Claims.Single(s => s.Type == "CompanyId").Value);
                var Data = await _reportService.ViewAllViewAllPOsInvoiceItemForReportPageForReportAsync(CompanyID);

                var response = new DataResponse<ViewModelGetDailyMonthlyYearlySales>
                {
                    Data = Data,
                    //Payload = payload
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.BadRequest, new Message<string>() { Text = ex.Message });
            }
        }


        #endregion
    }
}
