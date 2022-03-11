using System;

namespace RestaurantCheckout.Models
{
  public class OrderDetails
  {
    public int CustomerId { get; set; }
    public decimal Starter { get; set; }
    public decimal Main { get; set; }
    public decimal Drink { get; set; }
    public DateTime Time { get; set; }
  }
}
