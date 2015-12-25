﻿using System;

namespace Brownian_Motion.ViewModel
{
    public class NormalRandom
    {
        #region Private Members

        private static uint _w;
        private static uint _z;

        #endregion Private Members

        #region Constructor

        static NormalRandom()
        {
            _w = 421378526;
            _z = 346436668;
        }

        #endregion Constructor

        #region Private Methods

        // Get a random sample from thae range (0, 1).
        public static double GetUniform()
        {
            uint u = GetUint();
            return (u + 1.0) * 2.328306435454494e-10;
        }

        // This is the heart of the generator.
        // It uses George Marsaglia's MWC algorithm to produce an unsigned integer.
        // See http://www.bobwheeler.com/statistics/Password/MarsagliaPost.txt
        private static uint GetUint()
        {
            _z = 36969 * (_z & 65535) + (_z >> 16);
            _w = 18000 * (_w & 65535) + (_w >> 16);
            return (_z << 16) + _w;
        }

        #endregion Private Methods

        #region Public Methods

        // Get normal (Gaussian) random sample with mean 0 and standard deviation 1
        public static double GetNormal()
        {
            // Use Box-Muller algorithm
            double u1 = GetUniform();
            double u2 = GetUniform();
            double r = Math.Sqrt(-2.0 * Math.Log(u1));
            double theta = 2.0 * Math.PI * u2;
            return r * Math.Sin(theta);
        }

        #endregion Public Methods
    }
}

