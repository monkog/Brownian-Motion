using System.Windows.Input;
using BrownianMotion.Helpers;

namespace BrownianMotion.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private BrownianMotion _motion;

		private ICommand _brownianMovementsCommand;

		private ICommand _brownianFractionalMovementsCommand;

		/// <summary>
		/// Gets or sets the Brownian Motion.
		/// </summary>
		public BrownianMotion Motion
		{
			get { return _motion; }
			set
			{
				if (_motion == value) return;
				_motion = value;
				OnPropertyChanged();
			}
		}

		public ICommand BrownianMovementsCommand { get { return _brownianMovementsCommand ?? (_brownianMovementsCommand = new DelegateCommand(BrownianMovements)); } }

		public ICommand BrownianFractionalMovementsCommand { get { return _brownianFractionalMovementsCommand ?? (_brownianFractionalMovementsCommand = new DelegateCommand(BrownianFractionalMovements)); } }

		public MainViewModel()
		{
			Motion = new BrownianMotion();
			Motion.BrownianMovements();
			Motion.BrownianFractionalMovements();
		}

		private void BrownianMovements(object obj)
		{
			Motion.BrownianMovements();
		}

		private void BrownianFractionalMovements(object obj)
		{
			Motion.BrownianFractionalMovements();
		}
	}
}