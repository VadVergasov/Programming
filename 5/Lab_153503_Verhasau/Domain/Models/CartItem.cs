using Domain.Entities;

namespace Domain.Models;

public class CartItem
{
    public Souvenir Souvenir { get; set; }
    public int Quantity { get; set; }
}
