using BrownianMotion.Helpers;
using Xunit;

namespace BrownianMotionTests.Helpers
{
	public class NumberCollectionExtensionsTests
	{
		[Fact]
		public void CumulatedSum_Collection_SumArray()
		{
			var array = new[] {1.0, 2.0, 3.0, 4.0};
			var expected = new[] { 1.0, 3.0, 6.0, 10.0 };

			var sum = array.CumulatedSum();

			Assert.Equal(expected, sum);
		}
	}
}