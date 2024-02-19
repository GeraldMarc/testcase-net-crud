using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestCaseNetCrud.Data;
using TestCaseNetCrud.Models;

namespace TestCaseNetCrud.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private readonly Uri apiUrl = new Uri("http://localhost:54352/api/");

        // GET: PurchaseOrders
        public async Task<ActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = apiUrl;
                    HttpResponseMessage response = await client.GetAsync("PurchaseOrder/GetAllPurchaseOrders");

                    if (response.IsSuccessStatusCode)
                    {
                        var listPurchaseOrders = await response.Content.ReadAsAsync<List<PurchaseOrdersEntity>>();
                        return View(listPurchaseOrders);
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return View("Error");
                }
            }
        }

        // GET: PurchaseOrders/Details/5
        public async Task<ActionResult> Details(string id)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = apiUrl;
                HttpResponseMessage response = await client.GetAsync($"PurchaseOrder/GetPurchaseOrderDetail/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var purchaseOrdersViewModel = await response.Content.ReadAsAsync<PurchaseOrdersViewModel>();
                    response.Dispose();
                    client.Dispose();
                    return View(purchaseOrdersViewModel);
                }
                else
                {
                    response.Dispose();
                    client.Dispose();
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                client.Dispose();
                return View("Error");
            }
        }

        // GET: PurchaseOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaseOrders/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PurchaseOrders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PurchaseOrders/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PurchaseOrders/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
