using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{
    [Table("Booking")]
    public class Booking
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }

        public Booking()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }

        public Booking(decimal cost, DateTime date)
        {
            Id = Guid.NewGuid();
            Cost = cost;
            Date = date;
        }

    }

}
