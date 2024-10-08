using Application.Models; // Ajuste l'espace de noms selon ton projet
using Application.Extensions;
using System.Collections.Generic;

public class CartService
{
    // Utilise un dictionnaire pour stocker les paniers par userId (string)
    private Dictionary<string, Cart> _carts;

    public CartService()
    {
        _carts = new Dictionary<string, Cart>();
    }

    // Récupère un panier à partir du userId ou crée un nouveau panier si l'utilisateur n'a pas encore de panier
    public Cart GetCartFromUserId(ISession session, string userId)
    {
        if (_carts.ContainsKey(userId))
        {
            return _carts[userId];
        }
        else
        {
            // Crée un nouveau panier pour cet utilisateur
            var newCart = new Cart(Guid.NewGuid(), userId, new List<Item>());

            // Ajoute le panier au dictionnaire
            _carts[userId] = newCart;

            return newCart;
        }
    }

    // Ajoute un article au panier d'un utilisateur spécifique
    public void AddItemToCart(string userId, Item item)
    {
        if (_carts.ContainsKey(userId))
        {
            _carts[userId].AddItem(item);
        }
        else
        {
            throw new KeyNotFoundException("L'utilisateur avec cet ID n'a pas de panier.");
        }
    }

    // Supprime un article du panier d'un utilisateur spécifique
    public void DeleteItemFromCart(string userId, Item item)
    {
        if (_carts.ContainsKey(userId))
        {
            _carts[userId].DeleteItem(item);
        }
        else
        {
            throw new KeyNotFoundException("L'utilisateur avec cet ID n'a pas de panier.");
        }
    }

    // Sauvegarde le panier d'un utilisateur dans la session
    public void SaveCartToSession(ISession session, string userId)
    {
        if (_carts.ContainsKey(userId))
        {
            session.SetObject("Cart", _carts[userId]);
        }
        else
        {
            throw new KeyNotFoundException("L'utilisateur avec cet ID n'a pas de panier.");
        }
    }

    // Supprime le panier d'un utilisateur
    public void RemoveCart(string userId)
    {
        if (_carts.ContainsKey(userId))
        {
            _carts.Remove(userId);
        }
        else
        {
            throw new KeyNotFoundException("L'utilisateur avec cet ID n'a pas de panier.");
        }
    }

    // Obtient tous les paniers
    public Dictionary<string, Cart> GetAllCarts()
    {
        return _carts;
    }
}
