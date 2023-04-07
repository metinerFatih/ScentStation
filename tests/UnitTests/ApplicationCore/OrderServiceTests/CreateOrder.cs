using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Ardalis.Specification;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ApplicationCore.OrderServiceTests
{
    public class CreateOrder
    {
        private readonly string _demoBuyerId = "This-Is-A-Random-Buyer-Id";

        private readonly Address _demoAddress = new Address()
        {
            City = "London",
            Country = "UK",
            Street = "221B Baker Street",
            ZipCode = "NW1 6XE"
        };

        [Fact]
        public async Task ShouldThrowEmptyBasketExceptionIfBasketIsEmpty()
        {
            var mocBasketRepo = new Mock<IRepository<Basket>>();
            var mocBasketItemRepo = new Mock<IRepository<BasketItem>>();
            var mocProductRepo = new Mock<IRepository<Product>>();
            var mocOrderRepo = new Mock<IRepository<Order>>();

            Basket emptyBasket = new Basket()
            {
                Id = 33,
                BuyerId = _demoBuyerId,
                BasketItems = new List<BasketItem>()
            };

            mocBasketRepo.Setup(b => b.FirstOrDefaultAsync(It.IsAny<Specification<Basket>>())).ReturnsAsync(emptyBasket);

            var basketRepo = mocBasketRepo.Object;
            var basketItemRepo = mocBasketItemRepo.Object;
            var productRepo = mocProductRepo.Object;
            var orderRepo = mocOrderRepo.Object;

            IBasketService basketService = new BasketService(basketRepo, basketItemRepo, productRepo);
            IOrderService orderService = new OrderService(basketService, orderRepo);

            //İddia ediyorum (assert) fırlatılmalıdır(throw) EmptyBasketException hatasını
            //orderService sınıfın CreateOrderAsync metodu boş sepete sahip bir kullanıcı için çağırıldığında
            await Assert.ThrowsAsync<EmptyBasketException>(async () =>
                await orderService.CreateOrderAsync(_demoBuyerId, _demoAddress));
        }
    }
}
