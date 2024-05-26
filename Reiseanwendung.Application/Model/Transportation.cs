using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{
    [Table("Transportation")]
    public class Transportation
    {
        public Guid Id { get; set; }
        public string? Type { get; set; }


        private Transportation()
        {
            Id = Guid.NewGuid();
        }


        public Transportation(string type)
        {
            Id = Guid.NewGuid();
            Type = type;
        }
    }

}
