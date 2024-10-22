using System;
using System.Collections.Generic;
using System.Linq;
using Application.Areas.Identity.Data;
using Application.Models;
using Microsoft.EntityFrameworkCore;

public class ItemService
{
    private readonly ApplicationDbContext _context;

    public ItemService(ApplicationDbContext context)
    {
        _context = context;
    }
   

    // Get all filtered items
    public async Task<List<Item>> GetFilteredItemsAsync(string category, decimal? minPrice, decimal? maxPrice)
    {
        // Get all items
        var query = _context.Items.AsQueryable();

        // Filter by category if specified
        if (!string.IsNullOrEmpty(category) && category != "all")
        {
            query = query.Where(i => i.category == category);
        }

        // Filter by min price if specified
        if (minPrice.HasValue)
        {
            query = query.Where(i => i.price >= minPrice.Value);
        }

        // Filter by max price if specified
        if (maxPrice.HasValue)
        {
            query = query.Where(i => i.price <= maxPrice.Value);
        }

        // Execute request and send the response as a list
        return await query.ToListAsync();
    }
}



