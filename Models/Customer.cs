﻿namespace LiperFrontend.Models
{
    public class Customer
    {
        public int id { get; set; } = 0;
        public string name { get; set; }
        public string nameAR { get; set; }
        public string username { get; set; }
        public string password { get; set; } = "123";
        public string confirmPassword { get; set; } = "123";

        public string email { get; set; }
        public string phone { get; set; }
        public Gender gender { get; set; }
        public int genderId { get; set; }
        public City city { get; set; }
        public int cityId { get; set; }
        public IFormFile? files { get; set; }
        public bool isFavoriteStar { get; set; } = true;

    }
    public class Customers
    {
        public List<Customer> customers { get; set; }
        public responseMessage responseMessage { get; set; }
        public int totalCount { get; set; }
        public int totalPages { get; set; }
        public int currentPage { get; set; }
    }
    public class GetCustomer
    {
        public Customer customer { get; set; }
        public responseMessage responseMessage { get; set; }
    }
}
