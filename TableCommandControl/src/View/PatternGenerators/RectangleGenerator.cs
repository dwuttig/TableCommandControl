using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Domain;
using TableCommandControl.Utils;

namespace TableCommandControl.View.PatternGenerators {
    public class RectangleGenerator : ViewModelBase, IPatternGenerator {
        private readonly IMainViewModel _mainViewModel;

        private RelayCommand _generateCommand;

        private int _sideA = 400;

   

        public RectangleGenerator(IMainViewModel mainViewModel) {
            Require.NotNull(mainViewModel, "mainViewModel");
            _mainViewModel = mainViewModel;
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
        public int SideA {
            get { return _sideA; }
            set { SetProperty(ref _sideA, value); }}


        private bool CanGenerate() {
            return true;
        }

       

        private void Generate() {
            _mainViewModel.PolarCoordinates.Clear();
            double currentAngle = 0;
            double angleSteps = 360 / (double)_mainViewModel.Steps;
            IList<PolarCoordinate> polarCoordinates = new List<PolarCoordinate>();
            for (int i = 0; i < _mainViewModel.Steps+1; i++) {
                polarCoordinates.Add(new PolarCoordinate(currentAngle, GetRadiusFroAngle(currentAngle)*SideA/2));
                currentAngle += angleSteps;
            }
            _mainViewModel.PolarCoordinates = polarCoordinates;
        }

        private double GetRadiusFroAngle(double angle) {
            angle = ((angle + 45) % 90 - 45) / 180 * Math.PI;
            return 1 / Math.Cos(angle);
        }


      
    }
}