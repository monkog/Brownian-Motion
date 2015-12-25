using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using OxyPlot;

namespace Brownian_Motion.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Members

        private double _d;
        private int _n;
        private ICommand _brownianMovementsCommand;
        private ObservableCollection<DataPoint> _brownianMovement2DPoints;
        private ObservableCollection<DataPoint> _brownianMovementPoints;

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
                OnPropertyChanged("D");
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
                OnPropertyChanged("N");
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
                OnPropertyChanged("BrownianMovement2DPoints");
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
                OnPropertyChanged("BrownianMovementPoints");
            }
        }

        public ICommand BrownianMovementsCommand { get { return _brownianMovementsCommand ?? (_brownianMovementsCommand = new DelegateCommand(BrownianMovements)); } }

        #endregion Public Properties

        #region Constructors

        public MainViewModel()
        {
            D = 4.2192e-013;
            N = 1000;
            BrownianMovements(null);
        }

        #endregion Constructors

        #region Private Methods

        private void BrownianMovements(object param)
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

            var x = MathFunctions.CummulatedSum(dx);
            var y = MathFunctions.CummulatedSum(dy);

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

