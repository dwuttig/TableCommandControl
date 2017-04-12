using System;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Domain;

namespace TableCommandControl.View.PatternGenerators {
    public class CircleGenerator : ViewModelBase, IPatternGenerator {
        private readonly IMainViewModel _mainViewModel;

        private int _circleRadius = 1;

        private RelayCommand _generateCircleCommand;

        public CircleGenerator(IMainViewModel mainViewModel) {
            if (mainViewModel == null) {
                throw new ArgumentNullException(nameof(mainViewModel));
            }
            _mainViewModel = mainViewModel;
        }

        /// <summary>
        ///     Liefert oder setzt den Radius des Kreises der generiert werden soll.
        /// </summary>
        public int CircleRadius {
            get { return _circleRadius; }
            set { SetProperty(ref _circleRadius, value); }
        }

        /// <summary>
        ///     Liefert den Command zum Generieren eines Kreises anhand des Radius
        /// </summary>
        public RelayCommand GenerateCircleCommand {
            get {
                if (_generateCircleCommand == null) {
                    _generateCircleCommand = new RelayCommand(GenerateCircle, CanGenerateCircle);
                }

                return _generateCircleCommand;
            }
        }

        private bool CanGenerateCircle() {
            return CircleRadius > 0;
        }

        private void GenerateCircle() {
            _mainViewModel.PolarCoordinates.Clear();
            double angleSteps = 360 / (double)_mainViewModel.Steps;
            double currentAngle = 0;
            for (int i = 0; i < _mainViewModel.Steps + 1; i++) {
                _mainViewModel.PolarCoordinates.Add(new PolarCoordinate(currentAngle, _circleRadius));
                currentAngle += angleSteps;
            }
        }
    }
}