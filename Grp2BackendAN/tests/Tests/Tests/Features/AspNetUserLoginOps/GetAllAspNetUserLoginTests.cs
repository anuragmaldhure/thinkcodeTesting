
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserLoginOps
{
    public class GetAllAspNetUserLoginsTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserLoginService>(setupFixture)
    {

        [Fact]
        public async Task GetAllAspNetUserLoginsTest_ReturnsAllAspNetUserLogins_WithoutPagination()
        {
            // Arrange
            AspNetUserLogin aspNetUserLogin1 = null;
            AspNetUserLogin aspNetUserLogin2 = null;
            try
            {
                aspNetUserLogin1 = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                aspNetUserLogin2 = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
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
                await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin1);
                await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllAspNetUserLoginsTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            AspNetUserLogin aspNetUserLogin1 = null;
            AspNetUserLogin aspNetUserLogin2 = null;
            try
            {
                aspNetUserLogin1 = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                aspNetUserLogin2 = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = aspNetUserLogin1.ProviderDisplayName;
                var getAllAspNetUserLoginReqDto = new GetAllAspNetUserLoginReqDto
                    {
                        ProviderDisplayName = new FilterExpression<string?>
                        {
                            Filters = new List<FilterOption<string?>> {
                                new FilterOption<string?> { Value = filterValue, ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                        Options = new Pagination()
                    };
 
                // Act
                var result = await Service.GetAll(getAllAspNetUserLoginReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin1);
                await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetUserLoginsTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            AspNetUserLogin aspNetUserLogin1 = null;
            AspNetUserLogin aspNetUserLogin2 = null;
            try
            {
                aspNetUserLogin1 = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                aspNetUserLogin2 = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = "invalidValue"; 

                var getAllAspNetUserLoginReqDto = new GetAllAspNetUserLoginReqDto
                {
                    LoginProvider = new FilterExpression<string>
                        {
                            Filters = new List<FilterOption<string>> {
                                new FilterOption<string> { Value = filterValue , ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                    Options = new Pagination()
                };
                 
                // Act
                var result = await Service.GetAll(getAllAspNetUserLoginReqDto);

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
                await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin1);
                await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetUserLoginsTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            AspNetUserLogin aspNetUserLogin1 = null;
            AspNetUserLogin aspNetUserLogin2 = null;
            try
            {
                aspNetUserLogin1 = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                aspNetUserLogin2 = await AspNetUserLoginHelper.AddAspNetUserLogin(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllAspNetUserLoginReqDto = new GetAllAspNetUserLoginReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllAspNetUserLoginReqDto);

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
                await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin1);
                await AspNetUserLoginHelper.CleanUp(applicationDbContext, aspNetUserLogin2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


