namespace project_leadconsult_core.BE
{
    /// <summary>
    /// Point
    /// </summary>
    public class Point
    {
        /// <summary>
        /// The coordinates
        /// </summary>
        public readonly System.Drawing.Point Coordinates;

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public readonly int No;

        /// <summary>
        /// Initializes a new instance of the <see cref="Point" /> class.
        /// </summary>
        /// <param name="no">The no.</param>
        /// <param name="coord_Axis_X">The coord axis x.</param>
        /// <param name="coord_Axis_Y">The coord axis y.</param>
        public Point(int no, int coord_Axis_X, int coord_Axis_Y)
        {
            No = no;
            Coordinates = new System.Drawing.Point(coord_Axis_X, coord_Axis_Y);
        }
    }
}