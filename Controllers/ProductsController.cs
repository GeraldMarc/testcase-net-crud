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
    public class ProductsController : Controller
    {
        private readonly Uri apiUrl = new Uri("http://localhost:54352/api/");

        // GET: Products
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = apiUrl;
                HttpResponseMessage response = await client.GetAsync("Product/GetAllProducts");

                if (response.IsSuccessStatusCode)
                {
                    var listProducts = await response.Content.ReadAsAsync<List<ProductsEntity>>();
                    response.Dispose();
                    client.Dispose();
                    return View(listProducts);
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

        // GET: Products/Details/5
        public async Task<ActionResult> Details(string id)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = apiUrl;
                HttpResponseMessage response = await client.GetAsync($"Product/GetProductDetail/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var product = await response.Content.ReadAsAsync<ProductsEntity>();
                    response.Dispose();
                    client.Dispose();
                    return View(product);
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

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProductsEntity input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = apiUrl;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsJsonAsync("Product/CreateProduct", input);

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

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = apiUrl;
                HttpResponseMessage response = await client.GetAsync($"Product/GetProductDetail/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var product = await response.Content.ReadAsAsync<ProductsEntity>();
                    response.Dispose();
                    client.Dispose();
                    return View(product);
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

        // POST: Products/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, ProductsEntity input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = apiUrl;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PutAsJsonAsync($"Product/EditProduct/{id}", input);

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

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = apiUrl;
                HttpResponseMessage response = await client.GetAsync($"Product/GetProductDetail/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var product = await response.Content.ReadAsAsync<ProductsEntity>();
                    response.Dispose();
                    client.Dispose();
                    return View(product);
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

        // POST: Products/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, ProductsEntity input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = apiUrl;
                    HttpResponseMessage response = await client.DeleteAsync($"Product/DeleteProduct/{id}");

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
