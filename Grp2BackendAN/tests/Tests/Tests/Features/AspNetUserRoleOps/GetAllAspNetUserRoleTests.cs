
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserRoleOps
{
    public class GetAllAspNetUserRolesTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserRoleService>(setupFixture)
    {

        [Fact]
        public async Task GetAllAspNetUserRolesTest_ReturnsAllAspNetUserRoles_WithoutPagination()
        {
            // Arrange
            AspNetUserRole aspNetUserRole1 = null;
            AspNetUserRole aspNetUserRole2 = null;
            try
            {
                aspNetUserRole1 = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                aspNetUserRole2 = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
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
                await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole1);
                await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllAspNetUserRolesTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            AspNetUserRole aspNetUserRole1 = null;
            AspNetUserRole aspNetUserRole2 = null;
            try
            {
                aspNetUserRole1 = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                aspNetUserRole2 = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                                var filterValue = aspNetUserRole1.UserId;

                var getAllAspNetUserRoleReqDto = new GetAllAspNetUserRoleReqDto
                    {
                        UserId = new FilterExpression<string>
                        {
                            Filters = new List<FilterOption<string>> {
                                new FilterOption<string> { Value = filterValue, ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                        Options = new Pagination()
                    };

                // Act
                var result = await Service.GetAll(getAllAspNetUserRoleReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole1);
                await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetUserRolesTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            AspNetUserRole aspNetUserRole1 = null;
            AspNetUserRole aspNetUserRole2 = null;
            try
            {
                aspNetUserRole1 = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                aspNetUserRole2 = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = "invalidValue"; 

                var getAllAspNetUserRoleReqDto = new GetAllAspNetUserRoleReqDto
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
                var result = await Service.GetAll(getAllAspNetUserRoleReqDto);

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
                await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole1);
                await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetUserRolesTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            AspNetUserRole aspNetUserRole1 = null;
            AspNetUserRole aspNetUserRole2 = null;
            try
            {
                aspNetUserRole1 = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                aspNetUserRole2 = await AspNetUserRoleHelper.AddAspNetUserRole(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllAspNetUserRoleReqDto = new GetAllAspNetUserRoleReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllAspNetUserRoleReqDto);

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
                await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole1);
                await AspNetUserRoleHelper.CleanUp(applicationDbContext, aspNetUserRole2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


