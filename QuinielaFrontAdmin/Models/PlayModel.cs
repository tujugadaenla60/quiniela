using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Web;

namespace QuinielaFrontAdmin.Models
{
    public class PlayModel
    {
        public long? Id { get; set; }
        public long? CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public int? GameId { get; set; }
        public GameModel Game { get; set; }
        public int? ModeId { get; set; }
        public GameModeModel GameMode { get; set; }
        public long? DrawId { get; set; }
        public DrawModel Draw { get; set; }
        public int? StatusId { get; set; }
        public PlayStatusModel Status { get; set; }
        public string OfficialTicket { get; set; }
        public bool? Delivered { get; set; }
        public decimal? Total { get; set; }
        public bool? Winner { get; set; }
        public decimal? Prize { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public PlayQuniModel QuiniDetail { get; set; }
        public PlayLotoModel LotoDetail { get; set; }

    }

    public class PlayQuniModel
    {
        public long? Id { get; set; }
        public long? PlayId { get; set; }
        public int Option1 { get; set; }
        public int Option2 { get; set; }
        public int Option3 { get; set; }
        public int Option4 { get; set; }
        public int Option5 { get; set; }
        public int Option6 { get; set; }

    }

    public class PlayLotoModel
    {
        public long? Id { get; set; }
        public long? PlayId { get; set; }
        public int Option1 { get; set; }
        public int Option2 { get; set; }
        public int Option3 { get; set; }
        public int Option4 { get; set; }
        public int Option5 { get; set; }
        public int Option6 { get; set; }
        public int Jack1 { get; set; }
        public int Jack2 { get; set; }
    }

}