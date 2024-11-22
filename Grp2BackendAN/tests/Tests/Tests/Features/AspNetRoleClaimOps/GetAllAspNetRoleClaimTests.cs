
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetRoleClaimOps
{
    public class GetAllAspNetRoleClaimsTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetRoleClaimService>(setupFixture)
    {

        [Fact]
        public async Task GetAllAspNetRoleClaimsTest_ReturnsAllAspNetRoleClaims_WithoutPagination()
        {
            // Arrange
            AspNetRoleClaim aspNetRoleClaim1 = null;
            AspNetRoleClaim aspNetRoleClaim2 = null;
            try
            {
                aspNetRoleClaim1 = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                aspNetRoleClaim2 = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
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
                await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim1);
                await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllAspNetRoleClaimsTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            AspNetRoleClaim aspNetRoleClaim1 = null;
            AspNetRoleClaim aspNetRoleClaim2 = null;
            try
            {
                aspNetRoleClaim1 = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                aspNetRoleClaim2 = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = aspNetRoleClaim1.RoleId;
                var getAllAspNetRoleClaimReqDto = new GetAllAspNetRoleClaimReqDto
                    {
                        RoleId = new FilterExpression<string>
                        {
                            Filters = new List<FilterOption<string>> {
                                new FilterOption<string> { Value = filterValue, ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                        Options = new Pagination()
                    };
 
                // Act
                var result = await Service.GetAll(getAllAspNetRoleClaimReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim1);
                await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetRoleClaimsTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            AspNetRoleClaim aspNetRoleClaim1 = null;
            AspNetRoleClaim aspNetRoleClaim2 = null;
            try
            {
                aspNetRoleClaim1 = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                aspNetRoleClaim2 = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = aspNetRoleClaim1.Id + 10; 

                var getAllAspNetRoleClaimReqDto = new GetAllAspNetRoleClaimReqDto
                {
                    Id = new FilterExpression<int>
                        {
                            Filters = new List<FilterOption<int>> {
                                new FilterOption<int> { Value = filterValue , ComparisonOperator = ComparisonOperator.Equals }
                            }
                        },
                    Options = new Pagination()
                };
                 
                // Act
                var result = await Service.GetAll(getAllAspNetRoleClaimReqDto);

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
                await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim1);
                await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetRoleClaimsTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            AspNetRoleClaim aspNetRoleClaim1 = null;
            AspNetRoleClaim aspNetRoleClaim2 = null;
            try
            {
                aspNetRoleClaim1 = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                aspNetRoleClaim2 = await AspNetRoleClaimHelper.AddAspNetRoleClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllAspNetRoleClaimReqDto = new GetAllAspNetRoleClaimReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllAspNetRoleClaimReqDto);

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
                await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim1);
                await AspNetRoleClaimHelper.CleanUp(applicationDbContext, aspNetRoleClaim2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


