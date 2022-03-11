Feature: OrderCalculate

Check if an endpoint that calculates the total of the order and adds a 10% service charge on food.
Drinks have a 30% discount when ordered before 19:00

@Scenario1
Scenario: A group of 4 people orders 4 starters, 4 mains and 4 drinks
	Given Group of people makes an order
	| CustomerId | Starter | Main | Drink | Time				| 
	| 1          | 4       | 7    | 2.5   | 03/08/2022 19:00:00 |
	| 2          | 4       | 7    | 2.5   | 03/08/2022 19:00:00 |
	| 3          | 4       | 7    | 2.5   | 03/08/2022 19:00:00 |
	| 4          | 4       | 7    | 2.5   | 03/08/2022 19:00:00 |
	When the order is sent to the endpoint
	Then the total is calculated correctly
	| ExpectedTotal |
	| 58.4          | 

@Scenario2
Scenario: A group of 2 people order 1 starter and 2 mains and 2 drinks before 19:00. Two people join at 20:00 who order 2 mains and 2 drinks
	Given Group of people makes an order
	| CustomerId | Starter | Main | Drink | Time				|	
	| 1          | 4       | 7    | 2.5   | 03/08/2022 18:59:00 |
	| 2          | 0       | 7    | 2.5   | 03/08/2022 18:59:00 |
	When the order is sent to the endpoint
	Then the total is calculated correctly
	| ExpectedTotal |
	| 23.3          |
	And more people joined who order
	| CustomerId | Starter | Main | Drink | Time				|
	| 3          | 0       | 7    | 2.5   | 03/08/2022 20:00:00 | 
	| 4          | 0       | 7    | 2.5   | 03/08/2022 20:00:00 | 
	When the order is sent to the endpoint
	Then the total is calculated correctly
	| ExpectedTotal |
	| 43.7          |

@Scenario3
Scenario: A group of 4 people order 4 starters, 4 mains and 4 drinks. 1 person cancels their order so the order is 3 starters, 3 mains and 3 drinks
	Given Group of people makes an order
	| CustomerId | Starter | Main | Drink | Time				 |
	| 1          | 4       | 7    | 2.5   | 03/08/2022 20:00:00  |
	| 2          | 4       | 7    | 2.5   | 03/08/2022 20:00:00  | 
	| 3          | 4       | 7    | 2.5   | 03/08/2022 20:00:00  | 
	| 4          | 4       | 7    | 2.5   | 03/08/2022 20:00:00  | 
	When the order is sent to the endpoint
	Then the total is calculated correctly
	| ExpectedTotal |
	| 58.4          |
	And some people cancel the order
	| CustomerId | Starter | Main | Drink | Time				|
	| 4          | 4       | 7    | 2.5   | 03/08/2022 20:00:00 | 
	When the order is sent to the endpoint
	Then the total is calculated correctly
	| ExpectedTotal |
	| 43.8          |


