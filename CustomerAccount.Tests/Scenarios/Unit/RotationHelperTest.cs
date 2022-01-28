using CustomerAccount.Domain.Helpers;
using FluentAssertions;
using Xunit;


namespace CustomerAccount.Tests.Scenarios.Unit
{
    public  class RotationHelperTest
    {
        [Fact]
        public  void Should_Rotate_Twice()
        {
            int timesToRotate = 2;
            int[] firtSituation = {3,4,5 };

            var expected =  "453";
            var rotation = RotationHelper.Rotate(firtSituation, timesToRotate);
            rotation.Should().Be(expected);
            
            int[] secondSituation = { 1, 2, 3 };
            
            expected = "231";
            rotation = RotationHelper.Rotate(secondSituation, timesToRotate);
            rotation.Should().Be(expected);

            int[] thirdSituation = { 8,2,3 };
                                   
            expected = "238";
            rotation = RotationHelper.Rotate(thirdSituation, timesToRotate);
            rotation.Should().Be(expected);
        }
    }
}
