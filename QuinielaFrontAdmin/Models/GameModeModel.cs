using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuinielaFrontAdmin.Models
{
    public class GameModeModel
    {
        public int? Id { get; set; }
        public int? GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}