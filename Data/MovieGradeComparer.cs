using MovieTheater.Models;
using System.Collections.Generic;

namespace MovieTheater.Data
{
    public sealed class MovieGradeComparer : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            double a, b;
            if (double.TryParse(x.MovieGrade, out double i)) a = i;
            else                                             a = 0.0;
            if (double.TryParse(y.MovieGrade, out double j)) b = j;
            else                                             b = 0.0;

            return a > b ? -1 : 1;
        }
    }
}