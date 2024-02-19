using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestCaseNetCrud.Data;
using TestCaseNetCrud.Models;

namespace TestCaseNetCrud.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly Uri apiUrl = new Uri("http://localhost:54352/api/");

        // GET: Suppliers
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = apiUrl;
                HttpResponseMessage response = await client.GetAsync("Supplier/GetAllSuppliers");

                if (response.IsSuccessStatusCode)
                {
                    var listSuppliers = await response.Content.ReadAsAsync<List<SuppliersEntity>>();
                    response.Dispose();
                    client.Dispose();
                    return View(listSuppliers);
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

        // GET: Suppliers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = apiUrl;
                HttpResponseMessage response = await client.GetAsync($"Supplier/GetSupplierDetail/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var supplier = await response.Content.ReadAsAsync<SuppliersEntity>();
                    response.Dispose();
                    client.Dispose();
                    return View(supplier);
                }
                else
                {
                    response.Dispose();
                    client.Dispose();
                    return View("Error");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                client.Dispose();
                return View("Error");
            }
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        public async Task<ActionResult> Create(SuppliersEntity input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = apiUrl;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsJsonAsync("Supplier/CreateSupplier", input);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        response.Dispose();
                        client.Dispose();
                        return RedirectToAction("Index");
                    }
                    response.Dispose();
                    client.Dispose();
                }
                return View();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return View();
            }
        }

        // GET: Suppliers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = apiUrl;
                HttpResponseMessage response = await client.GetAsync($"Supplier/GetSupplierDetail/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var supplier = await response.Content.ReadAsAsync<SuppliersEntity>();
                    response.Dispose();
                    client.Dispose();
                    return View(supplier);
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

        // POST: Suppliers/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, SuppliersEntity input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = apiUrl;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PutAsJsonAsync($"Supplier/EditSupplier/{id}", input);

                    if (response.IsSuccessStatusCode)
                    {
                        response.Dispose();
                        client.Dispose();
                        return RedirectToAction("Index");
                    }
                    response.Dispose();
                    client.Dispose();
                }
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return View();
            }
        }

        // GET: Suppliers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = apiUrl;
                HttpResponseMessage response = await client.GetAsync($"Supplier/GetSupplierDetail/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var supplier = await response.Content.ReadAsAsync<SuppliersEntity>();
                    response.Dispose();
                    client.Dispose();
                    return View(supplier);
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

        // POST: Suppliers/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, SuppliersEntity input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = apiUrl;
                    HttpResponseMessage response = await client.DeleteAsync($"Supplier/DeleteSupplier/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        response.Dispose();
                        client.Dispose();
                        return RedirectToAction("Index");
                    }

                    response.Dispose();
                    client.Dispose();
                }
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return View();
            }
        }
    }
}
