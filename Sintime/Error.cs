namespace WallE.Sintime
{
    /// <summary>
    /// Class that represents an error.
    /// </summary>
    public class Error
    {
        #region Properties

        /// <summary>
        /// Path of the file of the error.
        /// </summary>
        public string File { get; private set; }

        /// <summary>
        /// Line of the error on file.
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// Type of the error.
        /// </summary>
        public ErrorTypes ErrorType { get; private set; }

        /// <summary>
        /// Explication of the error.
        /// </summary>
        public string Explication { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize an error.
        /// </summary>
        /// <param name="file">Path of the file of the error.</param>
        /// <param name="line">Line of the error on file.</param>
        /// <param name="type">Type of the error.</param>
        /// <param name="explication">Explication of the error.</param>
        public Error(string file, int line, ErrorTypes type, string explication)
        {
            File = file;
            Line = line;
            ErrorType = type;
            Explication = explication;
        }

        #endregion
    }

    /// <summary>
    /// Enum that represents the types of errors.
    /// </summary>
    public enum ErrorTypes
    {
        None,
        Expected,
        Unknown,
    }
}
