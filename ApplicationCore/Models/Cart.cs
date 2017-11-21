using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }

        public List<CartEntry> CartEntries { get; set; } = new List<CartEntry>();

    }
}
