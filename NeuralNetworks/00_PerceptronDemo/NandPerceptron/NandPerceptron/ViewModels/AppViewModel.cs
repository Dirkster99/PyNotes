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
        private double _Bias = 3;

        private List<BinaryOperation> _NandItems;
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
            OnComputePerceptron(Input1, Input2, Bias);
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

                    OnComputePerceptron(_Input1, _Input2, Bias);
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

                    OnComputePerceptron(_Input1, _Input2, Bias);
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
        private void OnComputePerceptron(double input_1, double input_2, double bias)
        {
            var weights = new double[2]{ -2, -2 };
            var inputs = new double[2] { input_1, input_2 };

            double output = ComputePerceptron(weights, inputs);

            if ((output + bias) > 0)
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
