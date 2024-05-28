using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using App.DTO.v1_0.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace App.Test.Integration;

[Collection("NonParallel")]
public class HomeControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{/*
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    private readonly JsonSerializerOptions _camelCaseJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    
    public HomeControllerTest(CustomWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact(DisplayName = "POST - register new user")]
    public async Task RegisterNewUserTest()
    {
        // Arrange
        const string url = "/api/v1/identity/account/register?expiresInSeconds=1";
        const string email = "register@test.ee";
        const string password = "Foo123Bar!";

        var register = new RegisterInfo()
        {
            Email = email,
            Password = password,
        };
        var data = JsonContent.Create(register);

        // Act
        var response = await _client.PostAsync(url, data);

        // Assert
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent, email, DateTime.Now.AddSeconds(2).ToUniversalTime());
    }
    
    private void VerifyJwtContent(string jwt, string email,
        DateTime validToIsSmallerThan)
    {
        var JWTResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);

        Assert.NotNull(JWTResponse);
        Assert.NotNull(JWTResponse.RefreshToken);
        Assert.NotNull(JWTResponse.Jwt);

        // verify the actual JWT
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(JWTResponse.Jwt);
        Assert.Equal(email, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);
        Assert.True(jwtToken.ValidTo < validToIsSmallerThan);
    }

    private async Task<string> RegisterNewUser(string email, string password,
        int expiresInSeconds = 1)
    {
        var url = $"/api/v1/identity/account/register?expiresInSeconds={expiresInSeconds}";

        var register = new RegisterInfo()
        {
            Email = email,
            Password = password,
        };

        var data = JsonContent.Create(register);
        // Act
        var response = await _client.PostAsync(url, data);

        var responseContent = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.True(response.IsSuccessStatusCode);

        VerifyJwtContent(responseContent, email,
            DateTime.Now.AddSeconds(expiresInSeconds + 1).ToUniversalTime());

        return responseContent;
    }
    
    [Fact(DisplayName = "POST - login user")]
    public async Task LoginUserTest()
    {
        const string email = "login@test.ee";
        const string password = "Foo123Bar!";
        const int expiresInSeconds = 10;

        // Arrange
        await RegisterNewUser(email, password, expiresInSeconds);


        var url = "/api/v1/identity/Account/login?expiresInSeconds=10";

        var loginInfo = new LoginInfo()
        {
            Email = email,
            Password = password,
        };

        var data = JsonContent.Create(loginInfo);

        // Act
        var response = await _client.PostAsync(url, data);

        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent, email,
            DateTime.Now.AddSeconds(expiresInSeconds + 1).ToUniversalTime());
    }
    
    [Fact(DisplayName = "POST - log the user out")]
    public async Task LogoutUserTest()
    {
        const string email = "logout@test.ee";
        const string password = "Foo123Bar!";
        const int expiresInSeconds = 10;

        // Arrange
        await RegisterNewUser(email, password, expiresInSeconds);


        var url = "/api/v1/identity/Account/login?expiresInSeconds=10";

        var loginInfo = new LoginInfo()
        {
            Email = email,
            Password = password,
        };

        var data = JsonContent.Create(loginInfo);

        // Act
        var response = await _client.PostAsync(url, data);

        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.True(response.IsSuccessStatusCode);

        var jwtData = JsonSerializer.Deserialize<JWTResponse>(responseContent, _camelCaseJsonSerializerOptions)!;

        url = "/api/v1/identity/Account/logout";

        var logout = new LogoutInfo()
        {
            RefreshToken = jwtData.RefreshToken
        };

        data = JsonContent.Create(logout);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtData.Jwt);

        // Act
        response = await _client.PostAsync(url, data);
        responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "POST - JWT expired")]
    public async Task JwtExpired()
    {
        const string email = "jwt@expire.ee";
        const string password = "Foo123Bar!";
        const int expiresInSeconds = 3;

        const string url = "/api/v1/Vehicle/GetVehicles";

        // Arrange
        var jwt = await RegisterNewUser(email, password, expiresInSeconds);
        var JWTResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);
        _testOutputHelper.WriteLine(JWTResponse!.Jwt);


        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWTResponse.Jwt);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.True(response.IsSuccessStatusCode);

        // Arrange
        await Task.Delay((expiresInSeconds + 2) * 1000);

        var request2 = new HttpRequestMessage(HttpMethod.Get, url);
        request2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWTResponse.Jwt);

        // Act
        var response2 = await _client.SendAsync(request2);

        // Assert
        Assert.False(response2.IsSuccessStatusCode);
    }
    
    [Fact(DisplayName = "POST - JWT renewal")]
    public async Task JwtRenewal()
    {
        const string email = "jwt@renewal.ee";
        const string password = "Foo.bar1";
        const int expiresInSeconds = 3;

        const string url = "/api/v1/Vehicle/GetVehicles";

        // Arrange
        var jwt = await RegisterNewUser(email, password, expiresInSeconds);
        var JWTResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);
        
        // let the jwt expire
        await Task.Delay((expiresInSeconds + 2) * 1000);

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWTResponse!.Jwt);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.False(response.IsSuccessStatusCode);

        // Arrange
        var refreshUrl = $"/api/v1/identity/account/refreshtoken?expiresInSeconds={expiresInSeconds}";
        var refreshData = new
        {
            JWT = JWTResponse.Jwt, JWTResponse.RefreshToken,
        };

        var data =  JsonContent.Create(refreshData);
        
        var response2 = await _client.PostAsync(refreshUrl, data);
        var responseContent2 = await response2.Content.ReadAsStringAsync();
        
        Assert.True(response2.IsSuccessStatusCode);
        
        JWTResponse = JsonSerializer.Deserialize<JWTResponse>(responseContent2, _camelCaseJsonSerializerOptions);

        var request3 = new HttpRequestMessage(HttpMethod.Get, url);
        request3.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWTResponse!.Jwt);

        // Act
        var response3 = await _client.SendAsync(request3);
        _testOutputHelper.WriteLine(response3.ToString());
        // Assert
        Assert.True(response3.IsSuccessStatusCode);
    }

*/
}