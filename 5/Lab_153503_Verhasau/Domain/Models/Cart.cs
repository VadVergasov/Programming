using Domain.Entities;

namespace Domain.Models;
public class Cart
{
    public Dictionary<int, CartItem> CartItems { get; set; } = new();

    public virtual void Add(Souvenir souvenir)
    {
        if (!CartItems.ContainsKey(souvenir.Id))
        {
            CartItems.Add(souvenir.Id, new CartItem() { Souvenir = souvenir, Quantity = 1 });
        }
        else
        {
            ++CartItems[souvenir.Id].Quantity;
        }
    }

    public virtual void Remove(int id)
    {
        if (--CartItems[id].Quantity <= 0)
        {
            CartItems.Remove(id);
        }
    }

    public virtual void Clear()
    {
        CartItems.Clear();
    }

    public int Quantity => CartItems.Sum(item => item.Value.Quantity);

    public decimal TotalPrice => CartItems.Sum(item => item.Value.Souvenir.Price * item.Value.Quantity);

}