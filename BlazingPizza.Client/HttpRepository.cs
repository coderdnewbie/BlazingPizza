using System.Net.Http.Json;

namespace BlazingPizza.Client;

public class HttpRepository(HttpClient httpClient) : IRepository
{
    public Task<List<OrderWithStatus>> GetOrdersAsync()
	{
		throw new NotImplementedException();
	}

	public Task<List<OrderWithStatus>> GetOrdersAsync(string userId)
	{
		throw new NotImplementedException();
	}

	public Task<OrderWithStatus> GetOrderWithStatus(int orderId)	
	{
		throw new NotImplementedException();
	}

	public Task<OrderWithStatus> GetOrderWithStatus(int orderId, string userId)
	{
		throw new NotImplementedException();
	}

	public async Task<List<PizzaSpecial>> GetSpecials()
	{
		return await httpClient.GetFromJsonAsync<List<PizzaSpecial>>("specials") ?? [];
	}

	public async Task<List<Topping>> GetToppings()
	{
		return await httpClient.GetFromJsonAsync<List<Topping>>("toppings") ?? [];
	}

	public async Task<int> PlaceOrder(Order order)
	{
		var response = await httpClient.PostAsJsonAsync("orders", order);
		var newOrderId = await response.Content.ReadFromJsonAsync<int>();
		return newOrderId;

	}
}
