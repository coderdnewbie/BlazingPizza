
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza;

public class EfRepository(PizzaStoreContext context) : IRepository
{
    public async Task<List<OrderWithStatus>> GetOrdersAsync()
	{
		var orders = await context.Orders
						.Include(o => o.DeliveryLocation)
						.Include(o => o.Pizzas).ThenInclude(p => p.Special)
						.Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
						.OrderByDescending(o => o.CreatedTime)
						.ToListAsync();

		return [.. orders.Select(o => OrderWithStatus.FromOrder(o))];
	}

	public async Task<List<OrderWithStatus>> GetOrdersAsync(string userId)
	{
		
		var orders = await context.Orders
						.Where(o => o.UserId == userId)
						.Include(o => o.DeliveryLocation)
						.Include(o => o.Pizzas).ThenInclude(p => p.Special)
						.Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
						.OrderByDescending(o => o.CreatedTime)
						.ToListAsync();

		return [.. orders.Select(o => OrderWithStatus.FromOrder(o))];

	}

	public async Task<OrderWithStatus> GetOrderWithStatus(int orderId)
	{

		//await Task.Delay(5000);

		var order = await context.Orders
						.Where(o => o.OrderId == orderId)
						.Include(o => o.DeliveryLocation)
						.Include(o => o.Pizzas).ThenInclude(p => p.Special)
						.Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
						.SingleOrDefaultAsync();

		ArgumentNullException.ThrowIfNull(order);

        return OrderWithStatus.FromOrder(order);

	}

	public async Task<OrderWithStatus> GetOrderWithStatus(int orderId, string userId)
	{
		var order = await context.Orders
						.Where(o => o.OrderId == orderId && o.UserId == userId)
						.Include(o => o.DeliveryLocation)
						.Include(o => o.Pizzas).ThenInclude(p => p.Special)
						.Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
						.SingleOrDefaultAsync();

		ArgumentNullException.ThrowIfNull(order);

        return OrderWithStatus.FromOrder(order);
	}

	public async Task<List<PizzaSpecial>> GetSpecials()
	{
		return await context.Specials.ToListAsync();
	}

	public async Task<List<Topping>> GetToppings()
	{
		return await context.Toppings.OrderBy(t => t.Name).ToListAsync();
	}

	public Task<int> PlaceOrder(Order order)
	{
		throw new NotImplementedException();
	}
}