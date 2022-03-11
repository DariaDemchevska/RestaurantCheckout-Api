using System.Collections.Generic;
using System.Linq;
using RestaurantCheckout.Models;

namespace RestaurantCheckout.Controllers
{
  public class OrderController
  {
    private readonly DiscountSettings _discountSettings = new DiscountSettings();
    private readonly ServiceChargeSettings _serviceChargeSettings = new ServiceChargeSettings();

    public decimal GetTotal(List<OrderDetails> orderDetailsList)
    {
      decimal startersTotal = orderDetailsList.Sum(x => x.Starter);
      decimal mainsTotal = orderDetailsList.Sum(x => x.Main);
      decimal drinksTotal = orderDetailsList.Sum(x => x.Drink);

      decimal discountAmount = GetDiscount(orderDetailsList);
      decimal serviceCharge = GetServiceCharge(startersTotal, mainsTotal);

      decimal total = startersTotal + mainsTotal + drinksTotal+serviceCharge-discountAmount;

      return total;
    }

    private decimal GetDiscount(List<OrderDetails> orderDetailsList)
    {
      decimal drinksForDiscount = orderDetailsList
        .Where(x => x.Time.Hour < _discountSettings.TimeFlag)
        .Sum(x => x.Drink);
      decimal discountAmount = drinksForDiscount * _discountSettings.Percentage / 100;

      return discountAmount;
    }

    private decimal GetServiceCharge(decimal startersTotal, decimal mainsTotal)
    {
      decimal serviceCharge = (startersTotal + mainsTotal) * _serviceChargeSettings.ServiceCharge/100;

      return serviceCharge;
    }
  }
}
