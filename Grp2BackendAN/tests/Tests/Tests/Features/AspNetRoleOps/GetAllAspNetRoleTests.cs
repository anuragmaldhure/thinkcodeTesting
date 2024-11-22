
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetRoleOps
{
    public class GetAllAspNetRolesTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetRoleService>(setupFixture)
    {

        [Fact]
        public async Task GetAllAspNetRolesTest_ReturnsAllAspNetRoles_WithoutPagination()
        {
            // Arrange
            AspNetRole aspNetRole1 = null;
            AspNetRole aspNetRole2 = null;
            try
            {
                aspNetRole1 = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                aspNetRole2 = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
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
                await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole1);
                await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllAspNetRolesTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            AspNetRole aspNetRole1 = null;
            AspNetRole aspNetRole2 = null;
            try
            {
                aspNetRole1 = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                aspNetRole2 = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = aspNetRole1.Name;
                var getAllAspNetRoleReqDto = new GetAllAspNetRoleReqDto
                    {
                        Name = new FilterExpression<string?>
                        {
                            Filters = new List<FilterOption<string?>> {
                                new FilterOption<string?> { Value = filterValue, ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                        Options = new Pagination()
                    };
 
                // Act
                var result = await Service.GetAll(getAllAspNetRoleReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole1);
                await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetRolesTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            AspNetRole aspNetRole1 = null;
            AspNetRole aspNetRole2 = null;
            try
            {
                aspNetRole1 = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                aspNetRole2 = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = "invalidValue"; 

                var getAllAspNetRoleReqDto = new GetAllAspNetRoleReqDto
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
                var result = await Service.GetAll(getAllAspNetRoleReqDto);

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
                await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole1);
                await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetRolesTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            AspNetRole aspNetRole1 = null;
            AspNetRole aspNetRole2 = null;
            try
            {
                aspNetRole1 = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                aspNetRole2 = await AspNetRoleHelper.AddAspNetRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllAspNetRoleReqDto = new GetAllAspNetRoleReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllAspNetRoleReqDto);

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
                await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole1);
                await AspNetRoleHelper.CleanUp(applicationDbContext, aspNetRole2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


