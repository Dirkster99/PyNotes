namespace NandPerceptron.ViewModels
{
    internal class BinaryOperation
    {
        #region fields

        #endregion fields

        #region ctors
        /// <summary>
        /// Class constructor
        /// </summary>
        public BinaryOperation(byte input1, byte input2, byte result)
            : this()
        {
            Input1 = input1;
            Input2 = input2;
            Result = result;
        }

        protected BinaryOperation()
        {
        }
        #endregion ctors

        public byte Input1
        {
            get;
        }

        public byte Input2
        {
            get;
        }

        public byte Result
        {
            get;
        }
    }
}
