using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace RewardsWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
                      
               
                List<Transaction> transactions = new List<Transaction>
        {
            new Transaction("Customer1", "January", 120),
            new Transaction("Customer1", "February", 75),
            new Transaction("Customer2", "January", 110),
            new Transaction("Customer2", "February", 60),
           
        };
            //We can use dbcontext
            // transactions = await PointService.CustomerPoints.FindAsync();
            // Calculate and display points earned
            CalculateAndDisplayPoints(transactions);
        }

            static void CalculateAndDisplayPoints(List<Transaction> transactions)
            {
                Dictionary<string, Dictionary<string, int>> pointsPerCustomerPerMonth = new Dictionary<string, Dictionary<string, int>>();
                Dictionary<string, int> totalPointsPerCustomer = new Dictionary<string, int>();

                foreach (Transaction transaction in transactions)
                {
                    int points = CalculatePoints(transaction.Amount);

                    if (!pointsPerCustomerPerMonth.ContainsKey(transaction.Customer))
                    {
                        pointsPerCustomerPerMonth[transaction.Customer] = new Dictionary<string, int>();
                    }

                    if (!totalPointsPerCustomer.ContainsKey(transaction.Customer))
                    {
                        totalPointsPerCustomer[transaction.Customer] = 0;
                    }

                    if (!pointsPerCustomerPerMonth[transaction.Customer].ContainsKey(transaction.Month))
                    {
                        pointsPerCustomerPerMonth[transaction.Customer][transaction.Month] = 0;
                    }

                    pointsPerCustomerPerMonth[transaction.Customer][transaction.Month] += points;
                    totalPointsPerCustomer[transaction.Customer] += points;
                }

                // Display points earned per customer per month
                foreach (var customerEntry in pointsPerCustomerPerMonth)
                {
                    Console.WriteLine($"Customer: {customerEntry.Key}");
                    foreach (var monthEntry in customerEntry.Value)
                    {
                        Console.WriteLine($"  {monthEntry.Key}: {monthEntry.Value} points");
                    }
                    Console.WriteLine($"Total: {totalPointsPerCustomer[customerEntry.Key]} points\n");
                }
            }

            static int CalculatePoints(double amount)
            {
                int points = 0;

                if (amount > 100)
                {
                    points += (int)((amount - 100) * 2);
                }

                if (amount > 50)
                {
                    points += (int)((amount - 50) * 1);
                }

                return points;
            }     
           

    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
class Transaction
{
    public string Customer { get; }
    public string Month { get; }
    public double Amount { get; }

    public Transaction(string customer, string month, double amount)
    {
        Customer = customer;
        Month = month;
        Amount = amount;
    }
}

