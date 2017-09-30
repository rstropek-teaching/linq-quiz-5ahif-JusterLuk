using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqQuiz.Library
{
    public static class Quiz
    {
        /// <summary>
        /// Returns all even numbers between 1 and the specified upper limit.
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// </exception>
        public static int[] GetEvenNumbers(int exclusiveUpperLimit)
        {
            // Why do you need the first `.ToArray()`?
            return((from num in (Enumerable.Range(1, exclusiveUpperLimit - 1).ToArray()) where (num % 2) == 0 select num).ToArray());
        }

        /// <summary>
        /// Returns the squares of the numbers between 1 and the specified upper limit 
        /// that can be divided by 7 without a remainder (see also remarks).
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="OverflowException">
        ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
        /// </exception>
        /// <remarks>
        /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// The result is in descending order.
        /// </remarks>
        public static int[] GetSquares(int exclusiveUpperLimit)
        {
            if(exclusiveUpperLimit > 1)
            {
                checked
                {
                    return ((from num in (Enumerable.Range(1, exclusiveUpperLimit - 1).ToArray()) where (num * num) % 7 == 0 orderby num descending select num * num).ToArray());
                }
            }
            else
            {
                return new int[0];
            }

           
        }

        /// <summary>
        /// Returns a statistic about families.
        /// </summary>
        /// <param name="families">Families to analyze</param>
        /// <returns>
        /// Returns one statistic entry per family in <paramref name="families"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="families"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
        /// in <paramref name="families"/> is empty.
        /// </remarks>
        public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
        {
            if (families == null)
            {
                throw new ArgumentNullException();
            }

            // In such cases, it is recommended to use `var` in the variable declaration. The type
            // is obvious because you can see it on the right side of the assignment -> no need
            // to repeat it on the left side.
            List<FamilySummary> sol = new List<FamilySummary>();
            foreach (var family in families)
            {
                sol.Add(new FamilySummary
                {
                    AverageAge = family.Persons.Count == 0 ? 0 : family.Persons.Average(p => p.Age),
                    FamilyID = family.ID,
                    NumberOfFamilyMembers = family.Persons.Count
                });
            }

            // You have to return an array. You know the size of the array (=size of `families`). Why not
            // using an array instead of `List<T>` for `sol`? That would save you from having to call
            // `ToArray` at the end?
            return sol.ToArray();
        }

        /// <summary>
        /// Returns a statistic about the number of occurrences of letters in a text.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>
        /// Collection containing the number of occurrences of each letter (see also remarks).
        /// </returns>
        /// <remarks>
        /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
        /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
        /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
        /// with number of occurrences equal to zero.
        /// </remarks>
        public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text)
        {
            // Same as above: Prefer `var`
            char[] textLetters = text.ToUpper().ToCharArray();
            List<int> letters = Enumerable.Range('A', 'Z').ToList(); //integers sind = chars
            List<(char letter, int numberOfOccurrences)> sol = new List<(char letter, int numberOfOccurrences)>();

            foreach (var letter in letters)
            {
                var count = textLetters.Count(l => l == letter);
                if (count > 0)
                {
                    sol.Add(((char)letter, count));
                }
            }

            return sol.ToArray(); //Ein eigenes Objekt mit char und int
        }
    }
}
