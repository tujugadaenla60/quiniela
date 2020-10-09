using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuinielaFrontAdmin.Models
{
    public class DrawModel
    {
        public long? Id { get; set; }
        public int? GameId { get; set; }
        public GameModel Game { get; set; }
        public int? GameModeId { get; set; }
        public GameModeModel GameMode { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Number { get; set; }
        public int? StatusId { get; set; }
        public DrawStatusModel Status { get; set; }
    }
}