//using EcommerceUserPanel.Controllers;
//using EcommerceUserPanel.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Text;


//namespace UnitTestProject
//{
//   public  class ProductCategoryTestController
//    {
//        private ShoppingDemoooo2Context context;

//        public static DbContextOptions<ShoppingDemoooo2Context>
//        dbContextOptions
//        { get; set; }

//        public static string connectionString =
//          "Data Source=TRD-517;Initial Catalog=ShoppingApisseDb5;Integrated Security=true;";
//        static ProductCategoryTestController()
//        {
//            dbContextOptions = new DbContextOptionsBuilder<ShoppingDemoooo2Context>()
//                .UseSqlServer(connectionString).Options;

//        }
//        [TestMethod]
//        public void Display()
//        {
//            Assert.ThrowsException<NullReferenceException>(() =>
//            {
//                var controller = new ProductCategoryController(context);
//                var productcategoryId = 9;
//                var Data = controller.Display(productcategoryId);
//                Assert.IsType<OkObjectResult>(Data);
//            });

//        }
//        //[TestMethod]
//        //public async void Task_GetProductCategoryByID_Return_NotFoundResult()
//        //{
//        //    var controller = new ProductCategoryController(context);
//        //    var productcategoryId = 6;
//        //    var Data = await controller.Display(productcategoryId);
//        //    Assert.IsType<NotFoundResult>(Data);
//        //}
//        //[TestMethod]
//        //public async void Task_GetProductCategoryByID_MatchResult()
//        //{
//        //    var controller = new ProductCategoryController(context);
//        //    int id = 1;
//        //    var data = await controller.Display(id);
//        //    Assert.IsType<OkObjectResult>(data);
//        //    var OkResult = data.Should().BeOfType<OkObjectResult>().Subject;
//        //    var pro = OkResult.Value.Should().BeAssignableTo<ProductCategory>().Subject;
//        //    Assert.Equals("Summer Collection", pro.CategoryName);
//        //    Assert.Equals("Cotton Collection", pro.CategoryDescription);

//        //}
//    }
//}
