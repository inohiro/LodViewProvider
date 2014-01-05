using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LodViewProvider {

	public enum AggregationType {
		Min = 0,
		Max = 1,
		Sum = 2,
		Count = 3,
		Average = 4,
		GroupBy = 5,
		OrderBy = 6,
		OrderByDescending = 7
	}

	public class Aggregation : IRequestable	{

		public string Variable { get; private set; }
		public AggregationType AggregationType { get; private set; }
		// public AggregationType OrderByInnnerMethod { get; set; }
		public string OrderByInnerMethod { get; private set; }

		public Aggregation( string variable, AggregationType aggregationType ) {
			Variable = variable.Trim( '\"' );
			AggregationType = aggregationType;
		}

		public Aggregation( string variable, AggregationType aggregationType, string orderByInnerMethod = "Count" ) {
			Variable = variable.Trim( '\"' );
			AggregationType = aggregationType;
			OrderByInnerMethod = orderByInnerMethod;
		}

		public override string ToString() {
			var strBuild = new StringBuilder();

			switch ( AggregationType ) {
				case LodViewProvider.AggregationType.Average: {

				} break;
				case LodViewProvider.AggregationType.Count: {

				} break;
				case LodViewProvider.AggregationType.Max: {

				} break;
				case LodViewProvider.AggregationType.Min: {

				} break;
				default: {
					throw new InvalidAggregationTypeException();
				} break;
			}

			return strBuild.ToString();
		}
	}

	public class InvalidAggregationTypeException : Exception {
	}
}
