using System;
using System.Collections.Generic;
using System.Linq;
using Application.Areas.Identity.Data;
using Application.Models;
using Microsoft.EntityFrameworkCore;

public class CartService
{
    private readonly ApplicationDbContext _context;

    public CartService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Récupérer un Cart avec ses CartItems et Items associés
    public Cart GetActiveCart(string userId)
    {
        return _context.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.item).FirstOrDefault(c => c.userId == userId && c.isActive);
    }

    public List<Cart> GetUnactiveCart(string userId)
    {
        return _context.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.item).Where(c => c.userId == userId && !c.isActive).ToList();
    }

    // Ajouter un Item à un Cart
    public void AddItemToCart(string userId, Item item)
    {
        var cart = _context.Carts.Include(c => c.cartItems).FirstOrDefault(c => c.userId == userId && c.isActive);

        if (cart != null)
        {
            var existingCartItem = cart.cartItems.FirstOrDefault(ci => ci.itemId == item.id);

            // Vérifier si l'item existe déjà dans le cart
            if (existingCartItem == null)
            {
                // Créer un nouveau CartItem si l'item n'existe pas déjà
                var cartItem = new CartItem
                (
                    cart,
                    item,
                    1
                );

                cart.cartItems.Add(cartItem);
                _context.SaveChanges(); // Enregistre les modifications dans la base de données
            }
            else
            {   
                existingCartItem.addQuantity();
                cart.cartItems.Add(existingCartItem);
                _context.SaveChanges();
            }
        }else{
            var newCart= new Cart(Guid.NewGuid(),userId,new List<CartItem>());
            _context.Carts.Add(newCart);
            _context.SaveChanges(); 
            // Créer un nouveau CartItem si l'item n'existe pas déjà
            var cartItem = new CartItem
            (
                newCart,
                item,
                1
            );
            newCart.cartItems.Add(cartItem);
            _context.SaveChanges(); // Enregistre les modifications dans la base de données
            
        }
    }

    // Supprimer un Item d'un Cart
    public void RemoveItemFromCart(string userId, Item item)
    {
        var cart = _context.Carts.Include(c => c.cartItems).FirstOrDefault(c => c.userId == userId && c.isActive);
        
        if (cart != null)
        {
            // Trouver le CartItem associé à l'item
            var cartItemToRemove = cart.cartItems.FirstOrDefault(ci => ci.itemId == item.id);
            if (cartItemToRemove != null)
            {
                if (cartItemToRemove.quantity==1){
                    cart.cartItems.Remove(cartItemToRemove);
                    _context.SaveChanges(); // Enregistre les modifications dans la base de données
                }else{
                    cartItemToRemove.reduceQuantity();
                    _context.SaveChanges(); 
                }
            }
        }
    }

    public void BuyCart(string userId){
        var cart=this.GetActiveCart(userId);
        cart.isActive=false;
        _context.SaveChanges(); 
    }
    // Obtenir le prix total du Cart
    public decimal GetTotalPrice(string userId)
    {
        var cart = GetActiveCart(userId);
        return cart?.GetPrice() ?? 0;
    }
}

