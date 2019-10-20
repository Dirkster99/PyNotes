using System.Collections.Generic;

namespace NandPerceptron.ViewModels
{
    /// <summary>
    /// Implements a viewmodel that controls all background operations in this application.
    /// </summary>
    internal class AppViewModel : Base.BaseViewModel
    {
        #region fields
        private byte _Input1, _Input2;
        private byte _Output;
        private double _Weight2, _Weight1;
        private double _Bias = 3;

        private List<BinaryOperation> _NandItems;
        private double _TempResult;
        #endregion fields

        #region ctors
        /// <summary>
        /// Class constructor
        /// </summary>
        public AppViewModel()
        {
            _NandItems = new List<BinaryOperation>();
            _NandItems.Add(new BinaryOperation(0, 0, 1));
            _NandItems.Add(new BinaryOperation(0, 1, 1));
            _NandItems.Add(new BinaryOperation(1, 0, 1));
            _NandItems.Add(new BinaryOperation(1, 1, 0));
            
            _Input1 = 0;
            _Input2 = 0;
            _Weight1 = -2;
            _Weight2 = -2;
            OnComputePerceptron(Input1, Weight1, Input2, Weight2, Bias);
        }
        #endregion ctors

        #region properties
        /// <summary>
        /// Gets/sets values for input parameter 1 (actual values should always be 1 or 0).
        /// </summary>
        public byte Input1
        {
            get { return _Input1; }

            set
            {
                if (value != _Input1)
                {
                    _Input1 = value;
                    NotifyPropertyChanged(() => Input1);

                    OnComputePerceptron(Input1, Weight1, Input2, Weight2, Bias);
                }
            }
        }

        /// <summary>
        /// Gets/sets values for input parameter 2 (actual values should always be 1 or 0).
        /// </summary>
        public byte Input2
        {
            get { return _Input2; }

            set
            {
                if (value != _Input2)
                {
                    _Input2 = value;
                    NotifyPropertyChanged(() => Input2);

                    OnComputePerceptron(Input1, Weight1, Input2, Weight2, Bias);
                }
            }
        }

        /// <summary>
        /// Gets/sets values for weight parameter 1 (actual values should always be 1 or 0).
        /// </summary>
        public double Weight1
        {
            get { return _Weight1; }

            set
            {
                if (value != _Weight1)
                {
                    _Weight1 = value;
                    NotifyPropertyChanged(() => Weight1);

                    OnComputePerceptron(Input1, Weight1, Input2, Weight2, Bias);
                }
            }
        }

        /// <summary>
        /// Gets/sets values for weight parameter 2 (actual values should always be 1 or 0).
        /// </summary>
        public double Weight2
        {
            get { return _Weight2; }

            set
            {
                if (value != _Weight2)
                {
                    _Weight2 = value;
                    NotifyPropertyChanged(() => Weight2);

                    OnComputePerceptron(Input1, Weight1, Input2, Weight2, Bias);
                }
            }
        }

        /// <summary>
        /// Gets the Bias which controls with <see cref="Input1"/> and <see cref="Input2"/>
        /// the result of the Perceptron computation.
        /// </summary>
        public double Bias
        {
            get { return _Bias; }

            protected set
            {
                if (value != _Bias)
                {
                    _Bias = value;
                    NotifyPropertyChanged(() => Output);
                }
            }
        }

        public double TempResult
        {
            get { return _TempResult; }

            protected set
            {
                if (value != _TempResult)
                {
                    _TempResult = value;
                    NotifyPropertyChanged(() => TempResult);
                    NotifyPropertyChanged(() => TempResultDescr);
                }
            }
        }

        public string TempResultDescr
        {
            get
            {
                if (TempResult <= 0)
                    return "<= 0";
                else
                    return "> 0";
            }
        }

        /// <summary>
        /// Gets/sets values for input parameter 2 (actual values should always be 1 or 0).
        /// </summary>
        public byte Output
        {
            get { return _Output; }

            protected set
            {
                if (value != _Output)
                {
                    _Output = value;
                    NotifyPropertyChanged(() => Output);
                }
            }
        }

        public IEnumerable<BinaryOperation> NANDItems
        {
            get
            {
                return _NandItems;
            }
        }
        #endregion properties

        #region methods
        private void OnComputePerceptron(double input_1, double weight_1
                                       , double input_2, double weight_2, double bias)
        {
            var weights = new double[2]{ weight_1, weight_2 };
            var inputs = new double[2] { input_1, input_2 };

            TempResult = ComputePerceptron(weights, inputs) + bias;

            if (TempResult > 0)
                Output = 1;
            else
                Output = 0;
        }

        private double ComputePerceptron(double[] weights, double[] inputs)
        {
            double dotProduct = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                dotProduct += weights[i] * inputs[i];
            }

            return dotProduct;
        }
        #endregion methods
    }
}
