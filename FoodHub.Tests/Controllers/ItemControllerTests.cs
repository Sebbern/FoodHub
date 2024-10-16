using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FoodHub.Controllers;
using FoodHub.DAL;
using FoodHub.Models;
using FoodHub.ViewModels;

namespace FoodHub.Test.Controllers;

public class ItemControllerTests {
    [Fact]
    public async Task TestTable() {
        var itemList = new List<Item> {
            new Item {
                ItemId = 1,
                Name = "Item 1",
                Description = "Description 1",
                Price = 20,
                ImageUrl = "https://via.placeholder.com/150",
            },
            new Item {
                ItemId = 2,
                Name = "Item 2",
                Description = "Description 2",
                Price = 30,
                ImageUrl = "https://via.placeholder.com/150",
            }
        };
        var mockItemRepository = new Mock<IItemRepository>();
        mockItemRepository.Setup(repo => repo.GetAll()).ReturnsAsync(itemList);
        var mockLogger = new Mock<ILogger<ItemController>>();
        var itemController = new ItemController(mockItemRepository.Object, mockLogger.Object);

        // act
        var result = await itemController.Table();

        // assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var itemsViewModel = Assert.IsAssignableFrom<ItemsViewModel>(viewResult.ViewData.Model);
        Assert.Equal(2, itemsViewModel.Items.Count());
        Assert.Equal(itemList, itemsViewModel.Items);
    }

    [Fact]
    public async Task TestCreateNotOk() {
        // arrange
        var testItem = new Item {
            ItemId = 1,
            Price = 20,
            Description = "Description 1",
            ImageUrl = "https://via.placeholder.com/150",
        };
        var mockItemRepository = new Mock<IItemRepository>();
        mockItemRepository.Setup(repo => repo.Create(testItem)).ReturnsAsync(false);
        var mockLogger = new Mock<ILogger<ItemController>>();
        var itemController = new ItemController(mockItemRepository.Object, mockLogger.Object);

        // act
        var result = await itemController.Create(testItem);

        // assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var viewItem = Assert.IsAssignableFrom<Item>(viewResult.ViewData.Model);
        Assert.Equal(testItem, viewItem);
    }
}