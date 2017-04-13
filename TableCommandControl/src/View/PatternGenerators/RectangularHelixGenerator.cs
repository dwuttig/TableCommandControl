using System;
using System.Windows;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Domain;

namespace TableCommandControl.View.PatternGenerators {
    public class RectangularHelixGenerator :ViewModelBase, IPatternGenerator{
        private readonly IMainViewModel _mainViewModel;

        public RectangularHelixGenerator(IMainViewModel mainViewModel) {
            if (mainViewModel == null) {
                throw new ArgumentNullException(nameof(mainViewModel));
            }
            _mainViewModel = mainViewModel;
        }

        /// <summary>
        ///    Liefert oder setzt den StartRadius
        /// </summary>
        public int StartRadius {
            get { return _startRadius; }
            set { SetProperty(ref _startRadius, value); }
        }

        private int _startRadius = 10;

        /// <summary>
        ///    Liefert oder setzt den Endradius
        /// </summary>
        public int EndRadius {
            get { return _endRadius; }
            set { SetProperty(ref _endRadius, value); }
        }

        private int _endRadius = 0;

        /// <summary>
        ///    Liefert oder setzt die Anzahl der Windungen.
        /// </summary>
        public double WhorlCount {
            get { return _whorlCount; }
            set { SetProperty(ref _whorlCount, value); }
        }

        private double _whorlCount = 1;

        /// <summary>
        /// Liefert den Command zum generieren des Pfads
        /// </summary>
        public RelayCommand GenerateCommand {
            get {
                if (_generateCommand == null)
                    _generateCommand = new RelayCommand(Generate, CanGenerate);

                return _generateCommand;
            }
        }

        private RelayCommand _generateCommand;

        private void Generate() {
            _mainViewModel.PolarCoordinates.Add(PolarCoordinate.FromPoint(new Point(-50, 50)));
            _mainViewModel.PolarCoordinates.Add(PolarCoordinate.FromPoint(new Point(-50, -50)));
            _mainViewModel.PolarCoordinates.Add(PolarCoordinate.FromPoint(new Point(50, -50)));
            _mainViewModel.PolarCoordinates.Add(PolarCoordinate.FromPoint(new Point(50, 50)));

        }

        private bool CanGenerate() {
            return true;
        }






    }
}