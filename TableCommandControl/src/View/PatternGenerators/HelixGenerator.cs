using System.Collections.Generic;
using System.Collections.ObjectModel;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Domain;
using TableCommandControl.Utils;

namespace TableCommandControl.View.PatternGenerators {
    public class HelixGenerator : ViewModelBase, IPatternGenerator {
        private readonly IMainViewModel _mainViewModel;
        private RelayCommand _generateHelixCommand;
        private int _helixEndRadius = 1;
        private int _helixStartRadius = 250;
        private double _helixWhorls = 10;

        public HelixGenerator(IMainViewModel mainViewModel) {
            Require.NotNull(mainViewModel, "mainViewModel");
            _mainViewModel = mainViewModel;
        }

        /// <summary>
        ///     Liefert den Command zum Erzeugen der Helix
        /// </summary>
        public RelayCommand GenerateHelixCommand {
            get {
                if (_generateHelixCommand == null) {
                    _generateHelixCommand = new RelayCommand(GenerateHelix, CanGenerateHelix);
                }

                return _generateHelixCommand;
            }
        }

        /// <summary>
        ///     Liefert oder setzt den Endradius der Helix
        /// </summary>
        public int HelixEndRadius {
            get { return _helixEndRadius; }
            set { SetProperty(ref _helixEndRadius, value); }
        }

        /// <summary>
        ///     Liefert oder setzt Startradius der Spirale
        /// </summary>
        public int HelixStartRadius {
            get { return _helixStartRadius; }
            set { SetProperty(ref _helixStartRadius, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die Anzahl der Windungen der Helix
        /// </summary>
        public double HelixWhorls {
            get { return _helixWhorls; }
            set { SetProperty(ref _helixWhorls, value); }
        }

        private bool CanGenerateHelix() {
            return HelixStartRadius >= HelixEndRadius && HelixWhorls > 0;
        }

        private void GenerateHelix() {
            double radiusSteps = (HelixStartRadius - HelixEndRadius) / (double)_mainViewModel.Steps;
            double angleSteps = 360 * HelixWhorls / _mainViewModel.Steps;
            double currentAngle = 0;
            double currentRadius = HelixStartRadius;
            IList<PolarCoordinate> polarCoordinates = new List<PolarCoordinate>();
            for (int i = 0; i < _mainViewModel.Steps + 1; i++) {
                polarCoordinates.Add(new PolarCoordinate(currentAngle, currentRadius));
                currentRadius -= radiusSteps;
                currentAngle += angleSteps;
            }
            _mainViewModel.PolarCoordinates = polarCoordinates;
        }
    }
}