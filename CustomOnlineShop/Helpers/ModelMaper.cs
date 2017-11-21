using ApplicationCore.Models;
using CustomOnlineShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.Helpers
{
    public static class ModelMaper
    {

        public static CatalogItemViewModel MapToViewModel(this Product product)
        {
            CatalogItemViewModel vm = new CatalogItemViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                ImagePath = product.ImgPath,
                AverageRating = (product.ProductReviews != null && product.ProductReviews.Count > 0) ? product.ProductReviews.Average(x => x.Rating) : 0,
                ReviewsCount = product.ProductReviews != null ? product.ProductReviews.Count : 0
            };

            return vm;
        }


        public static CreateProductViewModel MapToCreateViewModel(this Product product)
        {
            CreateProductViewModel vm = new CreateProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price.ToString(),
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                CategoryId = product.CategoryId,
                ImgPath = product.ImgPath?.Remove(0, Constants.ProductImagesPath.Length),
            };

            return vm;
        }

        public static ProductDisplayViewModel MapToDisplayViewModel(this Product product)
        {
            ProductDisplayViewModel vm = new ProductDisplayViewModel
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName = product.Category.Category,
                Price = product.Price
            };

            return vm;
        }


        public static ShippingInfoViewModel MapToViewModel(this ShippingInfo info)
        {
            ShippingInfoViewModel vm = new ShippingInfoViewModel
            {
                Id = info.Id,
                Country = info.Country,
                City = info.City,
                Address = info.Address,
                ZipCode = info.ZipCode
            };

            return vm;
        }


        public static OrderEntryViewModel MapToViewModel(this OrderEntry orderEntry)
        {
            OrderEntryViewModel vm = new OrderEntryViewModel
            {
                ProductName = orderEntry.ProductName,
                Quantity = orderEntry.Quantity,
                UnitPrice = orderEntry.UnitPrice
            };

            return vm;
        }


        public static OrderViewModel MapToViewModel(this Order order)
        {
            OrderViewModel vm = new OrderViewModel
            {
                OrderId = order.Id,
                Date = order.Date,
                ShippingInfo = order.ShippingInfo?.MapToViewModel(),
                OrderEntries = order.OrderEntries?.Select(x => x.MapToViewModel()).ToList()
            };

            return vm;
        }


        public static ProductReviewViewModel MapToViewModel(this ProductReview review)
        {
            ProductReviewViewModel vm = new ProductReviewViewModel
            {
                Id = review.Id,
                ProductId = review.ProductID,
                Title = review.Title,
                Rating = review.Rating,
                Description = review.Description,
                AuthorUserName = review.AuthorUserName,
                PostDate = review.PostDate
            };

            return vm;
        }


        public static RoleViewModel MapToViewModel(this IdentityRole role)
        {
            RoleViewModel vm = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return vm;
        }


        public static ProductCategoryViewModel MapToViewModel(this ProductCategory category)
        {
            ProductCategoryViewModel vm = new ProductCategoryViewModel
            {
                Id = category.Id,
                Name = category.Category
            };

            return vm;
        }

    }
}
