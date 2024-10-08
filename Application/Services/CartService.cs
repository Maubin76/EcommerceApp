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
    public Cart GetCart(string userId)
    {
        return _context.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.item).FirstOrDefault(c => c.userId == userId);
    }

    // Ajouter un Item à un Cart
    public void AddItemToCart(string userId, Item item)
    {
        var cart = _context.Carts.Include(c => c.cartItems).FirstOrDefault(c => c.userId == userId);

        if (cart != null)
        {
            var existingCartItem = cart.cartItems.FirstOrDefault(ci => ci.itemId == item.id);

            // Vérifier si l'item existe déjà dans le cart
            if (existingCartItem == null)
            {
                // Créer un nouveau CartItem si l'item n'existe pas déjà
                var cartItem = new CartItem
                (
                    cart.id,
                    cart,
                    item // On peut lier directement l'item ici, si cela est approprié
                );

                cart.cartItems.Add(cartItem);
                //_context.CartItems.Add(cartItem);
                _context.SaveChanges(); // Enregistre les modifications dans la base de données
            }
            else
            {
                cart.cartItems.Add(existingCartItem);
                _context.SaveChanges();
            }
        }else{
            var newCart= new Cart(Guid.NewGuid(),userId,new List<CartItem>());
            // Créer un nouveau CartItem si l'item n'existe pas déjà
            var cartItem = new CartItem
            (
                newCart.id,
                newCart,
                item // On peut lier directement l'item ici, si cela est approprié
            );
            newCart.cartItems.Add(cartItem);
            _context.Carts.Add(newCart);
            //_context.CartItems.Add(cartItem);
            _context.SaveChanges(); // Enregistre les modifications dans la base de données
            
        }
    }

    // Supprimer un Item d'un Cart
    public void RemoveItemFromCart(string userId, Item item)
    {
        var cart = _context.Carts.Include(c => c.cartItems).FirstOrDefault(c => c.userId == userId);
        
        if (cart != null)
        {
            // Trouver le CartItem associé à l'item
            var cartItemToRemove = cart.cartItems.FirstOrDefault(ci => ci.itemId == item.id);
            if (cartItemToRemove != null)
            {
                cart.cartItems.Remove(cartItemToRemove);
                _context.SaveChanges(); // Enregistre les modifications dans la base de données
            }
        }
    }

    // Obtenir le prix total du Cart
    public decimal GetTotalPrice(string userId)
    {
        var cart = GetCart(userId);
        return cart?.GetPrice() ?? 0;
    }
}

