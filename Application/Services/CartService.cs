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

    // Retrieve an active cart with its associated CartItems and Items
    public Cart GetActiveCart(string userId)
    {
        return _context.Carts
                       .Include(c => c.cartItems)
                       .ThenInclude(ci => ci.item)
                       .FirstOrDefault(c => c.userId == userId && c.isActive);
    }

    // Retrieve a list of inactive carts (order history) for a specific user
    public List<Cart> GetUnactiveCart(string userId)
    {
        return _context.Carts
                       .Include(c => c.cartItems)
                       .ThenInclude(ci => ci.item)
                       .Where(c => c.userId == userId && !c.isActive)
                       .ToList();
    }

    // Add an item to a cart
    public void AddItemToCart(string userId, Item item)
    {
        // Get the active cart for the user
        var cart = _context.Carts.Include(c => c.cartItems)
                                 .FirstOrDefault(c => c.userId == userId && c.isActive);

        if (cart != null)
        {
            // Check if the item already exists in the cart
            var existingCartItem = cart.cartItems.FirstOrDefault(ci => ci.itemId == item.id);

            // If the item does not exist in the cart, create a new CartItem
            if (existingCartItem == null)
            {
                var cartItem = new CartItem
                (
                    cart,
                    item,
                    1 // Initial quantity
                );

                cart.cartItems.Add(cartItem);
                _context.SaveChanges(); // Save the changes to the database
            }
            else
            {
                // If the item already exists, increase its quantity
                existingCartItem.addQuantity();
                cart.cartItems.Add(existingCartItem);
                _context.SaveChanges();
            }
        }
        else
        {
            // If no active cart exists, create a new one
            var newCart = new Cart(Guid.NewGuid(), userId, new List<CartItem>());
            _context.Carts.Add(newCart);
            _context.SaveChanges(); 

            // Add the new item to the newly created cart
            var cartItem = new CartItem
            (
                newCart,
                item,
                1 // Initial quantity
            );
            newCart.cartItems.Add(cartItem);
            _context.SaveChanges(); // Save the changes to the database
        }
    }

    // Remove an item from the cart
    public void RemoveItemFromCart(string userId, Item item)
    {
        // Retrieve the active cart for the user
        var cart = _context.Carts.Include(c => c.cartItems)
                                 .FirstOrDefault(c => c.userId == userId && c.isActive);
        
        if (cart != null)
        {
            // Find the CartItem associated with the item
            var cartItemToRemove = cart.cartItems.FirstOrDefault(ci => ci.itemId == item.id);
            if (cartItemToRemove != null)
            {
                if (cartItemToRemove.quantity == 1)
                {
                    // If quantity is 1, remove the item from the cart
                    cart.cartItems.Remove(cartItemToRemove);
                    _context.SaveChanges(); // Save changes to the database
                }
                else
                {
                    // If quantity is greater than 1, reduce the quantity
                    cartItemToRemove.reduceQuantity();
                    _context.SaveChanges();
                }
            }
        }
    }

    // Mark the cart as purchased by setting it as unactive
    public void BuyCart(string userId)
    {
        var cart = this.GetActiveCart(userId);
        cart.isActive = false;
        _context.SaveChanges(); // Save the changes to the database
    }

    // Get the total price of the cart
    public decimal GetTotalPrice(string userId)
    {
        var cart = GetActiveCart(userId);
        return cart?.GetPrice() ?? 0; // Return the total price, or 0 if the cart is null
    }
}


