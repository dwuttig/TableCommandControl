using System.Collections.ObjectModel;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Domain;

namespace TableCommandControl.View {
    public class MainViewModel : WindowViewModelBase, IMainViewModel {
        private int _circleRadius = 1;

        private RelayCommand _generateCircleCommand;

        private RelayCommand _generateHelixCommand;

        private int _helixEndRadius = 1;

        private int _helixStartRadius = 2;

        private double _helixWhorls = 1;

        private ObservableCollection<PolarCoordinate> _polarCoordinates = new ObservableCollection<PolarCoordinate>();

        private int _steps = 200;

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

        /// <summary>
        ///     Liefert oder setzt die zu senden Polarkoordinaten.
        /// </summary>
        public ObservableCollection<PolarCoordinate> PolarCoordinates {
            get { return _polarCoordinates; }
            set { SetProperty(ref _polarCoordinates, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die Schrittanzahl beim Generieren der Pfade.
        /// </summary>
        public int Steps {
            get { return _steps; }
            set { SetProperty(ref _steps, value); }
        }

        private bool CanGenerateCircle() {
            return CircleRadius > 0;
        }

        private bool CanGenerateHelix() {
            return HelixStartRadius >= HelixEndRadius && HelixWhorls > 0;
        }

        private void GenerateCircle() {
            PolarCoordinates.Clear();
            double angleSteps = 360 / (double)Steps;
            double currentAngle = 0;
            for (int i = 0; i < Steps+1; i++) {
                PolarCoordinates.Add(new PolarCoordinate(currentAngle, _circleRadius));
                currentAngle += angleSteps;
            }
        }

        private void GenerateHelix() {
            double radiusSteps = (HelixStartRadius - HelixEndRadius) / (double)Steps;
            PolarCoordinates.Clear();
            double angleSteps = 360 * HelixWhorls / Steps;
            double currentAngle = 0;
            double currentRadius = HelixStartRadius;
            for (int i = 0; i < Steps+1; i++) {
                PolarCoordinates.Add(new PolarCoordinate(currentAngle, currentRadius));
                currentRadius -= radiusSteps;
                currentAngle += angleSteps;
            }
        }
    }
}