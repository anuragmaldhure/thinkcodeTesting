
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserOps
{
    public class GetAllAspNetUsersTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserService>(setupFixture)
    {

        [Fact]
        public async Task GetAllAspNetUsersTest_ReturnsAllAspNetUsers_WithoutPagination()
        {
            // Arrange
            AspNetUser aspNetUser1 = null;
            AspNetUser aspNetUser2 = null;
            try
            {
                aspNetUser1 = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                aspNetUser2 = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
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
                await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser1);
                await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllAspNetUsersTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            AspNetUser aspNetUser1 = null;
            AspNetUser aspNetUser2 = null;
            try
            {
                aspNetUser1 = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                aspNetUser2 = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = aspNetUser1.FirstName;
                var getAllAspNetUserReqDto = new GetAllAspNetUserReqDto
                    {
                        FirstName = new FilterExpression<string>
                        {
                            Filters = new List<FilterOption<string>> {
                                new FilterOption<string> { Value = filterValue, ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                        Options = new Pagination()
                    };
 
                // Act
                var result = await Service.GetAll(getAllAspNetUserReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser1);
                await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetUsersTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            AspNetUser aspNetUser1 = null;
            AspNetUser aspNetUser2 = null;
            try
            {
                aspNetUser1 = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                aspNetUser2 = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = "invalidValue"; 

                var getAllAspNetUserReqDto = new GetAllAspNetUserReqDto
                {
                    Id = new FilterExpression<string>
                        {
                            Filters = new List<FilterOption<string>> {
                                new FilterOption<string> { Value = filterValue , ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                    Options = new Pagination()
                };
                 
                // Act
                var result = await Service.GetAll(getAllAspNetUserReqDto);

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
                await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser1);
                await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetUsersTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            AspNetUser aspNetUser1 = null;
            AspNetUser aspNetUser2 = null;
            try
            {
                aspNetUser1 = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                aspNetUser2 = await AspNetUserHelper.AddAspNetUser(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllAspNetUserReqDto = new GetAllAspNetUserReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllAspNetUserReqDto);

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
                await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser1);
                await AspNetUserHelper.CleanUp(applicationDbContext, aspNetUser2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


