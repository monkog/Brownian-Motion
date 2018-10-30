using System.Collections.Generic;
using System.Linq;

namespace BrownianMotion.Helpers
{
    public static class NumberCollectionExtensions
    {
        /// <summary>
        /// Calculates the cumulated sum of provided values.
        /// </summary>
        /// <param name="values">The collection of values.</param>
        /// <returns>The cumulated sum of the vector.</returns>
        public static double[] CumulatedSum(this IEnumerable<double> values)
        {
	        values = values.ToList();
            var result = new double[values.Count()];

            for (int i = 0; i < values.Count(); i++)
            {
                var sum = 0.0;
                for (int j = 0; j <= i; j++)
                    sum += values.ElementAt(j);

                result[i] = sum;
            }

            return result;
        }
    }
}