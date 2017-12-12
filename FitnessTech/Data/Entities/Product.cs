using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTech.Data.Entities
{
  public class Product
  {
    public int Id { get; set; }
    public string Category { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public string Title { get; set; }
    public string SupplementDescription { get; set; }
    public string SupplementId { get; set; }
    public string Producer { get; set; }
  }
}
