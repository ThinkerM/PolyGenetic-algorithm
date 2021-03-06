﻿using System;
using System.Linq;
using System.Text;
using System.Drawing;
using Genetic_Algorithm.GA.Generics;
using Polygons.Utils;

namespace Polygons.GA
{
    
    /// <summary>
    /// <see cref="T:Genetic_Algorithm.GA.Generics.GeneticAlgorithmAdapter`2" /> which contains the logic for crossover of the <see cref="T:Polygons.GA.PolygonIndividual" />s
    /// </summary>
    public class PolygonAdapter : GeneticAlgorithmAdapter<PolygonIndividual, IPolygonGene>
    {
        
        public PolygonAdapter(IFitnessCalculator<PolygonIndividual, IPolygonGene> fitnessCalculator) : base(fitnessCalculator)
        {
        }

        
        public override PolygonIndividual CrossOver(PolygonIndividual parent1, PolygonIndividual parent2)
        {
            var parent1AngleOrderedGenome = parent1.Genome.OrderBy(g => g.AngleRelativeToCentroid.Radians);
            var parent2AngleOrderedGenome = parent2.Genome.OrderBy(g => g.AngleRelativeToCentroid.Radians);
            if (parent1.Genome.Count == parent2.Genome.Count)
            {
                var zippedGenomes = parent1AngleOrderedGenome.Zip(parent2AngleOrderedGenome, (first, second)
                    => new {gene1 = first, gene2 = second });

                var newGenome = zippedGenomes.Select(genePair => UniqueRandom.Instance.HalfProbability()
                                                         ? genePair.gene1
                                                         : genePair.gene2);

                var childColor = GetChildColor(parent1.Polygon.OutlineColor, parent2.Polygon.OutlineColor);

                return new PolygonIndividual(
                    newGenome,
                    ChildName(parent1.Name, parent2.Name),
                    childColor);
            }
            else
            { throw new Exception($"Incompatible genomes:{System.Environment.NewLine}{parent1.Name}: Length {parent1.Genome.Count}{System.Environment.NewLine}{parent2.Name}: Length {parent2.Genome.Count}"); }
        }

        private static string ChildName(string p1, string p2)
        {
            int shortestParentLength = Math.Min(p1.Length, p2.Length);
            int longestParentLength = Math.Max(p1.Length, p2.Length);
            var sb = new StringBuilder(longestParentLength);
            for (int i = 0; i < shortestParentLength; i++)
            {
                if (UniqueRandom.Instance.HalfProbability())
                {
                    sb.Append(UniqueRandom.Instance.HalfProbability()
                                  ? p1[i]
                                  : p2[i]);
                }
                else
                { sb.Append(UniqueRandom.Instance.RandomAlphanumericString(1)); }
            }
            int lengthToFill = Math.Min(longestParentLength, 15); //prevent too long names to avoid this becoming a bottleneck
            sb.Append(UniqueRandom.Instance.RandomAlphanumericString(lengthToFill - sb.Length));
            return sb.ToString();
        }

        private static Color GetChildColor(Color p1, Color p2)
        {
            return UniqueRandom.Instance.RandomColor();
        }
    }
}
