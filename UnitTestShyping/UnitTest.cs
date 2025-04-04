using EliasTestShyping.DTO;
using EliasTestShyping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestShyping
{
    public class UnitTest
    {
        private readonly EventCodeMapper _mapper = new();

        [Theory]
        [InlineData("loaded", "1", null, "2004")]
        [InlineData("loaded", "2", "HeavyObject", "2009")]
        [InlineData("delivered", "1", "DelayedDelivery", "3001")]
        [InlineData("delivered", "1", "Other", "7000")]
        [InlineData("delivered", "2", "Other", "7000")]
        [InlineData("delivered", "1", null, "2003")]
        [InlineData("delivered", "2", null, "3000")]
        [InlineData("notDelivered", "1", "NoAccess", "6003")]
        [InlineData("notDelivered", "2", "Other", "7000")]
        [InlineData("notDelivered", "1", null, "5006")]
        [InlineData("notDelivered", "2", null, "5006")]
        [InlineData("notLoaded", "1", "NoAccess", "6003")]
        [InlineData("notLoaded", "2", "Other", "7000")]
        [InlineData("notLoaded", "1", null, "5006")]
        [InlineData("notLoaded", "2", null, "5006")]
        [InlineData("notLoaded", "2", null, "5222")]  //in this case should be failed
        [InlineData("inProgress", "2", null, "2000")]
        public void GetEventCode_ReturnsExpectedResult(string statusCode, string sequenceNumber, string tag, string expectedCode)
        {
            var orderStatus = new OrderStatusDto
            {
                StatusCode = statusCode,
                SequenceNumber = sequenceNumber,
                Comments = tag != null
                ? new List<CommentDto> { new() { Name = (statusCode == Enumerations.StatusCode.loaded.ToString() || statusCode == Enumerations.StatusCode.notLoaded.ToString())
                ? Enumerations.TagType.loadedDataTag.ToString()
                : Enumerations.TagType.deliveredDataTag.ToString(), Comment = tag } }
                : new List<CommentDto>()
            };

            var result = _mapper.GetEventCode(orderStatus);
            Assert.Equal(expectedCode, result);
        }

        //I refactored the GetEventCode method using modern C# features like pattern matching and dictionary lookups,
        //fixed the bug where "delivered" with the "Other" tag returned the wrong event code,
        //and added new mappings for "HeavyObject" (2009) and "DelayedDelivery" (3001). 
        //Additionally, I expanded unit tests to ensure full coverage of all status and tag combinations.
    }
}
