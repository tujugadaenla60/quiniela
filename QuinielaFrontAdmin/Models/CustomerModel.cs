using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuinielaFrontAdmin.Models
{
    public class CustomerModel
    {
        public long? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int? StateId { get; set; }
        public StateModel State { get; set; }
        public int? StateCityId { get; set; }
        public StateCityModel StateCity { get; set; }
        public decimal? Balance { get; set; }
        public bool? PositiveBalance { get; set; }
    }
}