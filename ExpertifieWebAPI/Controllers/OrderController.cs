using Microsoft.AspNetCore.Mvc;

namespace ExpertifieWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        // A simple in-memory list to simulate a database for demonstration purposes
        private static List<Order> _orders = new List<Order>()
    {
        new Order { Id = 1, CustomerName = "Alice", TotalAmount = 100 },
        new Order { Id = 2, CustomerName = "Bob", TotalAmount = 75 }
    };

        /// <summary>
        /// GET: api/Order/hello
        /// A simple greeting endpoint.
        /// </summary>
        [HttpGet("hello")]
        public ActionResult<string> Hello()
        {
            return "Hello from OrderController!";
        }

        /// <summary>
        /// GET: api/Order/{orderId}
        /// Retrieves order details by Order ID.
        /// </summary>
        /// <param name="orderId">The ID of the order to retrieve.</param>
        [HttpGet("{orderId}")]
        public ActionResult<Order> GetOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound($"Order with ID {orderId} not found."); // Returns a 404 Not Found response.
            }
            return Ok(order); // Returns a 200 OK response with the order details.
        }

        /// <summary>
        /// POST: api/Order
        /// Creates a new order.
        /// </summary>
        /// <param name="newOrder">The order object to create.</param>
        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order newOrder)
        {
            // Basic validation (you would have more robust validation in a real application).
            if (newOrder == null)
            {
                return BadRequest("Order data is invalid."); // Returns a 400 Bad Request response.
            }

            // Assign a new ID (in a real application, this would typically be handled by the database)
            newOrder.Id = _orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;
            _orders.Add(newOrder);

            // Returns a 201 Created response with the newly created order and its location.
            return CreatedAtAction(nameof(GetOrder), new { orderId = newOrder.Id }, newOrder);
        }

        /// <summary>
        /// PUT: api/Order/{orderId}
        /// Updates an existing order.
        /// </summary>
        /// <param name="orderId">The ID of the order to update.</param>
        /// <param name="updatedOrder">The updated order object.</param>
        [HttpPut("{orderId}")]
        public ActionResult UpdateOrder(int orderId, [FromBody] Order updatedOrder)
        {
            if (updatedOrder == null || orderId != updatedOrder.Id)
            {
                return BadRequest("Order ID mismatch or invalid order data."); // Returns a 400 Bad Request response.
            }

            var existingOrder = _orders.FirstOrDefault(o => o.Id == orderId);
            if (existingOrder == null)
            {
                return NotFound($"Order with ID {orderId} not found."); // Returns a 404 Not Found response.
            }

            // Update the properties of the existing order (in a real app, this interacts with a database)
            existingOrder.CustomerName = updatedOrder.CustomerName;
            existingOrder.TotalAmount = updatedOrder.TotalAmount;
            // Update other properties as needed

            return NoContent(); // Returns a 204 No Content response, indicating success without new content.
        }

        /// <summary>
        /// DELETE: api/Order/{orderId}
        /// Deletes an order by ID.
        /// </summary>
        /// <param name="orderId">The ID of the order to delete.</param>
        [HttpDelete("{orderId}")]
        public ActionResult DeleteOrder(int orderId)
        {
            var orderToRemove = _orders.FirstOrDefault(o => o.Id == orderId);
            if (orderToRemove == null)
            {
                return NotFound($"Order with ID {orderId} not found."); // Returns a 404 Not Found response.
            }

            _orders.Remove(orderToRemove);

            return NoContent(); // Returns a 204 No Content response, indicating successful deletion without new content.
        }
    }
}
