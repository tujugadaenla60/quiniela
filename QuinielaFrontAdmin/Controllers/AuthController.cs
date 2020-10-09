using QuinielaFrontAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace QuinielaFrontAdmin.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CustomerCreate(CustomerModel customer)
        {
            CustomerModel oCustomer;
            try
            {
                if (!String.IsNullOrEmpty(customer.Email.Trim()) && !String.IsNullOrEmpty(customer.Password))
                {

                  oCustomer = DataAccess.CustomerDAC.Create(customer) ;
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(oCustomer);
        }
    }
}