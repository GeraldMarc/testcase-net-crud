using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestCaseNetCrud.Data;
using TestCaseNetCrud.Models;

namespace TestCaseNetCrud.Controllers
{
    [RoutePrefix("api/Supplier")]
    public class SuppliersApiController : ApiController
    {
        [Route("GetAllSuppliers")]
        // GET: api/Supplier/GetAllSuppliers
        public IHttpActionResult GetAllSuppliers()
        {
            List<SuppliersEntity> listSuppliers = new List<SuppliersEntity>();
            SuppliersRepository suppliersRepository = new SuppliersRepository();
            listSuppliers = suppliersRepository.GetAll();

            return Ok(listSuppliers);
        }

        [Route("GetSupplierDetail/{id}")]
        // GET: api/Supplier/GetSupplierDetail/{id}
        public IHttpActionResult GetSupplierDetail(string id)
        {
            SuppliersEntity supplier = new SuppliersEntity();
            SuppliersRepository suppliersRepository = new SuppliersRepository();
            supplier = suppliersRepository.GetById(id);

            return Ok(supplier);
        }

        [Route("CreateSupplier")]
        // GET: api/Supplier/CreateSupplier
        public IHttpActionResult CreateSupplier(SuppliersEntity input)
        {
            SuppliersRepository suppliersRepository = new SuppliersRepository();
            suppliersRepository.CreateSupplier(input);

            return Ok();
        }

        [Route("EditSupplier/{id}")]
        // PUT: api/Supplier/EditSupplier/{id}
        public IHttpActionResult EditSupplier(string id, SuppliersEntity input)
        {
            SuppliersRepository suppliersRepository = new SuppliersRepository();
            suppliersRepository.EditSupplier(id, input);

            return Ok();
        }

        [Route("DeleteSupplier/{id}")]
        // DELETE: api/Supplier/DeleteSupplier/{id}
        public IHttpActionResult DeleteSupplier(string id)
        {
            SuppliersRepository suppliersRepository = new SuppliersRepository();
            suppliersRepository.DeleteSupplier(id);

            return Ok();
        }
    }

    [RoutePrefix("api/Product")]
    public class ProductsApiController : ApiController
    {
        [Route("GetAllProducts")]
        // GET: api/Product/GetAllProducts
        public IHttpActionResult GetAllProducts()
        {
            List<ProductsEntity> listProducts = new List<ProductsEntity>();
            ProductsRepository productsRepository = new ProductsRepository();
            listProducts = productsRepository.GetAll();

            return Ok(listProducts);
        }

        [Route("GetProductDetail/{id}")]
        // GET: api/Product/GetProductDetail/{id}
        public IHttpActionResult GetProductDetail(string id)
        {
            ProductsEntity product = new ProductsEntity();
            ProductsRepository productsRepository = new ProductsRepository();
            product = productsRepository.GetById(id);

            return Ok(product);
        }

        [Route("CreateProduct")]
        // GET: api/Product/CreateProduct
        public IHttpActionResult CreateProduct(ProductsEntity input)
        {
            ProductsRepository productsRepository = new ProductsRepository();
            productsRepository.CreateProduct(input);

            return Ok();
        }

        [Route("EditProduct/{id}")]
        // PUT: api/Product/EditProduct/{id}
        public IHttpActionResult EditProduct(string id, ProductsEntity input)
        {
            ProductsRepository productsRepository = new ProductsRepository();
            productsRepository.EditProduct(id, input);

            return Ok();
        }

        [Route("DeleteProduct/{id}")]
        // DELETE: api/Product/DeleteProduct/{id}
        public IHttpActionResult DeleteProduct(string id)
        {
            ProductsRepository productsRepository = new ProductsRepository();
            productsRepository.DeleteProduct(id);

            return Ok();
        }
    }

    [RoutePrefix("api/PurchaseOrder")]
    public class PurchaseOrdersApiController : ApiController
    {
        [Route("GetAllPurchaseOrders")]
        // GET: api/PurchaseOrder/GetAllPurchaseOrders
        public IHttpActionResult GetAllPurchaseOrders()
        {
            List<PurchaseOrdersEntity> listPurchaseOrders = new List<PurchaseOrdersEntity>();
            PurchaseOrdersRepository purchaseOrdersRepository = new PurchaseOrdersRepository();
            listPurchaseOrders = purchaseOrdersRepository.GetAll();

            return Ok(listPurchaseOrders);
        }

        [Route("GetPurchaseOrderDetail/{id}")]
        // GET: api/PurchaseOrder/GetPurchaseOrderDetail/{id}
        public IHttpActionResult GetPurchaseOrderDetail(string id)
        {
            PurchaseOrdersViewModel purchaseOrder = new PurchaseOrdersViewModel();
            PurchaseOrdersRepository purchaseOrdersRepository = new PurchaseOrdersRepository();
            purchaseOrder = purchaseOrdersRepository.GetById(id);

            return Ok(purchaseOrder);
        }
    }
}
