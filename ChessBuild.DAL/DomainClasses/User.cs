using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChessBuild.DAL.DomainClasses
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Online { get; set; }
        public bool InQueue { get; set; }
        public virtual List<Moves> Moves { get; set; }
    }
}
