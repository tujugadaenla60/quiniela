using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuinielaFrontAdmin.Models
{
    public class StateCityModel
    {
        public int? Id { get; set; }
        public int? StateId { get; set; }
        public string Name { get; set; }
        public StateModel State { get; set; }
    }
}