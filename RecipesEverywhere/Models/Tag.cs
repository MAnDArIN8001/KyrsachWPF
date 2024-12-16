using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesEverywhere.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!; 

        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    }
}
