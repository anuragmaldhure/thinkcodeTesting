
using thinkbridge.Grp2BackendAN.Tests.Helpers;
using thinkbridge.Grp2BackendAN.UnitTests;
using static thinkbridge.Grp2BackendAN.Core.Enums.PaginationEnums;
namespace thinkbridge.Grp2BackendAN.Tests.Tests.Features.AspNetUserClaimOps
{
    public class GetAllAspNetUserClaimsTests(SetupFixture setupFixture) : TestBaseCollection<IAspNetUserClaimService>(setupFixture)
    {

        [Fact]
        public async Task GetAllAspNetUserClaimsTest_ReturnsAllAspNetUserClaims_WithoutPagination()
        {
            // Arrange
            AspNetUserClaim aspNetUserClaim1 = null;
            AspNetUserClaim aspNetUserClaim2 = null;
            try
            {
                aspNetUserClaim1 = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                aspNetUserClaim2 = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
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
                await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim1);
                await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim2);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetAllAspNetUserClaimsTest_ReturnsFilteredResultSuccessfully_ForGivenValidFilterOptions()
        {
            // Arrange
            AspNetUserClaim aspNetUserClaim1 = null;
            AspNetUserClaim aspNetUserClaim2 = null;
            try
            {
                aspNetUserClaim1 = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                aspNetUserClaim2 = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = aspNetUserClaim1.UserId;
                var getAllAspNetUserClaimReqDto = new GetAllAspNetUserClaimReqDto
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
                var result = await Service.GetAll(getAllAspNetUserClaimReqDto);

                // Assert
                result.Status.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.False(true, $"An exception occurred: {ex.Message}");
            }
            finally
            {
                await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim1);
                await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetUserClaimsTest_ReturnsFailure_GivenInvalidFilterOptions()
        {
            // Arrange
            AspNetUserClaim aspNetUserClaim1 = null;
            AspNetUserClaim aspNetUserClaim2 = null;
            try
            {
                aspNetUserClaim1 = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                aspNetUserClaim2 = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var filterValue = aspNetUserClaim1.Id + 10; 

                var getAllAspNetUserClaimReqDto = new GetAllAspNetUserClaimReqDto
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
                var result = await Service.GetAll(getAllAspNetUserClaimReqDto);

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
                await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim1);
                await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim2);
                await applicationDbContext.SaveChangesAsync();
            }
        }


        [Fact]
        public async Task GetAllAspNetUserClaimsTest_ReturnsPagedResultSuccessfully_GivenValidPaginationOptions()
        {
            // Arrange
            AspNetUserClaim aspNetUserClaim1 = null;
            AspNetUserClaim aspNetUserClaim2 = null;
            try
            {
                aspNetUserClaim1 = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                aspNetUserClaim2 = await AspNetUserClaimHelper.AddAspNetUserClaim(applicationDbContext);
                await applicationDbContext.SaveChangesAsync();
                var getAllAspNetUserClaimReqDto = new GetAllAspNetUserClaimReqDto
                {
                    Options = new Pagination
                    {
                        PageNum = 1,
                        PageSize = 1 // Only two records should be returned on the first page
                    }
                };

                // Act
                var result = await Service.GetAll(getAllAspNetUserClaimReqDto);

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
                await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim1);
                await AspNetUserClaimHelper.CleanUp(applicationDbContext, aspNetUserClaim2);
                await applicationDbContext.SaveChangesAsync();
            }
        }
        
    }
}


