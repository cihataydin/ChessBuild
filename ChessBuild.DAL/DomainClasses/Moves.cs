using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBuild.DAL.DomainClasses
{
    public class Moves
    {
        public int ID { get; set; }
        public string Move { get; set; }
        public virtual User User { get; set; }
    }
}
