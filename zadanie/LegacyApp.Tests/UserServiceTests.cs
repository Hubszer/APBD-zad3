namespace LegacyApp.Tests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_ReturnsFalseWhenFirstNameIsEmpty()
    {

        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser(
            null,
            "Kowalski",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        // Assert.Equal(false, result);
        Assert.False(result);
    }

    // AddUser_ReturnsFalseWhenMissingAtSignAndDotInEmail

    // AddUser_ReturnsFalseWhenYoungerThen21YearsOld

    // AddUser_ReturnsTrueWhenVeryImportantClient

    // AddUser_ReturnsTrueWhenImportantClient

    // AddUser_ReturnsTrueWhenNormalClient

    // AddUser_ReturnsFalseWhenNormalClientWithNoCreditLimit

    // AddUser_ThrowsExceptionWhenUserDoesNotExist

    // AddUser_ThrowsExceptionWhenUserNoCreditLimitExistsForUser

    [Fact]
    public void AddUser_ThrowsArgumentExceptionWhenClientDoesNotExist()
    {

        // Arrange
        var userService = new UserService();

        // Act
        Action action = () => userService.AddUser(
            "Jan",
            "Kowalski",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            100
        );

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void AddUser_ReturnsFalseWhenMissingAtSignAndDotInEmail()
    {
        var userService = new UserService();

        var result = userService.AddUser(
            "Jan",
            "Kowalski",
            "kowalskikowalskipl",
            DateTime.Parse("2000-01-01"),
            10);

        Assert.False(result);
    }

    [Fact]
    public void AddUser_ReturnsFalseWhenYoungerThen21YearsOld()
    {
        var userService = new UserService();

        var res = userService.AddUser(
            "Jan",
            "Kowalski",
            "kowalski@kowalski.pl",
            DateTime.Now.AddYears(-20),
            10
        );

        Assert.False(res);

    }

    [Fact]
    public void AddUser_ReturnsTrueWhenVeryImportantClient()
    {
        //Arrange
        var service = new UserService();

        //Act
        var result = service.AddUser("jan",
            "Malewski", 
            "kowalski@wp.pl",
            new DateTime(1980, 1, 1), 
            2);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void AddUser_ReturnsTrueWhenImportantClient()
    {

        //Arrange
        var service = new UserService();

        //Act
        var result = service.AddUser("John",
            "John", "johnjohn@gmail.pl",
            new DateTime(1980, 1, 1)
            , 3);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void AddUser_ReturnsTrueWhenNormalClient()
    {
        //Arrange
        var service = new UserService();

        //Act
        var result = service.AddUser("Jan",
            "Kwiatkowski",
            "kwiatkowski@wp.pl", 
            new DateTime(2002, 1, 1), 
            5);

        //Assert
        Assert.True(result);
    }

   [Fact]
    public void AddUser_ReturnsFalseWhenNormalClientWithNoCreditLimit()
    {
        //Arrange
        var service = new UserService();

        //Act
        var result = service.AddUser("Jan", 
            "Kowalski",
            "kowalski@wp.pl", 
            new DateTime(1980, 1, 1), 
            1);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_ThrowsExceptionWhenUserDoesNotExist()
    {
        //Arrange
        var service = new UserService();

        //Act and Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = service.AddUser("Jan",
                "Unknown", 
                "kowalski@wp.pl",
                new DateTime(1980, 1, 1),
                10);
        });
    }

    [Fact]
    public void AddUser_ThrowsExceptionWhenUserNoCreditLimitExistsForUser()
    {
        //Arrange
        var service = new UserService();

        //Act and Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = service.AddUser(
                "John", 
                "Kowalski", 
                "kokokojohn@wp.pl",
                new DateTime(2000, 1, 1), 
                6);
        });
    }
}
