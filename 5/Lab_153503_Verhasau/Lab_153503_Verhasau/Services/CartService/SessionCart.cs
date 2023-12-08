using Domain.Models;
using Lab_153503_Verhasau.Extensions;
using System.Text.Json.Serialization;
using Domain.Entities;

namespace Lab_153503_Verhasau.Services.CartService;

public class SessionCart : Cart
{
    public static Cart GetCart(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
        SessionCart cart = session?.Get<SessionCart>("Cart") ?? new SessionCart();
        cart.Session = session;
        return cart;
    }

    [JsonIgnore]
    public ISession? Session { get; set; }
    public override void Add(Souvenir product)
    {
        base.Add(product);
        Session?.Set("Cart", this);
    }

    public override void Remove(int id)
    {
        base.Remove(id);
        Session?.Set("Cart", this);
    }

    public override void Clear()
    {
        base.Clear();
        Session?.Remove("Cart");
    }
}