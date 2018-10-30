using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AForge.Math;
using BrownianMotion.Helpers;
using OxyPlot;

namespace BrownianMotion.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Members

        private double _d;

        private int _n;

        private int _n2;

        private double _h;

        private ICommand _brownianMovementsCommand;

        private ICommand _brownianFractionalMovementsCommand;

        private ObservableCollection<DataPoint> _brownianMovement2DPoints;

        private ObservableCollection<DataPoint> _brownianMovementPoints;

        private ObservableCollection<DataPoint> _brownianFractionalMovementPoints;

        #endregion Private Members

        #region Public Properties

        /// <summary>
        /// Gets or sets the Diffusion coefficient.
        /// </summary>
        public double D
        {
            get { return _d; }
            set
            {
                if (_d == value) return;
                _d = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Hurst parameter.
        /// </summary>
        public double H
        {
            get { return _h; }
            set
            {
                if (_h == value) return;
                _h = value;
                if (_h <= 0) _h = 0.1;
                if (_h >= 1) _h = 0.9;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the number of samples.
        /// </summary>
        public int N
        {
            get { return _n; }
            set
            {
                if (_n == value) return;
                _n = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the number of samples for fractional Brownian movement.
        /// </summary>
        public int N2
        {
            get { return _n2; }
            set
            {
                if (_n2 == value) return;
                _n2 = value;
                if (_n2 <= 0) _n2 = 1;
                if (_n2 >= 15) _n2 = 14;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the brownian movement points in 2D.
        /// </summary>
        public ObservableCollection<DataPoint> BrownianMovement2DPoints
        {
            get { return _brownianMovement2DPoints; }
            set
            {
                if (_brownianMovement2DPoints == value) return;
                _brownianMovement2DPoints = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the brownian movement points.
        /// </summary>
        public ObservableCollection<DataPoint> BrownianMovementPoints
        {
            get { return _brownianMovementPoints; }
            set
            {
                if (_brownianMovementPoints == value) return;
                _brownianMovementPoints = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the brownian fractional movement points.
        /// </summary>
        public ObservableCollection<DataPoint> BrownianFractionalMovementPoints
        {
            get { return _brownianFractionalMovementPoints; }
            set
            {
                if (_brownianFractionalMovementPoints == value) return;
                _brownianFractionalMovementPoints = value;
                OnPropertyChanged();
            }
        }

        public ICommand BrownianMovementsCommand { get { return _brownianMovementsCommand ?? (_brownianMovementsCommand = new DelegateCommand(BrownianMovements)); } }

        public ICommand BrownianFractionalMovementsCommand { get { return _brownianFractionalMovementsCommand ?? (_brownianFractionalMovementsCommand = new DelegateCommand(BrownianFractionalMovements)); } }

        #endregion Public Properties

        #region Constructors

        public MainViewModel()
        {
            D = 4.2192e-013;
            N = 1000;
            N2 = 13;
            H = 0.9;
            BrownianMovements();
            BrownianFractionalMovements();
        }

        #endregion Constructors

        #region Private Methods

        private void BrownianFractionalMovements(object param = null)
        {
            BrownianFractionalMovementPoints = new ObservableCollection<DataPoint>();
            var n = (int)Math.Pow(2, Math.Min(13, N2));
            var r = new double[n + 1];
            var rx = new Complex[2 * n];
            r[0] = 1;
            rx[0].Re = 1;

            for (int i = 1; i <= n; i++)
            {
                r[i] = 0.5 * (Math.Pow(i + 1, 2 * H) - 2 * Math.Pow(i, 2 * H) + Math.Pow(i - 1, 2 * H));
                rx[i].Re = r[i];
            }

            var index = r.Length;
            for (int i = r.Length - 2; i >= 1; i--)
                rx[index++].Re = r[i];

            FourierTransform.FFT(rx, FourierTransform.Direction.Forward);

            // Eigenvalues
            var lambda = new double[rx.Length];
            for (int i = 0; i < rx.Length; i++)
                lambda[i] = rx[i].Re / (2 * n);

            var ww = new Complex[2 * n];

            for (int i = 0; i < lambda.Length; i++)
                ww[i] = Math.Sqrt(lambda[i]) * new Complex(NormalRandom.GetNormal(), NormalRandom.GetNormal());
            FourierTransform.FFT(ww, FourierTransform.Direction.Forward);

            var w = new double[n + 1];
            for (int i = 0; i < n + 1; i++)
                w[i] = ww[i].Re;

            w = w.CumulatedSum();
            for (int i = 0; i < w.Length; i++)
                BrownianFractionalMovementPoints.Add(new DataPoint((double)i / n, Math.Pow(n, -H) * w[i]));
        }

        private void BrownianMovements(object param = null)
        {
            BrownianMovement2DPoints = new ObservableCollection<DataPoint>();
            BrownianMovementPoints = new ObservableCollection<DataPoint>();
            var tau = 0.1;

            var time = new double[N];
            for (int i = 0; i < N; i++)
                time[i] = (i + 1) * tau;

            var k = Math.Sqrt(2 * D * tau);
            var dx = new double[N];
            var dy = new double[N];
            for (int i = 0; i < N; i++)
            {
                dx[i] = k * NormalRandom.GetNormal();
                dy[i] = k * NormalRandom.GetNormal();
            }

            var x = dx.CumulatedSum();
            var y = dy.CumulatedSum();

            var squaredDisplacement = new double[N];
            for (int i = 0; i < N; i++)
                squaredDisplacement[i] = (x[i] * x[i]) + (y[i] * y[i]);

            for (int i = 0; i < N; i++)
            {
                BrownianMovementPoints.Add(new DataPoint(time[i], squaredDisplacement[i]));
                BrownianMovement2DPoints.Add(new DataPoint(x[i], y[i]));
            }
        }

        #endregion Private Methods
    }
}