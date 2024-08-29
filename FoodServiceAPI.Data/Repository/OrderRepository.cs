using FoodService.Models.Entities;
using FoodServiceAPI.Data.Messaging;
using FoodServiceAPI.Data.SqlServer.Context;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace FoodServiceAPI.Data.SqlServer.Repository
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly MessagePublisher _messagePublisher;

        public OrderRepository(AppDbContext context, MessagePublisher messagePublisher, ILogger<OrderRepository> logger)
            : base(context, logger)
        {
            _context = context;
            _messagePublisher = messagePublisher;
        }

        public async Task<Order> CreateOrderWithTransactionAsync(Order order)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Save the order to the database
                    var createdOrder = await _context.Order.AddAsync(order);
                    await _context.SaveChangesAsync();

                    // Prepare the message for RabbitMQ (Outbox Pattern)
                    var message = new { createdOrder.Entity.OrderId, Status = "Created" };
                    var messageBody = JsonConvert.SerializeObject(message);

                    // Save the message to the Outbox table
                    var outboxMessage = new OutboxMessage
                    {
                        Message = Encoding.UTF8.GetBytes(messageBody),
                        Processed = false // Mark as unprocessed
                    };
                    await _context.OutboxMessage.AddAsync(outboxMessage);
                    await _context.SaveChangesAsync();

                    // Commit the transaction
                    await transaction.CommitAsync();

                    // Publish the message to the RabbitMQ queue
                    _messagePublisher.Publish("order_queue", messageBody);

                    return createdOrder.Entity;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
