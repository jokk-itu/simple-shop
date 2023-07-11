using System.Linq;
using System.Threading;
using Api.Contracts;
using Api.Domain.ItemAggregate;
using Api.Domain.OrderAggregate;
using Api.Infrastructure;
using Api.Infrastructure.Abstract;
using Api.Infrastructure.Handlers;
using Api.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShopContext>((serviceProvider, dbContextBuilder) =>
{
  dbContextBuilder.UseInMemoryDatabase("Shop");
  dbContextBuilder.AddInterceptors(new DomainEventInterceptor(serviceProvider));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository<Item>, Repository<Item>>();
builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();

builder.Services.AddScoped<IDomainEventHandler<ItemAddedToOrderEvent>, ItemAddedToOrderEventHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/item/{itemId:int}",
  async (
    int itemId,
    [FromServices] IRepository<Item> itemRepository,
    CancellationToken cancellationToken) =>
  {
    var item = await itemRepository.GetById(itemId, cancellationToken: cancellationToken);

    if (item is null)
    {
      return Results.NotFound();
    }

    var itemDto = new ItemDto
    {
      Id = itemId,
      Name = item.Name,
      Price = item.Price
    };
    return Results.Ok(itemDto);
  });

app.MapPost("/api/item", async (
  PostItemRequest request,
  [FromServices] IRepository<Item> itemRepository,
  [FromServices] IUnitOfWork unitOfWork,
  CancellationToken cancellationToken) =>
{
  var item = new Item(request.Name, request.Price);
  await itemRepository.Add(item, cancellationToken: cancellationToken);
  await unitOfWork.Commit();

  var response = new ItemDto
  {
    Id = item.Id,
    Name = item.Name,
    Price = item.Price
  };
  return Results.Ok(response);
});

app.MapGet("/api/order/{orderId:int}", async (
  int orderId,
  [FromServices] IRepository<Order> orderRepository,
  CancellationToken cancellationToken) =>
{
  var order = await orderRepository.GetById(orderId, cancellationToken: cancellationToken);

  if (order is null)
  {
    return Results.NotFound();
  }

  var contactDto = new ContactDto
  {
    Email = order.Contact.Email,
    Name = order.Contact.Name,
    PhoneNumber = order.Contact.PhoneNumber,
    Organization = order.Contact.Organization
  };
  var addressDto = new AddressDto
  {
    City = order.Address.City,
    State = order.Address.State,
    ZipCode = order.Address.ZipCode,
    Street = order.Address.Street
  };
  var orderDto = new OrderDto
  {
    Id = order.Id,
    Address = addressDto,
    Contact = contactDto,
    DeliveryAt = order.DeliveryAt,
    DeliveryMethod = order.DeliveryMethod,
    PaymentMethod = order.PaymentMethod,
    Instructions = order.Instructions
  };

  return Results.Ok(orderDto);
});

app.MapPost("/api/order", async (
  PostOrderRequest request,
  [FromServices] IRepository<Order> orderRepository,
  [FromServices] IRepository<Item> itemRepository,
  [FromServices] IUnitOfWork unitOfWork,
  CancellationToken cancellationToken) =>
{
  var contact = new Contact(
    request.Name,
    request.PhoneNumber,
    request.Email,
    request.Organization);

  var address = new Address(
    request.Street,
    request.City,
    request.ZipCode,
    request.State);

  var order = new Order(
    contact,
    address,
    request.PaymentMethod,
    request.DeliveryMethod,
    request.DeliveryAt,
    request.Instructions);

  var itemIds = request.OrderItems.Select(x => x.ItemId).ToArray();
  var items = await itemRepository
    .GetByIds(itemIds, cancellationToken: cancellationToken);

  request.OrderItems
    .OrderBy(x => x.ItemId)
    .Zip(items)
    .ToList()
    .ForEach(x => { x.Second.AddOrder(order, x.First.Quantity); });

  await orderRepository.Add(order, cancellationToken: cancellationToken);

  await unitOfWork.Commit();

  var contactDto = new ContactDto
  {
    Email = contact.Email,
    Name = contact.Name,
    PhoneNumber = contact.PhoneNumber,
    Organization = contact.Organization
  };
  var addressDto = new AddressDto
  {
    Street = address.Street,
    City = address.City,
    State = address.State,
    ZipCode = address.ZipCode
  };
  var response = new OrderDto
  {
    Id = order.Id,
    DeliveryAt = order.DeliveryAt,
    DeliveryMethod = order.DeliveryMethod,
    Instructions = order.Instructions,
    PaymentMethod = order.PaymentMethod,
    Contact = contactDto,
    Address = addressDto
  };
  return Results.Ok(response);
});
app.Run();