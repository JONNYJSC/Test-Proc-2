using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Prod_2.Models
{
    public class Value
    {
        [Key]
        public int Id { get; set; }
        public int Value1 { get; set; }
        public string Value2 { get; set; }
    }
}
