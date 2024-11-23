using System;
using System.Collections.Generic;
using System.Linq;
using Application.Areas.Identity.Data;
using Application.Models;
using Microsoft.EntityFrameworkCore;

public class ReviewService
{
    private readonly ApplicationDbContext _context;

    public ReviewService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Add a review to the database by giving its attributes
    public void AddReviewToDatabase(int rating, string comment,int itemId, string userId){
        //Create the review
        Review newReview=new Review(itemId,userId,rating,comment);
        // Add it to the context
        _context.Reviews.Add(newReview);
        //Save the changes
        _context.SaveChanges();
    }

    // Get all the Reviews of one item using its id
    public List<Review> GetAllItemReviews(int itemId){
        List<Review> reviews = _context.Reviews.Where(i => i.itemId == itemId).ToList();
        return reviews;
    }
}

