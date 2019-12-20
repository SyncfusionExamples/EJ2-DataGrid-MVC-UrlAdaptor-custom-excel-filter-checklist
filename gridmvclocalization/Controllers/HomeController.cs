using gridmvclocalization.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.EJ2.Base;
using Syncfusion.EJ2.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Syncfusion.EJ2;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace gridmvclocalization.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            //ViewBag.dataSource = OrdersDetails.GetAllRecords().Take(5).ToList();
            //ViewBag.dataSource1 = DetailsGrid.GetAllRecords().Where(ord => ord.EmployeeID == 1).Take(5).ToList();
            ViewBag.dataSource2 = DetailsGrid.GetAllRecords().ToList();

            return View();
        } 
      
        public ActionResult UrlDatasource(DataManagerRequest dm)
        {
            IEnumerable DataSource = OrdersDetails.GetAllRecords();
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<OrdersDetails>().Count();
            if (dm.Select != null)
            {
                DataSource = operation.PerformSelect(DataSource, dm.Select);  // Selected the columns value based on the filter request
                DataSource = DataSource.Cast<dynamic>().Distinct().AsEnumerable(); // Get the distinct values from the selected column
            }
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public Object SelectedRows(int Emp) {
            var DataSource = OrdersDetails.GetAllRecords().ToList();
            var ds = DetailsGrid.GetAllRecords().Where(emp => emp.EmployeeID == int.Parse(Emp.ToString()));            
            return Json(ds);
        }
    }
    public class OrdersDetails
    {
        public static List<OrdersDetails> order = new List<OrdersDetails>();
        public OrdersDetails()
        {

        }
        public OrdersDetails(int OrderID, string CustomerId, int EmployeeId, double Freight, bool Verified, DateTime OrderDate, string ShipCity, string ShipName, DateTime ShippedDate, string ShipAddress)
        {
            this.OrderID = OrderID;
            this.CustomerID = CustomerId;
            this.EmployeeID = EmployeeId;
            this.Freight = Freight;
            this.ShipCity = ShipCity;
            this.Verified = Verified;
            this.OrderDate = OrderDate;
            this.ShipName = ShipName;
            this.ShippedDate = ShippedDate;
            this.ShipAddress = ShipAddress;
        }
        public static List<OrdersDetails> GetAllRecords()
        {
            if (order.Count() == 0)
            {
                int code = 10000;
                    order.Add(new OrdersDetails(code + 1, "ALFKI", 1, 2.3 * 1, false, new DateTime(1991, 05, 15), "Berlin", "Simons bistro", new DateTime(1996, 7, 16), "Kirchgasse 6"));
                    order.Add(new OrdersDetails(code + 2, "ANATR", 2, 3.3 * 1, true, new DateTime(1990, 04, 04), "Madrid", "Queen Cozinha", new DateTime(1996, 9, 11), "Avda. Azteca 123"));
                    order.Add(new OrdersDetails(code + 3, "ANTON", 3, 4.3 * 1, true, new DateTime(1957, 11, 30), "Cholchester", "Frankenversand", new DateTime(1996, 10, 7), "Carrera 52 con Ave. Bolívar #65-98 Llano Largo"));
                    order.Add(new OrdersDetails(code + 4, "BLONP", 4, 5.3 * 1, false, new DateTime(1930, 10, 22), "Ernst Handel", "Austria", new DateTime(1996, 12, 30), "Magazinweg 7"));
                    order.Add(new OrdersDetails(code + 5, "BOLID", 5, 6.3 * 1, true, new DateTime(1953, 02, 18), "Hanari Carnes", "Switzerland", new DateTime(1997, 12, 3), "1029 - 12th Ave. S."));
                    order.Add(new OrdersDetails(code + 1, "ALFKI", 6, 2.3 * 1, false, new DateTime(1991, 05, 15), "Berlin", "Simons bistro", new DateTime(1996, 7, 16), "Kirchgasse 6"));
                    order.Add(new OrdersDetails(code + 2, "ANATR", 7, 3.3 * 1, true, new DateTime(1990, 04, 04), "Madrid", "Queen Cozinha", new DateTime(1996, 9, 11), "Avda. Azteca 123"));
                    order.Add(new OrdersDetails(code + 3, "ANTON", 8, 4.3 * 1, true, new DateTime(1957, 11, 30), "Cholchester", "Frankenversand", new DateTime(1996, 10, 7), "Carrera 52 con Ave. Bolívar #65-98 Llano Largo"));
                    order.Add(new OrdersDetails(code + 4, "BLONP", 9, 5.3 * 1, false, new DateTime(1930, 10, 22), "Ernst Handel", "Austria", new DateTime(1996, 12, 30), "Magazinweg 7"));
            }
            return order;
        }

        public int? OrderID { get; set; }
        public string CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public double? Freight { get; set; }
        public string ShipCity { get; set; }
        public bool Verified { get; set; }
        public DateTime OrderDate { get; set; }

        public string ShipName { get; set; }

        public DateTime ShippedDate { get; set; }
        public string ShipAddress { get; set; }
    }
    public class DetailsGrid
    {
        public static List<DetailsGrid> order = new List<DetailsGrid>();
        public DetailsGrid()
        {

        }
        public DetailsGrid(string CustomerId, int EmployeeId, string ShipCity, string ShipName, string ShipCountry, string Status)
        {
            this.CustomerID = CustomerId;
            this.EmployeeID = EmployeeId;
            this.ShipCity = ShipCity;
            this.ShipName = ShipName;
            this.ShipCountry = ShipCountry;
            this.Status = Status;
        }
        public static List<DetailsGrid> GetAllRecords()
        {
            if (order.Count() == 0)
            {
                    order.Add(new DetailsGrid( "ALFKI", 1, "Berlin", "Simons bistro", "Denmark", "Open"));
                    order.Add(new DetailsGrid("ANTON", 3, "Cholchester", "Frankenversand", "Germany", "InProgress"));
                    order.Add(new DetailsGrid( "ANATR", 2, "Madrid", "Queen Cozinha", "Brazil", "Cancel"));
                    order.Add(new DetailsGrid("BLONP", 4, "Marseille", "Ernst Handel", "Austria","Closed"));
                    order.Add(new DetailsGrid("ALFKI", 6, "Berlin", "Simons bistro", "Denmark","validated"));
                    order.Add(new DetailsGrid("BLONP", 7, "Marseille", "Ernst Handel", "Austria","Code Review"));
                    order.Add(new DetailsGrid("BOLID", 5, "Tsawassen", "Hanari Carnes", "Switzerland", "Rejected"));
                    order.Add(new DetailsGrid( "BLONP", 9, "Marseille", "Ernst Handel", "Austria", "New"));
                    order.Add(new DetailsGrid("BOLID", 8, "Tsawassen", "Hanari Carnes", "Switzerland", "Old"));
            }
            return order;
        }
        
        public string CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public string ShipCity { get; set; }

        public string ShipName { get; set; }

        public string Status { get; set; }

        public string ShipCountry { get; set; }
    }
}