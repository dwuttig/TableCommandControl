using System;
using System.Data.Common;
using System.Text;
using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Domain;

namespace TableCommandControl.View.PatternGenerators {
    public class HelixGenerator : ViewModelBase, IPatternGenerator {
        private readonly IMainViewModel _mainViewModel;

        private RelayCommand _generateHelixCommand;

        private int _helixEndRadius = 1;

        private int _helixStartRadius = 200;

        private double _helixWhorls = 10;

        public HelixGenerator(IMainViewModel mainViewModel) {
            if (mainViewModel == null) {
                throw new ArgumentNullException(nameof(mainViewModel));
            }
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
            _mainViewModel.PolarCoordinates.Clear();
            double angleSteps = 360 * HelixWhorls / _mainViewModel.Steps;
            double currentAngle = 0;
            double currentRadius = HelixStartRadius;
            StringBuilder sbAngles = new StringBuilder();
            StringBuilder sbRadius = new StringBuilder();
            for (int i = 0; i < _mainViewModel.Steps + 1; i++) {
                _mainViewModel.PolarCoordinates.Add(new PolarCoordinate(currentAngle, currentRadius));
                sbAngles.Append($"{(int)(currentAngle/0.9)},");
                sbRadius.Append($"{(int)currentRadius*10},");
                currentRadius -= radiusSteps;
                currentAngle += angleSteps;
            }
            string angle = sbAngles.ToString();
            string radius = sbRadius.ToString();
        }
    }
}