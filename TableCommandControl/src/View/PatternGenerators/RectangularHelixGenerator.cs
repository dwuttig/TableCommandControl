using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Domain;

namespace TableCommandControl.View.PatternGenerators {
    public class RectangularHelixGenerator : ViewModelBase, IPatternGenerator {
        private readonly IMainViewModel _mainViewModel;

        private int _endLength = 10;

        private RelayCommand _generateCommand;

        private int _startLength = 300;

        private double _whorlCount = 10;

        public RectangularHelixGenerator(IMainViewModel mainViewModel) {
            if (mainViewModel == null) {
                throw new ArgumentNullException(nameof(mainViewModel));
            }
            _mainViewModel = mainViewModel;
        }

        /// <summary>
        ///     Liefert oder setzt den Endradius
        /// </summary>
        public int EndLength {
            get { return _endLength; }
            set { SetProperty(ref _endLength, value); }
        }

        /// <summary>
        ///     Liefert den Command zum generieren des Pfads
        /// </summary>
        public RelayCommand GenerateCommand {
            get {
                if (_generateCommand == null) {
                    _generateCommand = new RelayCommand(Generate, CanGenerate);
                }

                return _generateCommand;
            }
        }

        /// <summary>
        ///     Liefert oder setzt den StartRadius
        /// </summary>
        public int StartLength {
            get { return _startLength; }
            set { SetProperty(ref _startLength, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die Anzahl der Windungen.
        /// </summary>
        public double WhorlCount {
            get { return _whorlCount; }
            set { SetProperty(ref _whorlCount, value); }
        }

        private bool CanGenerate() {
            return true;
        }

        private void Generate() {
            double currentRadius = StartLength;
            double radiusStepSize = (StartLength - EndLength) / WhorlCount;

            double currentAngle = 0;
            double angleSteps = 360.0 / _mainViewModel.Steps;
            IList<PolarCoordinate> coordinates = new List<PolarCoordinate>();

            for (int w = 0; w < WhorlCount; w++) {
                for (int i = 0; i < _mainViewModel.Steps + 1; i++) {
                    coordinates.Add(
                        new PolarCoordinate(currentAngle, GetRadiusFroAngle(currentAngle) * currentRadius / 2));
                    currentAngle += angleSteps;
                }
                currentRadius -= radiusStepSize;
            }

            _mainViewModel.PolarCoordinates = new ObservableCollection<PolarCoordinate>(coordinates);
        }

        private static double GetRadiusFroAngle(double angle) {
            angle = ((angle + 45) % 90 - 45) / 180 * Math.PI;
            return 1 / Math.Cos(angle);
        }
    }
}