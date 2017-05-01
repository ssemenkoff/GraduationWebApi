using Core_Server.Models.Authentication;
using System;
using System.Linq;
using Core_Server.Models.Data;
using System.Collections.Generic;

namespace Core_Server.Helpers {
    public static class DbInitializer
    {
        private static User DefaultAdmin = new User {
            Login = "DefaultAdmin",
            Email = "Admin@admin.com",
            Password = "Admin1234",
            Role = "Admin"
        };

        public static void Initialize(UserContext userContext, DataContext dataContext)
        {
            Console.WriteLine("Filling Database");

            InitializeUsers(userContext);
            InitializeData(dataContext);
        }   

        private static void InitializeUsers(UserContext context) {
            Console.WriteLine("Filling Users");

            context.Database.EnsureCreated();

            if (context.Users.FirstOrDefault(x => x.Login == DefaultAdmin.Login) != null)
            {
                return;
            }

            context.Users.Add(DefaultAdmin);

            User sampleUser = new User {
                Login = "Username",
                Email = "Username@email.email",
                Password = "Password",
                Role = "User",
                PhoneNumber = "123123123"
            };
            
            if (context.Users.FirstOrDefault(x => x.Login == DefaultAdmin.Login) != null)
            {
                context.SaveChanges();
                return;
            }

            context.Users.Add(sampleUser);
            context.SaveChanges();
            return;

        }

        private static void InitializeData(DataContext context) {
            Console.WriteLine("Filling Data");

            context.Database.EnsureCreated();
            context.Categories.RemoveRange(context.Categories);
            context.SubCategories.RemoveRange(context.SubCategories);

            context.SaveChanges();

            Image CategoryImage = new Image {
                Id = Guid.NewGuid().ToString(),
                Path = "CategoryPath"
            };

            Image SubCategoryImage = new Image {
                Id = Guid.NewGuid().ToString(),
                Path = "SubCategoryPath"
            };

            context.Images.Add(CategoryImage);
            context.Images.Add(SubCategoryImage);

            Category InitialCategory = new Category {
                Id = Guid.NewGuid().ToString(),
                Name = "Initial category",
                Image = CategoryImage
            };

            context.Categories.Add(InitialCategory);

            SubCategory InitialSubcategory = new SubCategory {
                Id = Guid.NewGuid().ToString(),
                Name = "Initial sub category",
                Image = SubCategoryImage,
                Category = InitialCategory,
                Description = "Testing description"
            };

            context.SubCategories.Add(InitialSubcategory);

            var ProductImage1 = new Image {
                Id = Guid.NewGuid().ToString(),
                Path = "ProductImage1"
            };

            var ProductImage2 = new Image {
                Id = Guid.NewGuid().ToString(),
                Path = "ProductImage2"
            };

            var PreviewImage = new Image {
                Id = Guid.NewGuid().ToString(),
                Path = "PreviewImage"
            };

            context.Images.Add(ProductImage1);
            context.Images.Add(ProductImage2);
            context.Images.Add(PreviewImage);

            Product InitialProduct = new Product {
                Id = Guid.NewGuid().ToString(),
                Name = "Initial product",
                Category = InitialCategory,
                SubCategory = InitialSubcategory,
                PreviewImage = PreviewImage,
                Images = new List<Image>{ ProductImage1, ProductImage2},
                ShortDescription = "Some long description! asdhoiqwebfpojqndvoicqjdsjiohbawodihgweuchbnaklscnhob oiqhewgfuh bqn koxchguasvbdkjcldn  qoiucbxlnj oiechn",
                Description = text,
                Price = 321
            };

            context.Products.Add(InitialProduct);

            context.SaveChanges();
        }

        private const string text = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Corporis, iste, dolores. Eligendi sed, mollitia magnam. Perspiciatis nostrum et repellat quos dolor ullam pariatur consequuntur quasi minus, iste, nemo temporibus praesentium! Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sed quos, fugit labore, dolorem laudantium beatae consequuntur eligendi libero delectus repudiandae animi. Quis aspernatur consequuntur quasi quaerat. Perspiciatis ipsum ex praesentium.                     Lorem ipsum dolor sit amet, consectetur adipisicing elit. Dolore quaerat atque et id ullam placeat, perspiciatis quis nisi asperiores sapiente, numquam eaque eum repudiandae hic excepturi cum cupiditate, autem ducimus.";
    }
}