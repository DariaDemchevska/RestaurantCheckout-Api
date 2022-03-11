using System.Collections.Generic;
using System;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using RestaurantCheckout.Controllers;
using RestaurantCheckout.Models;

namespace RestaurantCheckoutSpecFlowTests.Steps
{
  [Binding]
  public class OrderCalculateSteps
  {
    private decimal _actualResult;
    private List<OrderDetails> _orderDetailsList;

    [Given(@"Group of people makes an order")]
    public void GivenGroupOfPeopleMakesAnOrder(Table table)
    {
      _orderDetailsList = table.CreateSet<OrderDetails>().ToList();
    }

    [When(@"the order is sent to the endpoint")]
    public void WhenTheOrderIsSentToTheEndpoint()
    {
      _actualResult = new OrderController().GetTotal(_orderDetailsList);
    }

    [Then(@"the total is calculated correctly")]
    public void ThenTheTotalIsCalculatedCorrectly(Table table)
    {
      string expectedTotalString = table.Rows[0]["ExpectedTotal"];
      decimal expectedResult = Convert.ToDecimal(expectedTotalString);
      
      Assert.AreEqual(expectedResult, _actualResult, 
        $"The total is not calculated correctly. Must be {expectedResult}, but {_actualResult}.");
    }

    [Then(@"more people joined who order")]
    public void ThenMorePeopleJoinedWhoOrder(Table table)
    {
      List<OrderDetails> joinedCustomers=table.CreateSet<OrderDetails>().ToList();
      foreach (var customer in joinedCustomers)
      {
        _orderDetailsList.Add(customer);
      }
    }

    [Then(@"some people cancel the order")]
    public void ThenSomePeopleCancelTheOrder(Table table)
    {
      List<OrderDetails> customersToCancel = table.CreateSet<OrderDetails>().ToList();
      IEnumerable<int> customersIdToCancel= customersToCancel.Select(x => x.CustomerId);

      foreach (var id in customersIdToCancel)
      {
        _orderDetailsList.RemoveAll(x => x.CustomerId == id);
      }
    }
  }
}
