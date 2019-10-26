using DL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;


namespace WebApplication2.Controllers
{
    public class OrderApiController : ApiController
    {
        private Model1 db = new Model1();

  
        public IQueryable<Orders> GetOrdersBooks()
        {
            return db.Orders;
        }

        [ResponseType(typeof(Orders))]
        public IHttpActionResult GetOrdersBooks(int id)
        {
            Orders ordersBooks = db.Orders.Find(id);
            if (ordersBooks == null)
            {
                return NotFound();
            }

            return Ok(ordersBooks);
        }

    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrdersBooks(int id, Orders ordersBooks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ordersBooks.Id)
            {
                return BadRequest();
            }

            db.Entry(ordersBooks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersBooksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        [ResponseType(typeof(Orders))]
        public IHttpActionResult PostOrdersBooks(Orders ordersBooks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ordersBooks.CreateDate = DateTime.Today;
            ordersBooks.ReturnDate = DateTime.Today;
            db.Orders.Add(ordersBooks);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ordersBooks.Id }, ordersBooks);
        }


        [ResponseType(typeof(Orders))]
        public IHttpActionResult DeleteOrdersBooks(int id)
        {
            Orders ordersBooks = db.Orders.Find(id);
            if (ordersBooks == null)
            {
                return NotFound();
            }

            db.Orders.Remove(ordersBooks);
            db.SaveChanges();

            return Ok(ordersBooks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdersBooksExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}