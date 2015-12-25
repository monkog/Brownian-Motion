namespace Brownian_Motion.ViewModel
{
    public static class MathFunctions
    {
        #region Private Methods


        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Calculates the cummulated sum.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The cummulated sum of the vector.</returns>
        public static double[] CummulatedSum(double[] vector)
        {
            var result = new double[vector.Length];

            for (int i = 0; i < vector.Length; i++)
            {
                var sum = 0.0;
                for (int j = 0; j <= i; j++)
                    sum += vector[j];

                result[i] = sum;
            }

            return result;
        }

        #endregion Public Methods
    }
}

