using AutoMapper;
using DevShop.Api.Views;
using DevShop.Api.Models;

namespace DevShop.Api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Product mappings
            CreateMap<Product, ProductView>();
            CreateMap<CreateProductView, Product>();
            CreateMap<UpdateProductView, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Customer mappings
            CreateMap<Customer, CustomerView>();
            CreateMap<CreateCustomerView, Customer>();
            CreateMap<UpdateCustomerView, Customer>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Order mappings
            CreateMap<Order, OrderView>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            
            CreateMap<CreateOrderView, Order>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OrderStatus.Pending))
                .ForMember(dest => dest.OrderDateOnUtc, opt => opt.MapFrom(src => DateTime.UtcNow));
            
            CreateMap<UpdateOrderView, Order>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // OrderItem mappings
            CreateMap<OrderItem, OrderItemView>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
            
            CreateMap<CreateOrderItemView, OrderItem>()
                .ConstructUsing((src, context) => new OrderItem(
                    context.Items["OrderId"]?.ToString() ?? string.Empty,
                    src.ProductId,
                    src.Quantity,
                    src.UnitPrice));
            
            CreateMap<UpdateOrderItemView, OrderItem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
} 