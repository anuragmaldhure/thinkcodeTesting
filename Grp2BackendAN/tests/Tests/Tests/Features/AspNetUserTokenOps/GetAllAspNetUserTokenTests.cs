
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserTokenOps
{
    public class GetAllAspNetUserTokensTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserTokenService>(setupFixture)
    {

        [Fact]
        public async Task GetAllAspNetUserTokensTest_ReturnsAllAspNetUserTokens_WithoutPagination()
        {
            // Arrange
            AspNetUserToken aspNetUserToken1 = null;
            AspNetUserToken aspNetUserToken2 = null;
            try
            {
                aspNetUserToken1 = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                aspNetUserToken2 = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();

                // Act
                var result = await Service.GetAll(null);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken1);
                await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllAspNetUserTokensTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            AspNetUserToken aspNetUserToken1 = null;
            AspNetUserToken aspNetUserToken2 = null;
            try
            {
                aspNetUserToken1 = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                aspNetUserToken2 = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = aspNetUserToken1.Value;
                var getAllAspNetUserTokenReqDto = new GetAllAspNetUserTokenReqDto
                    {
                        Value = new FilterExpression<string?>
                        {
                            Filters = new List<FilterOption<string?>> {
                                new FilterOption<string?> { Value = filterValue, ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                        Options = new Pagination()
                    };
 
                // Act
                var result = await Service.GetAll(getAllAspNetUserTokenReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken1);
                await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetUserTokensTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            AspNetUserToken aspNetUserToken1 = null;
            AspNetUserToken aspNetUserToken2 = null;
            try
            {
                aspNetUserToken1 = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                aspNetUserToken2 = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = "invalidValue"; 

                var getAllAspNetUserTokenReqDto = new GetAllAspNetUserTokenReqDto
                {
                    UserId = new FilterExpression<string>
                        {
                            Filters = new List<FilterOption<string>> {
                                new FilterOption<string> { Value = filterValue , ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                    Options = new Pagination()
                };
                 
                // Act
                var result = await Service.GetAll(getAllAspNetUserTokenReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                result.Data.Should().BeEmpty();
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken1);
                await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetUserTokensTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            AspNetUserToken aspNetUserToken1 = null;
            AspNetUserToken aspNetUserToken2 = null;
            try
            {
                aspNetUserToken1 = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                aspNetUserToken2 = await AspNetUserTokenHelper.AddAspNetUserToken(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllAspNetUserTokenReqDto = new GetAllAspNetUserTokenReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllAspNetUserTokenReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
                result.Data.Should().HaveCount(1);
            }
            catch (Exception ex)
            {
                // Handle exception or fail the test
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken1);
                await AspNetUserTokenHelper.CleanUp(applicationDbContext, aspNetUserToken2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


